using Meadow.Hardware;
using Ra8875Driver.Constants;

namespace Ra8875Driver;

public class Ra8875
{
    private const byte CommandWriteByte = 0b10000000;
    private const byte CommandReadByte = 0b11000000;
    private const byte DataWriteByte = 0b00000000;
    private const byte DataReadByte = 0b01000000;
    
    private readonly ISpiBus _spiBus;
    private readonly IDigitalOutputPort _resetPort;
    private readonly IDigitalInputPort _waitPort;
    private readonly IDigitalOutputPort _litePort;
    private readonly IDigitalOutputPort _chipSelect;
    private readonly byte[] _outputBuffer = new byte[2];
    private readonly int _height, _width;

    public Ra8875(
        ISpiBus spiBus,
        IDigitalOutputPort resetPort,
        IDigitalOutputPort litePort,
        IDigitalOutputPort chipSelect,
        IDigitalInputPort waitPort,
        int width,
        int height)
    {
        _spiBus = spiBus;
        _resetPort = resetPort;
        _litePort = litePort;
        _chipSelect = chipSelect;
        _waitPort = waitPort;
        _height = height;
        _width = width;
        
        Initialize();
    }

    private void Initialize()
    {
        // TODO: Code currently specific to 800x480 display
        InitializePll();
        WriteRegister(Registers.SysR, SysR.ColorDepth16Bpp & SysR.Mcu8Bit);
        WriteRegister(Registers.Pcsr, Pcsr.FetchedFallingEdge & Pcsr.SystemClockX2);
        Thread.Sleep(1);
        
        // Horizontal setup
        var fineTuning = Hndftr.LowPolarity & 0x11;
        WriteRegister(Registers.Hdwr, (byte)(_width / 8 - 1)); // pixels = (HDWR + 1) * 8
        WriteRegister(Registers.Hndftr, Hndftr.LowPolarity & 0x11); // fine tuning
        // WriteRegister(Registers.Hndr, _width ); // pixels = (hndr + 1)*8 + (hdnftr/2 + 1)*2 + 2
    }

    private void InitializePll()
    {
        WriteRegister(Registers.PllC1, Pll.DivNLarge);
        Thread.Sleep(1);
        WriteRegister(Registers.PllC2, Pll.DivKStandard);
        Thread.Sleep(1);
    }

    private void WriteCommand(byte command)
    {
        _outputBuffer[0] = CommandWriteByte;
        _outputBuffer[1] = command;
        _spiBus.Write(_chipSelect, _outputBuffer);
    }

    private void WriteData(byte data)
    {
        _outputBuffer[0] = DataWriteByte;
        _outputBuffer[1] = data;
        _spiBus.Write(_chipSelect, _outputBuffer);
    }

    private void WriteRegister(byte register, byte data)
    {
        // TODO: Figure out if we can do this in one Spi?
        WriteCommand(register);
        WriteData(data);
    }
}