
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public NodeGraph Graph;
    public ChairMicroControllerState Mock, Arduino;
    public bool ShowColors = true; // TODO implement this
    public bool ShowNodeDebugInfo = true;
    public bool ShowNodeData = true;
}
