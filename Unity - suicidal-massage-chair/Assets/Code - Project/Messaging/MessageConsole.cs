using Framework;
using TMPro;
using UnityEngine;
using Event = Framework.Event;

[ExecuteInEditMode]
public class MessageConsole : MonoBehaviour
{
    public TextMeshProUGUI tmPro;

    private Messager _messager;
    void Start()
    {
        Events.Instance.AddListener<ConsoleMessage>(UpdateText);
        _messager = GetComponent<Messager>();
    }

    private void UpdateText(ConsoleMessage e)
    {
        if(e.Messager != _messager)
            return;
        
        tmPro.text = e.Text;
    }
}