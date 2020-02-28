using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class CoreButtonResponse : MonoBehaviour
{
    private Settings settings => SettingsHolder.Instance.Settings;

    private void Start()
    {
        Events.Instance.AddListener<UserInputUp>(Repeat);
    }
    public void Repeat(UserInputUp userInputUp)
    {
        if (userInputUp.Button == UserInputButton.Repeat)
        {
            Debug.Log("Repeat");
            settings.Graph.PlayNode(settings.Graph.Current);
        }
    }
}
