namespace Ra8875Driver.Constants;

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