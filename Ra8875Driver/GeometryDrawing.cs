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

    public void DrawRect(ushort x0, ushort y0, ushort x1, ushort y1, Color color, bool fill)
    {
        _registerCommunicator.WriteRegister(Registers.Dlhsr0, (byte)x0);
        _registerCommunicator.WriteRegister(Registers.Dlhsr1, (byte)(x0 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlvsr0, (byte)y0);
        _registerCommunicator.WriteRegister(Registers.Dlvsr1, (byte)(y0 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlher0, (byte)x1);
        _registerCommunicator.WriteRegister(Registers.Dlher1, (byte)(x1 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlver0, (byte)y1);
        _registerCommunicator.WriteRegister(Registers.Dlver1, (byte)(y1 >> 8));
        
        // Color
        var rawColor = color.Color16bppRgb565;
        _registerCommunicator.WriteRegister(Registers.Fgcr0, (byte)((rawColor & 0xF800) >> 11));
        _registerCommunicator.WriteRegister(Registers.Fgcr1, (byte)((rawColor & 0x07e0) >> 5));
        _registerCommunicator.WriteRegister(Registers.Fgcr2, (byte)(rawColor & 0x001f));

        var data = fill ? 0xB0 : 0x90;
        _registerCommunicator.WriteRegister(Registers.Dcr, (byte)data);
    }

    public void DrawCircle(ushort centerX, ushort centerY, byte radius, Color color, bool fill)
    {
        // X and y0 position
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