namespace Ra8875Driver.Displays;

internal abstract class DisplayInfo
{
    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract DisplayInitializationOptions InitOptions { get; }

    protected abstract byte FineTuning { get; }
}