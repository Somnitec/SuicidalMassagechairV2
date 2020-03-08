using Framework;
using Input;
using UnityEngine;

public class ButtonPressAudioEffects : MonoBehaviour
{
    private Settings settings => SettingsHolder.Instance.Settings;
    private AudioManager audio => AudioManager.Instance;

    private void Start()
    {
        Events.Instance.AddListener<AllInput>(HandleInput);
    }

    public void HandleInput(AllInput allInput)
    {
        var index = Random.Range(0, settings.ClickSoundEffects.Count);
        var fx = settings.ClickSoundEffects[index];
        audio.PlayEffect(fx);
    }
}