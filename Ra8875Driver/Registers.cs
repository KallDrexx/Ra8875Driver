namespace Ra8875Driver;

internal enum Registers : byte
{
    /// <summary>
    /// Power and Display Control Register
    /// </summary>
    Pwrr = 0x01,
    
    /// <summary>
    /// Pixel clock setting register
    /// </summary>
    Pcsr = 0x04,
   
    /// <summary>
    /// System configuration register
    /// </summary>
    SysR = 0x10,
    
    /// <summary>
    /// Horizontal width register
    /// </summary>
    Hdwr = 0x14,

    /// <summary>
    /// Horizontal Non-Display Period Fine Tuning Option register
    /// </summary>
    Hndftr = 0x15,

    /// <summary>
    /// LCD Horizontal Non-Display Period Register
    /// </summary>
    Hndr = 0x16,

    /// <summary>
    /// HSync start position register
    /// </summary>
    Hstr = 0x17,

    /// <summary>
    /// HSync pulse width register
    /// </summary>
    HPwr = 0x18,

    /// <summary>
    /// Vertical display height register
    /// </summary>
    Vdhr0 = 0x19,

    /// <summary>
    /// vertical display height register #2
    /// </summary>
    Vdhr1 = 0x1a,

    /// <summary>
    /// Vertical non-display period register
    /// </summary>
    Vndr0 = 0x1b,

    /// <summary>
    /// Vertical non-display period register #2
    /// </summary>
    Vndr1 = 0x1c,

    /// <summary>
    /// Vertical start position register
    /// </summary>
    Vstr0 = 0x1d,

    /// <summary>
    /// Vertical start position register 2
    /// </summary>
    Vstr1 = 0x1e,

    /// <summary>
    /// VSync pulse width register
    /// </summary>
    Vpwr = 0x1f,

    /// <summary>
    /// Horizontal start point 0 of active window
    /// </summary>
    Hsaw0 = 0x30,

    /// <summary>
    /// Horizontal start point 1 of active window
    /// </summary>
    Hsaw1 = 0x31,

    /// <summary>
    /// Vertical start point 0 of active window
    /// </summary>
    Vsaw0 = 0x32,
    
    /// <summary>
    /// Vertical start point 1 of active window
    /// </summary>
    Vsaw1 = 0x33,

    /// <summary>
    /// Horizontal end point 0 of active window
    /// </summary>
    Heaw0 = 0x34,

    /// <summary>
    /// Horizontal end point 1 of active window
    /// </summary>
    Heaw1 = 0x35,

    /// <summary>
    /// Vertical end point of active window 0
    /// </summary>
    Veaw0 = 0x36,

    /// <summary>
    /// Vertical end point of active window 1
    /// </summary>
    Veaw1 = 0x37,
    
    PllC1 = 0x88,
    PllC2 = 0x89,
   
    /// <summary>
    /// PWM1 control register
    /// </summary>
    P1Cr = 0x8a,
   
    /// <summary>
    /// PWM1 duty control register
    /// </summary>
    P1Dcr = 0x8b,
   
    /// <summary>
    /// Memory clear control register
    /// </summary>
    Mclr = 0x8e,
   
    /// <summary>
    /// Extra general purpose IO register
    /// </summary>
    GpioX = 0xc7,
}