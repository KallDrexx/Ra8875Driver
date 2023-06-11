namespace Ra8875Driver.Displays;

internal class DisplayInitializationOptions
{
    public required byte PllC1 { get; init; }
    public required byte PllC2 { get; init; }
    public required byte PixelClock { get; init; }
    public required byte HSyncNonDisplayPixels { get; init; }
    public required byte HSyncStartPixel { get; init; }
    public required byte HSyncPw { get; init; }
    public required byte HSyncFineTuning { get; init; }
    public required byte VSyncPw { get; init; }
    public required ushort VSyncNonDisplayPixels { get; init; }
    public required ushort VSyncStartPixels { get; init; }
    public required byte VerticalOffset { get; init; }
}