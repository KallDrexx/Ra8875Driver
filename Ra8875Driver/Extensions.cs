using Meadow.Foundation;

namespace Ra8875Driver;

internal static class Extensions
{
    public static (byte red, byte green, byte blue) ToRgb(this Color color)
    {
        var rawColor = color.Color16bppRgb565;
        return ((byte)((rawColor & 0xF800) >> 11), (byte)((rawColor & 0x07e0) >> 5), (byte)(rawColor & 0x001f));
    }
}