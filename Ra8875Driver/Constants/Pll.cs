namespace Ra8875Driver.Constants;

internal static class Pll
{
    /// <summary>
    /// PLLDIVK: divided by 4. Samples all use this
    /// </summary>
    public const byte DivKStandard = 0b00000010;

    /// <summary>
    /// PLLDIVN value for 320x240 and 480x272 displays
    /// </summary>
    public const byte DivNSmall = 10;

    /// <summary>
    /// PLLDIVN value for 640x480 and 800x480 displays
    /// </summary>
    public const byte DivNLarge = 11;
}