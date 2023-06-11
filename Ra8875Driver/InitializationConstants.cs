namespace Ra8875Driver;

/// <summary>
/// Horizontal Non-Display Period Fine Tuning Option Register
/// </summary>
internal static class Hndftr
{
    public const byte HighPolarity = 0b10000000;
    public const byte LowPolarity = 0b00000000;
}

/// <summary>
/// Pixel clock setting register
/// </summary>
internal class Pcsr
{
    /// <summary>
    /// Pixel data fetched at rising edge
    /// </summary>
    public const byte FetchedRisingEdge = 0b0000000;
    
    /// <summary>
    /// Pixel data fetched at falling edge
    /// </summary>
    public const byte FetchedFallingEdge = 0b1000000;

    /// <summary>
    /// Pixel clock set to the system clock period
    /// </summary>
    public const byte SystemClockX1 = 0b00000000;
    
    /// <summary>
    /// Pixel clock set to 2 times the pixel clock period
    /// </summary>
    public const byte SystemClockX2 = 0b00000001;
    
    /// <summary>
    /// Pixel clock set to 4 times the pixel clock period
    /// </summary>
    public const byte SystemClockX4 = 0b00000010;
    
    /// <summary>
    /// Pixel clock set to 8 times the pixel clock period
    /// </summary>
    public const byte SystemClockX8 = 0b00000011;


}

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

internal static class SysR
{
    public const byte ColorDepth8Bpp = 0b00000000;
    public const byte ColorDepth16Bpp = 0b00001100;

    public const byte Mcu8Bit = 0b00000000;
    public const byte Mcu16Bit = 0b00000011;
}
