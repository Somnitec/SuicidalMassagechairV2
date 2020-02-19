using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class UserInterfaceMicroControllerCommandButton : MonoBehaviour
{
    public UserInputButton UserInputButton;

    public void SendButtonToController()
    {
        Events.Instance.Raise(new UserInputUp(UserInputButton));
    }
}
