using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public UserInterfaceMicroController controller;

    private List<GameObject> initializedButtons = new List<GameObject>();

    [Button]
    public void SetupButtons()
    {
        DeleteButtons();
        CreateButtonsFromUserInterFaceButtonValues();
    }

    private void CreateButtonsFromUserInterFaceButtonValues()
    {
        foreach (UserInterfaceMicroControllerData.UserInterfaceButtonValue buttonValue in Enum.GetValues(typeof(UserInterfaceMicroControllerData.UserInterfaceButtonValue)))
        {
            GameObject newButton = GameObject.Instantiate(buttonPrefab);
            newButton.transform.parent = transform;
            newButton.name = buttonValue.ToString();
            Button button = newButton.GetComponent<Button>();
            button.onClick.AddListener(delegate () { controller.SendButton(buttonValue); });
            TMPro.TextMeshProUGUI textMeshPro = newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            textMeshPro.text = buttonValue.ToString();
            initializedButtons.Add(newButton);
        }
    }

    [Button]
    public void DeleteButtons()
    {
        foreach (GameObject b in initializedButtons)
        {
            GameObject.DestroyImmediate(b);
        }
        initializedButtons.Clear();
    }
}
