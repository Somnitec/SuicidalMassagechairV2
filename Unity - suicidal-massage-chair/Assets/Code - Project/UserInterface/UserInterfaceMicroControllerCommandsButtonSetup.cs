using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class UserInterfaceMicroControllerCommandsButtonSetup : MonoBehaviour
{
    public GameObject buttonPrefab;
    private UserInterfaceMicroController controller;

    private void Start()
    {
        controller = FindObjectOfType<UserInterfaceMicroController>();
    }

#if UNITY_EDITOR

    [Button]
    public void SetupButtons()
    {
        Start();
        DeleteButtons();
        CreateButtonsFromUserInterFaceButtonValues();
    }

    private void CreateButtonsFromUserInterFaceButtonValues()
    {
        foreach (AllInputButtons buttonValue in Enum.GetValues(typeof(AllInputButtons)))
        {
            // Create game object
            GameObject newButton = GameObject.Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform);
            newButton.name = buttonValue.ToString();

            // Set enum value
            UserInterfaceMicroControllerCommandButton userInterfaceButton = newButton.GetComponent<UserInterfaceMicroControllerCommandButton>();
            userInterfaceButton.allInputButtons = buttonValue;

            // Set event
            Button button = newButton.GetComponent<Button>();
            UnityEventTools.AddPersistentListener(button.onClick, new UnityAction(userInterfaceButton.SendButtonToController));

            // Set text
            TMPro.TextMeshProUGUI textMeshPro = newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            textMeshPro.text = buttonValue.ToString();
        }
    }

#endif

    [Button]
    public void DeleteButtons()
    {
        List<Transform> children =new List<Transform>();
        foreach (Transform b in transform)
        {
            if(b.GetComponent<Button>() != null)
                children.Add(b);
        }
        foreach (Transform b in children)
        {
            GameObject.DestroyImmediate(b.gameObject);
        }
    }
}
