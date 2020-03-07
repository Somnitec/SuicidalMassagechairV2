using Framework;
using UnityEngine;

public class ButtonPressAudioEffects : MonoBehaviour
{
    private Settings settings => SettingsHolder.Instance.Settings;
    private AudioManager audio => AudioManager.Instance;

    private void Start()
    {
        Events.Instance.AddListener<UserInputUp>(HandleInput);
    }

    public void HandleInput(UserInputUp userInputUp)
    {
        var index = Random.Range(0, settings.ClickSoundEffects.Count);
        var fx = settings.ClickSoundEffects[index];
        audio.PlayEffect(fx);
    }
}