using Meadow.Devices;
using Meadow.Hardware;
using Meadow.Units;

namespace Ra8875Driver.Sample;

public class DebuggingSpiBus : ISpiBus
{
    private readonly ISpiBus _inner;
    
    public Frequency[] SupportedSpeeds => _inner.SupportedSpeeds;
    public SpiClockConfiguration Configuration => _inner.Configuration;
    public bool LogInputOutput { get; set; }
    
    public DebuggingSpiBus(ISpiBus spiBus)
    {
        _inner = spiBus;
    }
    
    public void Read(IDigitalOutputPort? chipSelect, Span<byte> readBuffer, ChipSelectMode csMode = ChipSelectMode.ActiveLow)
    {
        _inner.Read(chipSelect, readBuffer, csMode);
        if (LogInputOutput)
        {
            Console.WriteLine($"SPI Read: {BitConverter.ToString(readBuffer.ToArray())}");
        }
    }

    public void Write(IDigitalOutputPort? chipSelect, Span<byte> writeBuffer, ChipSelectMode csMode = ChipSelectMode.ActiveLow)
    {
        if (LogInputOutput)
        {
            Console.WriteLine($"SPI Write: {BitConverter.ToString(writeBuffer.ToArray())}");
        }
        
        _inner.Write(chipSelect, writeBuffer, csMode);
    }

    public void Exchange(IDigitalOutputPort? chipSelect, Span<byte> writeBuffer, Span<byte> readBuffer, ChipSelectMode csMode = ChipSelectMode.ActiveLow)
    {
        if (LogInputOutput)
        {
            Console.WriteLine($"SPI Exchange Write: {BitConverter.ToString(writeBuffer.ToArray())}");
        }
        
        _inner.Exchange(chipSelect, writeBuffer, readBuffer, csMode);
        
        if (LogInputOutput)
        {
            Console.WriteLine($"SPI Exchange Read: {BitConverter.ToString(readBuffer.ToArray())}");
        }
    }
}