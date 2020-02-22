using Framework;
using TMPro;
using UnityEngine;
using Event = Framework.Event;

[ExecuteInEditMode]
public class MessageConsole : MonoBehaviour
{
    public TextMeshProUGUI tmPro;

    // Start is called before the first frame update
    void Start()
    {
        Events.Instance.AddListener<ConsoleMessage>(UpdateText);
    }

    private void UpdateText(ConsoleMessage e)
    {
        tmPro.text = e.Text;
        Debug.Log(e.Text);
    }
}