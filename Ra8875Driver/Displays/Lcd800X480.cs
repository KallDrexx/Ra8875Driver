namespace Ra8875Driver.Displays;

internal class Lcd800X480 : DisplayInfo
{
    public override int Width => 800;
    public override int Height => 480;

    public override byte PllC1 => Initializer.Pll.DivNLarge;
    public override byte PllC2 => Initializer.Pll.DivKStandard;
    public override byte PixelClock => Initializer.Pcsr.FetchedFallingEdge | Initializer.Pcsr.SystemClockX2;
    public override byte HSyncFineTuning => 0;
    public override byte HSyncNonDisplayPixels => 26;
    public override byte HSyncStartPixel => 32;
    public override byte HSyncPw => 96;
    public override byte VerticalOffset => 0;
    public override ushort VsyncNonDisplayPixels => 32;
    public override ushort VSyncStartPixels => 23;
    public override byte VSyncPw => 2;
}