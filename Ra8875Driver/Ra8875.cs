using Meadow.Hardware;
using Ra8875Driver.Displays;

namespace Ra8875Driver;

public class Ra8875
{
    private readonly IDigitalOutputPort _resetPort;
    private readonly IDigitalInputPort _waitPort;
    private readonly IDigitalOutputPort _litePort;
    private readonly RegisterCommunicator _registerCommunicator;
    private readonly byte[] _outputBuffer = new byte[2];
    private readonly int _height, _width;
    private readonly DisplayInfo _displayInfo;

    public Ra8875(
        ISpiBus spiBus,
        IDigitalOutputPort resetPort,
        IDigitalOutputPort litePort,
        IDigitalOutputPort chipSelect,
        IDigitalInputPort waitPort,
        DisplayType displayType)
    {
        _resetPort = resetPort;
        _litePort = litePort;
        _waitPort = waitPort;
        _registerCommunicator = new RegisterCommunicator(spiBus, chipSelect);

        _displayInfo = displayType switch
        {
            DisplayType.Lcd800X480 => new Lcd800X480(),
            _ => throw new NotSupportedException($"{displayType} not supported"),
        };

        _height = _displayInfo.Height;
        _width = _displayInfo.Width;

        Initialize();
    }

    private void Initialize()
    {
        var initOptions = _displayInfo.InitOptions;
        
        // A lot of these options came from the official adafruit arduino driver at 
        // https://github.com/adafruit/Adafruit_RA8875/blob/master/Adafruit_RA8875.cpp.  
        // It's not clear how they came up with all these calculations as they don't 
        // totally match the data sheet.

        _registerCommunicator.WriteRegister(Registers.PllC1, initOptions.PllC1);
        Thread.Sleep(1);
        _registerCommunicator.WriteRegister(Registers.PllC2, initOptions.PllC2);
        Thread.Sleep(1);

        var initRegisters = new[]
        {
            new RegisterValue(Registers.SysR, SysR.ColorDepth16Bpp & SysR.Mcu8Bit),
            new RegisterValue(Registers.Pcsr, initOptions.PixelClock),

            // Horizontal setup
            new RegisterValue(Registers.Hdwr, (byte)(_width / 8 - 1)),
            new RegisterValue(Registers.Hndftr, (byte)(Hndftr.HighPolarity & initOptions.HSyncFineTuning)),
            new RegisterValue(Registers.Hndr,
                (byte)((initOptions.HSyncNonDisplayPixels - initOptions.HSyncFineTuning - 2) / 8)),

            new RegisterValue(Registers.Hstr, (byte)(initOptions.HSyncStartPixel / 8 - 1)),
            new RegisterValue(Registers.HPwr, (byte)(initOptions.HSyncPw / 8 - 1)),

            // Vertical setup
            new RegisterValue(Registers.Vdhr0, (byte)((_height - 1 + initOptions.VerticalOffset) & 0xFF)),
            new RegisterValue(Registers.Vdhr1, (byte)((_height - 1 + initOptions.VerticalOffset) >> 8)),
            new RegisterValue(Registers.Vndr0, (byte)(initOptions.HSyncNonDisplayPixels - 1)),
            new RegisterValue(Registers.Vndr1, 0),
            new RegisterValue(Registers.Vstr0, (byte)(initOptions.VSyncStartPixels - 1)),
            new RegisterValue(Registers.Vstr1, 0),
            new RegisterValue(Registers.Vpwr, (byte)(initOptions.VSyncPw - 1)),

            // Set active window
            new RegisterValue(Registers.Hsaw0, 0),
            new RegisterValue(Registers.Hsaw1, 0),
            new RegisterValue(Registers.Heaw0, (byte)((_width - 1) & 0xFF)),
            new RegisterValue(Registers.Heaw1, (byte)((_width - 1) >> 8)),
            new RegisterValue(Registers.Vsaw0, initOptions.VerticalOffset),
            new RegisterValue(Registers.Vsaw1, initOptions.VerticalOffset),
            new RegisterValue(Registers.Veaw0, (byte)((_height - 1 + initOptions.VerticalOffset) & 0xff)),
            new RegisterValue(Registers.Veaw1, (byte)((_height - 1 + initOptions.VerticalOffset) >> 8)),
        };
        
        _registerCommunicator.WriteRegisters(initRegisters);
        
        ClearDisplay(true);
    }

    public void ClearDisplay(bool fullScreen)
    {
        const byte startClearFunction = 0b10000000;
        const byte clearFullDisplay = 0b00000000;
        const byte clearActiveWindow = 0b01000000;
        var clearCommand = (byte)(startClearFunction & (fullScreen ? clearFullDisplay : clearActiveWindow));
        
        _registerCommunicator.WriteRegister(Registers.Mclr, clearCommand);
    }
}