using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Hardware;
using Meadow.Units;

namespace Ra8875Driver.Sample;

public class MeadowApp : App<F7FeatherV2>
{
    private Ra8875 _ra8875 = null!;

    public override Task Initialize()
    {
        var config = new SpiClockConfiguration(
            new Frequency(3000, Frequency.UnitType.Kilohertz),
            SpiClockConfiguration.Mode.Mode0);
        var spiBus = new DebuggingSpiBus(Device.CreateSpiBus(Device.Pins.SCK, Device.Pins.COPI, Device.Pins.CIPO, config))
        {
            LogInputOutput = true,
        };
        
        var waitPort = Device.CreateDigitalInputPort(Device.Pins.D13);
        var resetPort = Device.CreateDigitalOutputPort(Device.Pins.D04, false);
        var chipSelect = Device.CreateDigitalOutputPort(Device.Pins.D03);
        
        Console.WriteLine($"SPI frequency: {spiBus.Configuration.Speed.Kilohertz}khz");
        
        _ra8875 = new Ra8875(spiBus, resetPort, chipSelect, waitPort, DisplayType.Lcd800X480);
        Console.WriteLine("Ra8875 initialized");
        _ra8875.SetDisplayOn(true);
        
        Console.WriteLine("Display started");
        
        _ra8875.DrawCircle(100, 100, 25, Color.Aqua, true);

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        Console.WriteLine("Running");
        while (true)
        {
            await Task.Delay(100);
        }
    }
}