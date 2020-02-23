
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
        ScriptEvent.Instance.AddListener<AirBag>(Airbag);
        ScriptEvent.Instance.AddListener<AirPump>(AirPump);
        ScriptEvent.Instance.AddListener<BackLightColor>(BackLightColor);
        ScriptEvent.Instance.AddListener<BacklightOn>(BackLightOn);
        ScriptEvent.Instance.AddListener<ButtVibration>(ButtVibration);
        ScriptEvent.Instance.AddListener<ChairPosition>(ChairPosition);
        ScriptEvent.Instance.AddListener<ChairStopAll>(ChairStopAll);
        ScriptEvent.Instance.AddListener<FeetRollerOn>(FeetRollingOn);
        ScriptEvent.Instance.AddListener<FeetRollerSpeed>(FeetRollingSpeed);
        ScriptEvent.Instance.AddListener<RecalibrateChair>(RecalibrateChair);
        ScriptEvent.Instance.AddListener<ResetChair>(Reset);
        ScriptEvent.Instance.AddListener<RollerKneadingOn>(RollerKneadingOn);
        ScriptEvent.Instance.AddListener<RollerKneadingSpeed>(RollerKneadingSpeed);
        ScriptEvent.Instance.AddListener<RollerPosition>(RollerPosition);
        ScriptEvent.Instance.AddListener<RollerPoundingOn>(RollerPoundingOn);
        ScriptEvent.Instance.AddListener<RollerPoundingSpeed>(RollerPoundingSpeed);
        ScriptEvent.Instance.AddListener<RedGreenStatusLight>(RedGreenStatusLight);
    }


    public virtual void RemoveListeners()
    {
        ScriptEvent.Instance.RemoveListener<AirBag>(Airbag);
        ScriptEvent.Instance.RemoveListener<AirPump>(AirPump);
        ScriptEvent.Instance.RemoveListener<BackLightColor>(BackLightColor);
        ScriptEvent.Instance.RemoveListener<BacklightOn>(BackLightOn);
        ScriptEvent.Instance.RemoveListener<ButtVibration>(ButtVibration);
        ScriptEvent.Instance.RemoveListener<ChairPosition>(ChairPosition);
        ScriptEvent.Instance.RemoveListener<ChairStopAll>(ChairStopAll);
        ScriptEvent.Instance.RemoveListener<FeetRollerOn>(FeetRollingOn);
        ScriptEvent.Instance.RemoveListener<FeetRollerSpeed>(FeetRollingSpeed);
        ScriptEvent.Instance.RemoveListener<RecalibrateChair>(RecalibrateChair);
        ScriptEvent.Instance.RemoveListener<ResetChair>(Reset);
        ScriptEvent.Instance.RemoveListener<RollerKneadingOn>(RollerKneadingOn);
        ScriptEvent.Instance.RemoveListener<RollerKneadingSpeed>(RollerKneadingSpeed);
        ScriptEvent.Instance.RemoveListener<RollerPosition>(RollerPosition);
        ScriptEvent.Instance.RemoveListener<RollerPoundingOn>(RollerPoundingOn);
        ScriptEvent.Instance.RemoveListener<RollerPoundingSpeed>(RollerPoundingSpeed);
        ScriptEvent.Instance.RemoveListener<RedGreenStatusLight>(RedGreenStatusLight);
    }

    protected abstract void RedGreenStatusLight(RedGreenStatusLight e);
    protected abstract void Reset(ResetChair args);
    protected abstract void Airbag(AirBag args);
    protected abstract void ChairPosition(ChairPosition args);
    protected abstract void AirPump(AirPump args);
    protected abstract void RollerPoundingSpeed(RollerPoundingSpeed e);
    protected abstract void RollerPoundingOn(RollerPoundingOn e);
    protected abstract void RollerPosition(RollerPosition e);
    protected abstract void RollerKneadingSpeed(RollerKneadingSpeed e);
    protected abstract void RollerKneadingOn(RollerKneadingOn e);
    protected abstract void RecalibrateChair(RecalibrateChair e);
    protected abstract void FeetRollingSpeed(FeetRollerSpeed e);
    protected abstract void FeetRollingOn(FeetRollerOn e);
    protected abstract void ChairStopAll(ChairStopAll e);
    protected abstract void ButtVibration(ButtVibration e);
    protected abstract void BackLightOn(BacklightOn e);
    protected abstract void BackLightColor(BackLightColor e);
}
