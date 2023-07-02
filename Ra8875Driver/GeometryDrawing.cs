using Meadow.Foundation;

namespace Ra8875Driver;

internal class GeometryDrawing
{
    private const byte DcrFill = 0x20;

    private readonly RegisterCommunicator _registerCommunicator;
    private readonly Waiter _waiter;

    public GeometryDrawing(RegisterCommunicator registerCommunicator, Waiter waiter)
    {
        _registerCommunicator = registerCommunicator;
        _waiter = waiter;
    }

    public void DrawRect(ushort x0, ushort y0, ushort x1, ushort y1, Color color, bool fill)
    {
        const byte dcrDrawSquare = 0x10;
        const byte dcrDrawSquareStart = 0x80;

        var (red, green, blue) = color.ToRgb();
        var startDrawOperation = dcrDrawSquareStart | dcrDrawSquare;
        if (fill) startDrawOperation |= DcrFill;

        var dataToWrite = new[] {
            new RegisterValue(Registers.Dlhsr0, (byte)x0),
            new RegisterValue(Registers.Dlhsr1, (byte)(x0 >> 8)),
            new RegisterValue(Registers.Dlvsr0, (byte)y0),
            new RegisterValue(Registers.Dlvsr1, (byte)(y0 >> 8)),
            new RegisterValue(Registers.Dlher0, (byte)x1),
            new RegisterValue(Registers.Dlher1, (byte)(x1 >> 8)),
            new RegisterValue(Registers.Dlver0, (byte)y1),
            new RegisterValue(Registers.Dlver1, (byte)(y1 >> 8)),
            
            // Color
            new RegisterValue(Registers.Fgcr0, red),
            new RegisterValue(Registers.Fgcr1, green),
            new RegisterValue(Registers.Fgcr2, blue),

            new RegisterValue(Registers.Dcr, (byte)startDrawOperation),
        };

        _waiter.WaitForReady();
        _registerCommunicator.WriteRegisters(dataToWrite);
        _waiter.SetNextWait(Registers.Dcr, dcrDrawSquareStart);
    }

    public void DrawCircle(ushort centerX, ushort centerY, byte radius, Color color, bool fill)
    {
        const byte dcrStartCircleDraw = 0x40;
        var (red, green, blue) = color.ToRgb();
        var startDrawOperation = dcrStartCircleDraw;
        if (fill) startDrawOperation |= DcrFill;

        var dataToWrite = new[] {
            // X and y0 position
            new RegisterValue(Registers.Dchr0, (byte)centerX),
            new RegisterValue(Registers.Dchr1, (byte)(centerX >> 8)),
            new RegisterValue(Registers.Dcvr0, (byte)centerY),
            new RegisterValue(Registers.Dcvr1, (byte)(centerY >> 8)),

            // Radius
            new RegisterValue(Registers.Dcrr, radius),

            // Color
            new RegisterValue(Registers.Fgcr0, red),
            new RegisterValue(Registers.Fgcr1, green),
            new RegisterValue(Registers.Fgcr2, blue),

            // Draw
            new RegisterValue(Registers.Dcr, startDrawOperation),
        };

        _waiter.WaitForReady();
        _registerCommunicator.WriteRegisters(dataToWrite);
        _waiter.SetNextWait(Registers.Dcr, dcrStartCircleDraw);
    }

    public void DrawLine(ushort x0, ushort y0, ushort x1, ushort y1, Color color)
    {
        const byte dcrDrawLine = 0x80;
        var (red, green, blue) = color.ToRgb();

        var dataToWrite = new[] {
            new RegisterValue(Registers.Dlhsr0, (byte)x0),
            new RegisterValue(Registers.Dlhsr1, (byte)(x0 >> 8)),
            new RegisterValue(Registers.Dlvsr0, (byte)y0),
            new RegisterValue(Registers.Dlvsr1, (byte)(y0 >> 8)),

            new RegisterValue(Registers.Dlher0, (byte)x1),
            new RegisterValue(Registers.Dlher1, (byte)(x1 >> 8)),
            new RegisterValue(Registers.Dlver0, (byte)y1),
            new RegisterValue(Registers.Dlver1, (byte)(y1 >> 8)),

            // Color
            new RegisterValue(Registers.Fgcr0, red),
            new RegisterValue(Registers.Fgcr1, green),
            new RegisterValue(Registers.Fgcr2, blue),

            new RegisterValue(Registers.Dcr, dcrDrawLine),
        };

        _waiter.WaitForReady();
        _registerCommunicator.WriteRegisters(dataToWrite);
        _waiter.SetNextWait(Registers.Dcr, dcrDrawLine);
    }

    public void DrawTriangle(ushort x0, ushort y0, ushort x1, ushort y1, ushort x2, ushort y2, Color color, bool fill)
    {
        const byte dcrDrawTriangle = 0x81;
        var (red, green, blue) = color.ToRgb();
        var startDrawOperation = dcrDrawTriangle;
        if (fill) startDrawOperation |= DcrFill;

        var dataToWrite = new[] {
            new RegisterValue(Registers.Dlher0, (byte)x1),
            new RegisterValue(Registers.Dlher1, (byte)(x1 >> 8)),
            new RegisterValue(Registers.Dlver0, (byte)y1),
            new RegisterValue(Registers.Dlver1, (byte)(y1 >> 8)),

            new RegisterValue(Registers.Dlhsr0, (byte)x0),
            new RegisterValue(Registers.Dlhsr1, (byte)(x0 >> 8)),
            new RegisterValue(Registers.Dlvsr0, (byte)y0),
            new RegisterValue(Registers.Dlvsr1, (byte)(y0 >> 8)),

            new RegisterValue(Registers.Dtph0, (byte)x2),
            new RegisterValue(Registers.Dtph1, (byte)(x2 >> 8)),
            new RegisterValue(Registers.Dtpv0, (byte)y2),
            new RegisterValue(Registers.Dtpv1, (byte)(y2 >> 8)),

            new RegisterValue(Registers.Fgcr0, red),
            new RegisterValue(Registers.Fgcr1, green),
            new RegisterValue(Registers.Fgcr2, blue),

            new RegisterValue(Registers.Dcr, startDrawOperation),
        };

        _waiter.WaitForReady();
        _registerCommunicator.WriteRegisters(dataToWrite);
        _waiter.SetNextWait(Registers.Dcr, 0x80);
    }
}