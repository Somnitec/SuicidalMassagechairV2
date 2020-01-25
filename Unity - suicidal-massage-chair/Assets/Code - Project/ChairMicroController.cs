using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMicroController : MonoBehaviour
{
    public enum Commands
    {
        Chair_Up,
        Chair_Down
        // TODO
    }

    // Todo rethink this in relation to arguments
    // Possibly event pattern?
    public void SendCommand(Commands command)
    {
        Debug.Log("The " + command.ToString() + " has been send");
    }
}
