using Meadow.Foundation;

namespace Ra8875Driver;

internal class GeometryDrawing
{
    private const byte DcrFill = 0x20;
    
    private readonly RegisterCommunicator _registerCommunicator;

    public GeometryDrawing(RegisterCommunicator registerCommunicator)
    {
        _registerCommunicator = registerCommunicator;
    }

    public void DrawRect(ushort x0, ushort y0, ushort x1, ushort y1, Color color, bool fill)
    {
        const byte dcrDrawSquare = 0x10;
        const byte dcrDrawSquareStart = 0x80;
        
        _registerCommunicator.WriteRegister(Registers.Dlhsr0, (byte)x0);
        _registerCommunicator.WriteRegister(Registers.Dlhsr1, (byte)(x0 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlvsr0, (byte)y0);
        _registerCommunicator.WriteRegister(Registers.Dlvsr1, (byte)(y0 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlher0, (byte)x1);
        _registerCommunicator.WriteRegister(Registers.Dlher1, (byte)(x1 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlver0, (byte)y1);
        _registerCommunicator.WriteRegister(Registers.Dlver1, (byte)(y1 >> 8));
        
        // Color
        var (red, green, blue) = color.ToRgb();
        _registerCommunicator.WriteRegister(Registers.Fgcr0, red);
        _registerCommunicator.WriteRegister(Registers.Fgcr1, green);
        _registerCommunicator.WriteRegister(Registers.Fgcr2, blue);

        var data = dcrDrawSquareStart | dcrDrawSquare;
        if (fill) data |= DcrFill;
        _registerCommunicator.WriteRegister(Registers.Dcr, (byte)data);
    }

    public void DrawCircle(ushort centerX, ushort centerY, byte radius, Color color, bool fill)
    {
        const byte dcrStartCircleDraw = 0x40;
        
        // X and y0 position
        _registerCommunicator.WriteRegister(Registers.Dchr0, (byte)centerX);
        _registerCommunicator.WriteRegister(Registers.Dchr1, (byte)(centerX >> 8));
        _registerCommunicator.WriteRegister(Registers.Dcvr0, (byte)centerY);
        _registerCommunicator.WriteRegister(Registers.Dcvr1, (byte)(centerY >> 8));
        
        // Radius
        _registerCommunicator.WriteRegister(Registers.Dcrr, radius);
        
        // Color
        var (red, green, blue) = color.ToRgb();
        _registerCommunicator.WriteRegister(Registers.Fgcr0, red);
        _registerCommunicator.WriteRegister(Registers.Fgcr1, green);
        _registerCommunicator.WriteRegister(Registers.Fgcr2, blue);
        
        // Draw
        var data = dcrStartCircleDraw;
        if (fill) data |= DcrFill;
        _registerCommunicator.WriteRegister(Registers.Dcr, data);
    }

    public void DrawLine(ushort x0, ushort y0, ushort x1, ushort y1, Color color)
    {
        const byte dcrDrawLine = 0x80;
        
        _registerCommunicator.WriteRegister(Registers.Dlhsr0, (byte)x0);
        _registerCommunicator.WriteRegister(Registers.Dlhsr1, (byte)(x0 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlvsr0, (byte)y0);
        _registerCommunicator.WriteRegister(Registers.Dlvsr1, (byte)(y0 >> 8));
        
        _registerCommunicator.WriteRegister(Registers.Dlher0, (byte)x1);
        _registerCommunicator.WriteRegister(Registers.Dlher1, (byte)(x1 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlver0, (byte)y1);
        _registerCommunicator.WriteRegister(Registers.Dlver1, (byte)(y1 >> 8));
        
        // Color
        var (red, green, blue) = color.ToRgb();
        _registerCommunicator.WriteRegister(Registers.Fgcr0, red);
        _registerCommunicator.WriteRegister(Registers.Fgcr1, green);
        _registerCommunicator.WriteRegister(Registers.Fgcr2, blue);
        
        _registerCommunicator.WriteRegister(Registers.Dcr, dcrDrawLine);
    }

    public void DrawTriangle(ushort x0, ushort y0, ushort x1, ushort y1, ushort x2, ushort y2, Color color, bool fill)
    {
        const byte dcrDrawTriangle = 0x81;
        
        _registerCommunicator.WriteRegister(Registers.Dlher0, (byte)x1);
        _registerCommunicator.WriteRegister(Registers.Dlher1, (byte)(x1 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlver0, (byte)y1);
        _registerCommunicator.WriteRegister(Registers.Dlver1, (byte)(y1 >> 8));
        
        _registerCommunicator.WriteRegister(Registers.Dlhsr0, (byte)x0);
        _registerCommunicator.WriteRegister(Registers.Dlhsr1, (byte)(x0 >> 8));
        _registerCommunicator.WriteRegister(Registers.Dlvsr0, (byte)y0);
        _registerCommunicator.WriteRegister(Registers.Dlvsr1, (byte)(y0 >> 8));
        
        _registerCommunicator.WriteRegister(Registers.Dtph0, (byte)x2);
        _registerCommunicator.WriteRegister(Registers.Dtph1, (byte)(x2 >> 8)); 
        _registerCommunicator.WriteRegister(Registers.Dtpv0, (byte)y2);
        _registerCommunicator.WriteRegister(Registers.Dtpv1, (byte)(y2 >> 8));
        
        var (red, green, blue) = color.ToRgb();
        _registerCommunicator.WriteRegister(Registers.Fgcr0, red);
        _registerCommunicator.WriteRegister(Registers.Fgcr1, green);
        _registerCommunicator.WriteRegister(Registers.Fgcr2, blue);
        
        var data = dcrDrawTriangle;
        if (fill) data |= DcrFill;
        _registerCommunicator.WriteRegister(Registers.Dcr, data);
    }
}