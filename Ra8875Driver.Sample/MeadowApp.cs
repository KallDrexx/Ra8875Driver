using Meadow;
using Meadow.Devices;

namespace Ra8875Driver.Sample;

public class MeadowApp : App<F7FeatherV2>
{
    private Ra8875 _ra8875 = null!;

    public override Task Initialize()
    {
        var spiBus = Device.CreateSpiBus();
        var waitPort = Device.CreateDigitalInputPort(Device.Pins.D03);
        var resetPort = Device.CreateDigitalOutputPort(Device.Pins.D02);
        var chipSelect = Device.CreateDigitalOutputPort(Device.Pins.D01, true);
        var lite = Device.CreateDigitalOutputPort(Device.Pins.D13);

        _ra8875 = new Ra8875(spiBus, resetPort, lite, chipSelect, waitPort, DisplayType.Lcd800X480);
        _ra8875.SetDisplayOn(true);

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        while (true)
        {
            await Task.Delay(100);
        }
    }
}