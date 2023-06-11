namespace Ra8875Driver.Displays;

internal abstract class DisplayInfo
{
    public int Width { get; }
    public int Height { get; }
    public DisplayInitializationOptions InitOptions { get; }

    protected abstract byte FineTuning { get; }
    
    protected byte CalculateHdwr()
    {
        // Horizontal display pixels = (HDWR + 1) * 8
        return (byte)((Width / 8) - 1);
    }

    protected byte CalculateHndr(byte hsyncNonDisplay, byte hsyncFineTune)
    {
        // This doesn't really match the data sheets, but open source drivers
        // seem to work this way
        return (byte)((hsyncNonDisplay - hsyncFineTune - 2) / 8);
    }
}