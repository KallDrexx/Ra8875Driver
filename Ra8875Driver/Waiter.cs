namespace Ra8875Driver;

internal class Waiter 
{
    private readonly RegisterCommunicator _registerCommunicator;
    private Registers? _registerToCall;
    private byte? _waitFlag;

    public Waiter(RegisterCommunicator registerCommunicator)
    {
        _registerCommunicator = registerCommunicator;
    }

    public void SetNextWait(Registers registerToCall, byte waitFlag)
    {
        _registerToCall = registerToCall;
        _waitFlag = waitFlag;
    }

    public void WaitForReady()
    {
        if (_registerToCall == null)
        {
            return; // Nothing to wait for
        }

        // Read from the register until the flag we are waiting on is no longer set
        while(true)
        {
            var registerValue = _registerCommunicator.ReadRegister(_registerToCall.Value);
            if ((registerValue & _waitFlag!.Value) == 0)
            {
                _registerToCall = null;
                _waitFlag = null;
                return;
            }
        }
    }
}