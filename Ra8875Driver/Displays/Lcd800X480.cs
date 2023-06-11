namespace Ra8875Driver.Displays;

internal class Lcd800X480 : DisplayInfo
{
    public override int Width => 800;
    public override int Height => 480;

    protected override byte FineTuning => 0;

    public override DisplayInitializationOptions InitOptions => new DisplayInitializationOptions
    {
        PllC1 = Pll.DivNLarge,
        PllC2 = Pll.DivKStandard,
        PixelClock = Pcsr.FetchedFallingEdge & Pcsr.SystemClockX2,

        // Values come from the Adafruit arduino driver https://github.com/adafruit/Adafruit_RA8875/blob/master/Adafruit_RA8875.cpp.
        // I could not figure out how they originate.
        HSyncNonDisplayPixels = 26,
        HSyncStartPixel = 32,
        HSyncPw = 96,
        HSyncFineTuning = 0,
        VSyncNonDisplayPixels = 32,
        VSyncStartPixels = 23,
        VSyncPw = 2,
        VerticalOffset = 0,
    };
    

}