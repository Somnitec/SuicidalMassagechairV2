
public abstract class AbstractChairMicroController
{
    protected ChairMicroControllerState state;

    public AbstractChairMicroController(ChairMicroControllerState state)
    {
        this.state = state;
        SetupListeners();
    }

    private void SetupListeners()
    {
        ScriptEvent.Instance.AddListener<ResetChair>(Reset);
        ScriptEvent.Instance.AddListener<AirBag>(Airbag);
        ScriptEvent.Instance.AddListener<AirPump>(AirPump);
        ScriptEvent.Instance.AddListener<ChairPosition>(ChairPosition);
    }


    public void RemoveListeners()
    {
        ScriptEvent.Instance.RemoveListener<ResetChair>(Reset);
        ScriptEvent.Instance.RemoveListener<AirBag>(Airbag);
        ScriptEvent.Instance.RemoveListener<AirPump>(AirPump);
        ScriptEvent.Instance.RemoveListener<ChairPosition>(ChairPosition);
    }

    protected abstract void Reset(ResetChair args);
    protected abstract void Airbag(AirBag args);
    protected abstract void ChairPosition(ChairPosition args);
    protected abstract void AirPump(AirPump args);
}
