using Ra8875Driver.Constants;

namespace Ra8875Driver.Displays;

internal class Lcd800X480 : DisplayInfo
{
    public int Width => 800;
    public int Height => 480;

    protected override byte FineTuning => 0;

    public DisplayInitializationOptions InitOptions => new DisplayInitializationOptions
    {
        PllC1 = Pll.DivNLarge,
        PllC2 = Pll.DivKStandard,
        SysR = SysR.ColorDepth16Bpp & SysR.Mcu8Bit,
        Pcsr = Pcsr.FetchedFallingEdge & Pcsr.SystemClockX2,
        Hndftr = (byte)(Hndftr.HighPolarity & FineTuning),
        Hndr = CalculateHndr(26, 0),
        
    }

}