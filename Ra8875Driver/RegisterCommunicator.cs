using Meadow.Hardware;

namespace Ra8875Driver;

internal readonly record struct RegisterValue(Registers Register, byte Value);

internal class RegisterCommunicator
{
    
    private const byte CommandWriteByte = 0b10000000;
    private const byte CommandReadByte = 0b11000000;
    private const byte DataWriteByte = 0b00000000;
    private const byte DataReadByte = 0b01000000;
    
    private readonly ISpiBus _spiBus;
    private readonly IDigitalOutputPort _chipSelect;
    private readonly byte[] _outputBuffer = new byte[2];

    public RegisterCommunicator(ISpiBus spiBus, IDigitalOutputPort chipSelect)
    {
        _spiBus = spiBus;
        _chipSelect = chipSelect;
    }

    public void WriteRegisters(ReadOnlySpan<RegisterValue> valuesToWrite)
    {
        foreach (var toWrite in valuesToWrite)
        {
            WriteRegister(toWrite.Register, toWrite.Value);
        }
    }
    
    public void WriteRegister(Registers register, byte data)
    {
        // TODO: Figure out if we can do this in one Spi call?
        WriteCommand((byte)register);
        WriteData(data);
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
}