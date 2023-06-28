﻿using Meadow.Foundation;
using Meadow.Hardware;
using Ra8875Driver.Displays;

namespace Ra8875Driver;

public class Ra8875
{
    private readonly IDigitalOutputPort _resetPort;
    private readonly IDigitalInputPort _waitPort;
    private readonly RegisterCommunicator _registerCommunicator;
    private readonly byte[] _outputBuffer = new byte[2];
    private readonly int _height, _width;
    private readonly DisplayInfo _displayInfo;
    private readonly GeometryDrawing _geometryDrawing;

    public Ra8875(
        ISpiBus spiBus,
        IDigitalOutputPort resetPort,
        IDigitalOutputPort chipSelect,
        IDigitalInputPort waitPort,
        DisplayType displayType)
    {
        _resetPort = resetPort;
        _waitPort = waitPort;
        _registerCommunicator = new RegisterCommunicator(spiBus, chipSelect);
        _geometryDrawing = new GeometryDrawing(_registerCommunicator);

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
        // Write the results of readbuffer to the console in hex
        _registerCommunicator.WriteRegister(Registers.Pwrr, (byte)(enabled ? 0x80 : 0x00));
        
        // Enable backlight
        _registerCommunicator.WriteRegister(Registers.GpioX, 0x01);
        
        var pwm1Value = (byte)((enabled ? 0x80 : 0x00) | 0x0a);
        _registerCommunicator.WriteRegister(Registers.P1Cr, pwm1Value);
        _registerCommunicator.WriteRegister(Registers.P1Dcr, 0xff);
        
    }

    public void DrawCircle(ushort centerX, ushort centerY, byte radius, Color color, bool fill)
    {
        _geometryDrawing.DrawCircle(centerX, centerY, radius, color, fill);
    }
}