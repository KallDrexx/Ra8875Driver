using Ra8875Driver.Displays;

namespace Ra8875Driver;

internal static class Initializer
{
    public static void Initialize(
        RegisterCommunicator registerCommunicator,
        DisplayInfo display)
    {
        // A lot of these options came from the official adafruit arduino driver at 
        // https://github.com/adafruit/Adafruit_RA8875/blob/master/Adafruit_RA8875.cpp.  
        // It's not clear how they came up with all these calculations as they don't 
        // totally match the data sheet.

        registerCommunicator.WriteRegister(Registers.PllC1, display.PllC1);
        Thread.Sleep(1);
        registerCommunicator.WriteRegister(Registers.PllC2, display.PllC2);
        Thread.Sleep(1);

        var initRegisters = new[]
        {
            new RegisterValue(Registers.SysR, SysR.ColorDepth16Bpp & SysR.Mcu8Bit),
            new RegisterValue(Registers.Pcsr, display.PixelClock),

            // Horizontal setup
            new RegisterValue(Registers.Hdwr, (byte)(display.Width / 8 - 1)),
            new RegisterValue(Registers.Hndftr, (byte)(Hndftr.HighPolarity & display.HSyncFineTuning)),
            new RegisterValue(Registers.Hndr,
                (byte)((display.HSyncNonDisplayPixels - display.HSyncFineTuning - 2) / 8)),

            new RegisterValue(Registers.Hstr, (byte)(display.HSyncStartPixel / 8 - 1)),
            new RegisterValue(Registers.HPwr, (byte)(display.HSyncPw / 8 - 1)),

            // Vertical setup
            new RegisterValue(Registers.Vdhr0, (byte)((display.Height - 1 + display.VerticalOffset) & 0xFF)),
            new RegisterValue(Registers.Vdhr1, (byte)((display.Height - 1 + display.VerticalOffset) >> 8)),
            new RegisterValue(Registers.Vndr0, (byte)(display.HSyncNonDisplayPixels - 1)),
            new RegisterValue(Registers.Vndr1, 0),
            new RegisterValue(Registers.Vstr0, (byte)(display.VSyncStartPixels - 1)),
            new RegisterValue(Registers.Vstr1, 0),
            new RegisterValue(Registers.Vpwr, (byte)(display.VSyncPw - 1)),

            // Set active window
            new RegisterValue(Registers.Hsaw0, 0),
            new RegisterValue(Registers.Hsaw1, 0),
            new RegisterValue(Registers.Heaw0, (byte)((display.Width - 1) & 0xFF)),
            new RegisterValue(Registers.Heaw1, (byte)((display.Width - 1) >> 8)),
            new RegisterValue(Registers.Vsaw0, display.VerticalOffset),
            new RegisterValue(Registers.Vsaw1, display.VerticalOffset),
            new RegisterValue(Registers.Veaw0, (byte)((display.Height - 1 + display.VerticalOffset) & 0xff)),
            new RegisterValue(Registers.Veaw1, (byte)((display.Height - 1 + display.VerticalOffset) >> 8)),
        };
        
        registerCommunicator.WriteRegisters(initRegisters);
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
    
    /// <summary>
    /// Horizontal Non-Display Period Fine Tuning Option Register
    /// </summary>
    private static class Hndftr
    {
        public const byte HighPolarity = 0b10000000;
        public const byte LowPolarity = 0b00000000;
    }

    private static class SysR
    {
        public const byte ColorDepth8Bpp = 0b00000000;
        public const byte ColorDepth16Bpp = 0b00001100;

        public const byte Mcu8Bit = 0b00000000;
        public const byte Mcu16Bit = 0b00000011;
    }
}