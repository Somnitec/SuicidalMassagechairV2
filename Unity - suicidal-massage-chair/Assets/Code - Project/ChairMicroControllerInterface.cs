
#region Chair Events

public class ChairDown : ChairEvent
{
}

public class ChairUp : ChairEvent
{
}

public class ChairReset : ChairEvent
{
}

public class ChairEvent
{
}

[System.Serializable]
public class ChairEvents : Framework.GenericEvents<ChairEvent>
{
}

#endregion

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
        ChairEvents.Instance.AddListener<ChairReset>(Reset);
        ChairEvents.Instance.AddListener<ChairUp>(Up);
        ChairEvents.Instance.AddListener<ChairDown>(Down);
    }

    public void RemoveListeners()
    {
        ChairEvents.Instance.RemoveListener<ChairReset>(Reset);
        ChairEvents.Instance.RemoveListener<ChairUp>(Up);
        ChairEvents.Instance.RemoveListener<ChairDown>(Down);
    }

    protected abstract void Reset(ChairReset args);
    protected abstract void Up(ChairUp args);
    protected abstract void Down(ChairDown args);
}
