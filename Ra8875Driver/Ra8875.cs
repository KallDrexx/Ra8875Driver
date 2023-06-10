using Meadow.Hardware;

namespace Ra8875Driver;

public class Ra8875
{
    private readonly ISpiBus _spiBus;
    private readonly IDigitalOutputPort _resetPort;
    private readonly IDigitalInputPort _waitPort;
    private readonly IDigitalOutputPort _litePort;
    private readonly IDigitalOutputPort _chipSelect;

    public Ra8875(
        ISpiBus spiBus,
        IDigitalOutputPort resetPort,
        IDigitalOutputPort litePort,
        IDigitalOutputPort chipSelect,
        IDigitalInputPort waitPort)
    {
        _spiBus = spiBus;
        _resetPort = resetPort;
        _litePort = litePort;
        _chipSelect = chipSelect;
        _waitPort = waitPort;
    }
}