using Meadow.Foundation;
using Meadow.Hardware;
using Ra8875Driver.Displays;

namespace Ra8875Driver;

public class Ra8875
{
    private readonly IDigitalOutputPort _resetPort;
    private readonly RegisterCommunicator _registerCommunicator;
    private readonly DisplayInfo _displayInfo;
    private readonly GeometryDrawing _geometryDrawing;
    private readonly Waiter _waiter;

    public Ra8875(
        ISpiBus spiBus,
        IDigitalOutputPort resetPort,
        IDigitalOutputPort chipSelect,
        DisplayType displayType)
    {
        _resetPort = resetPort;
        _registerCommunicator = new RegisterCommunicator(spiBus, chipSelect);
        _waiter = new Waiter(_registerCommunicator);
        _geometryDrawing = new GeometryDrawing(_registerCommunicator, _waiter);

        _displayInfo = displayType switch
        {
            DisplayType.Lcd800X480 => new Lcd800X480(),
            _ => throw new NotSupportedException($"{displayType} not supported"),
        };

        // Reset device
        _resetPort.State = false;
        Thread.Sleep(1);
        _resetPort.State = true;
        Thread.Sleep(10);

        Initializer.Initialize(_registerCommunicator, _displayInfo);
        
        ClearDisplay(true);
    }

    public void SetDisplayOn(bool enabled)
    {
        // Write the results of readbuffer to the console in hex
        _registerCommunicator.WriteRegister(Registers.Pwrr, (byte)(enabled ? 0x80 : 0x00));
        
        // Enable backlight
        _registerCommunicator.WriteRegister(Registers.GpioX, 0x01);
        
        var pwm1Value = (byte)((enabled ? 0x80 : 0x00) | 0x0a);
        _registerCommunicator.WriteRegister(Registers.P1Cr, pwm1Value);
        _registerCommunicator.WriteRegister(Registers.P1Dcr, 0xff);
        
    }
    
    public void DrawRect(ushort x0, ushort y0, ushort x1, ushort y1, Color color, bool fill)
    {
        _geometryDrawing.DrawRect(x0, y0, x1, y1, color, fill);
    }

    public void DrawCircle(ushort centerX, ushort centerY, byte radius, Color color, bool fill)
    {
        _geometryDrawing.DrawCircle(centerX, centerY, radius, color, fill);
    }
    
    public void DrawLine(ushort x0, ushort y0, ushort x1, ushort y1, Color color)
    {
        _geometryDrawing.DrawLine(x0, y0, x1, y1, color);
    }
    
    public void DrawTriangle(ushort x0, ushort y0, ushort x1, ushort y1, ushort x2, ushort y2, Color color, bool fill)
    {
        _geometryDrawing.DrawTriangle(x0, y0, x1, y1, x2, y2, color, fill);
    }

    public void ClearDisplay(bool fullScreen)
    {
        const byte startClearFunction = 0b10000000;
        const byte clearFullDisplay = 0b00000000;
        const byte clearActiveWindow = 0b01000000;
        var clearCommand = (byte)(startClearFunction | (fullScreen ? clearFullDisplay : clearActiveWindow));
        
        _registerCommunicator.WriteRegister(Registers.Mclr, clearCommand);
    }
}