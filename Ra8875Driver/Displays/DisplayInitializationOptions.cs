namespace Ra8875Driver.Displays;

internal class DisplayInitializationOptions
{
    public required byte PllC1 { get; init; }
    public required byte PllC2 { get; init; }
    public required byte SysR { get; init; }
    public required byte Pcsr { get; init; }
    public required byte Hdwr { get; init; }
    public required byte Hndftr { get; init; }
    public required byte Hndr { get; init; }
    public required byte Hstr { get; init; }
    public required byte Vdhr0 { get; init; }
    public required byte Vdhr1 { get; init; }
    public required byte Vndr0 { get; init; }
    public required byte Vndr1 { get; init; }
    public required byte Vstr0 { get; init; }
    public required byte Vstr1 { get; init; }
    public required byte Vpwr { get; init; }
}