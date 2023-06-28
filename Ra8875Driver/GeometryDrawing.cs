using Meadow.Foundation;

namespace Ra8875Driver;

internal class GeometryDrawing
{
    private const byte StartLineSquareTriangleDraw = 0b10000000;
    private const byte StartCircleDraw = 0b01000000;
    private const byte DrawFilled = 0b00100000;
    
    private readonly RegisterCommunicator _registerCommunicator;

    public GeometryDrawing(RegisterCommunicator registerCommunicator)
    {
        _registerCommunicator = registerCommunicator;
    }

    public void DrawCircle(ushort centerX, ushort centerY, byte radius, Color color, bool fill)
    {
        // X and y position
        _registerCommunicator.WriteRegister(Registers.Dchr0, (byte)(centerX & 0xFF));
        _registerCommunicator.WriteRegister(Registers.Dchr1, (byte)(centerX >> 8));
        _registerCommunicator.WriteRegister(Registers.Dcvr0, (byte)(centerY & 0xFF));
        _registerCommunicator.WriteRegister(Registers.Dcvr1, (byte)(centerY >> 8));
        
        // Radius
        _registerCommunicator.WriteRegister(Registers.Dcrr, radius);
        
        // Color
        var rawColor = color.Color16bppRgb565;
        _registerCommunicator.WriteRegister(Registers.Fgcr0, (byte)((rawColor & 0xF800) >> 11));
        _registerCommunicator.WriteRegister(Registers.Fgcr1, (byte)((rawColor & 0x07e0) >> 5));
        _registerCommunicator.WriteRegister(Registers.Fgcr2, (byte)(rawColor & 0x001f));
        
        // Draw
        var command = (byte)(StartCircleDraw & (fill ? DrawFilled : 0));
        _registerCommunicator.WriteRegister(Registers.Dcr, command);
    }
}