using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserInterfaceButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    private UserInterfaceMicroController controller;

    private List<GameObject> initializedButtons = new List<GameObject>();

    private void Start()
    {
        controller = FindObjectOfType<UserInterfaceMicroController>();
    }

    [Button]
    public void SetupButtons()
    {
        Start();
        DeleteButtons();
        CreateButtonsFromUserInterFaceButtonValues();
    }

    private void CreateButtonsFromUserInterFaceButtonValues()
    {
        foreach (UserInterfaceMicroControllerData.UserInterfaceButtonValue buttonValue in Enum.GetValues(typeof(UserInterfaceMicroControllerData.UserInterfaceButtonValue)))
        {
            // Create game object
            GameObject newButton = GameObject.Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform);
            newButton.name = buttonValue.ToString();

            // Set enum value
            UserInterfaceButton userInterfaceButton = newButton.GetComponent<UserInterfaceButton>();
            userInterfaceButton.UserInterfaceButtonValue = buttonValue;

            // Set event
            Button button = newButton.GetComponent<Button>();
            UnityEventTools.AddPersistentListener(button.onClick, new UnityAction(userInterfaceButton.SendButtonToController));

            // Set text
            TMPro.TextMeshProUGUI textMeshPro = newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            textMeshPro.text = buttonValue.ToString();

            initializedButtons.Add(newButton);
        }
    }

    [Button]
    public void DeleteButtons()
    {
        List<Transform> children =new List<Transform>();
        foreach (Transform b in transform)
        {
            children.Add(b);
        }
        foreach (Transform b in children)
        {
            GameObject.DestroyImmediate(b.gameObject);
        }
        initializedButtons.Clear();
    }
}
