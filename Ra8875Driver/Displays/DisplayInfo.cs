namespace Ra8875Driver.Displays;

internal abstract class DisplayInfo
{
    public abstract int Width { get; }
    public abstract int Height { get; }

    public abstract byte PllC1 { get; }
    public abstract byte PllC2 { get; }
    public abstract byte PixelClock { get; }
    public abstract byte HSyncFineTuning { get; }
    public abstract byte HSyncNonDisplayPixels { get; }
    public abstract byte HSyncStartPixel { get; }
    public abstract byte HSyncPw { get; }
    
    public abstract byte VerticalOffset { get; }
    public abstract ushort VsyncNonDisplayPixels { get; }
    public abstract ushort VSyncStartPixels { get; }
    public abstract byte VSyncPw { get; }
    
}