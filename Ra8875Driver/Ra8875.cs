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

        // Reset device
        _resetPort.State = false;
        Thread.Sleep(1);
        _resetPort.State = true;
        Thread.Sleep(10);

        Initializer.Initialize(_registerCommunicator, _displayInfo);
        
        ClearDisplay(true);
    }

    public void ClearDisplay(bool fullScreen)
    {
        const byte startClearFunction = 0b10000000;
        const byte clearFullDisplay = 0b00000000;
        const byte clearActiveWindow = 0b01000000;
        var clearCommand = (byte)(startClearFunction | (fullScreen ? clearFullDisplay : clearActiveWindow));
        
        _registerCommunicator.WriteRegister(Registers.Mclr, clearCommand);
    }

    public void SetDisplayOn(bool enabled)
    {
        _registerCommunicator.WriteRegister(Registers.Pwrr, (byte)(enabled ? 0x80 : 0x00));
        
        // Enable backlight
        _registerCommunicator.WriteRegister(Registers.GpioX, 0x01);
        
        var pwm1Value = (byte)((enabled ? 0x80 : 0x00) | 0x0a);
        _registerCommunicator.WriteRegister(Registers.P1Cr, pwm1Value);
        _registerCommunicator.WriteRegister(Registers.P1Dcr, 0xff);
    }
}