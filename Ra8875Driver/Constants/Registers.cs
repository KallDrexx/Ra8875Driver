namespace Ra8875Driver.Constants;

internal static class Registers
{
    /// <summary>
    /// Pixel clock setting register
    /// </summary>
    public const byte Pcsr = 0x04;
    
    /// <summary>
    /// Horizontal width register
    /// </summary>
    public const byte Hdwr = 0x14;

    /// <summary>
    /// Horizontal Non-Display Period Fine Tuning Option register
    /// </summary>
    public const byte Hndftr = 0x15;

    /// <summary>
    /// LCD Horizontal Non-Display Period Register
    /// </summary>
    public const byte Hndr = 0x16;
    
    public const byte SysR = 0x10;
    public const byte PllC1 = 0x88;
    public const byte PllC2 = 0x89;
}