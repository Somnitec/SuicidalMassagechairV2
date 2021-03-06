﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
public class ChairMicroControllerCommandsButtonSetup : MonoBehaviour
{
    public GameObject buttonPrefab;
    private ChairMicroController controller;

    private void Start()
    {
        controller = FindObjectOfType<ChairMicroController>();
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
        foreach (ChairMicroController.Commands buttonValue in Enum.GetValues(typeof(ChairMicroController.Commands)))
        {
            // Create game object
            GameObject newButton = GameObject.Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform);
            newButton.name = buttonValue.ToString();

            // Set enum value
            ChairMicroControllerCommandButton chairMicroControllerCommandButton = newButton.GetComponent<ChairMicroControllerCommandButton>();
            chairMicroControllerCommandButton.Command = buttonValue;

            // Set event
            Button button = newButton.GetComponent<Button>();
            UnityEventTools.AddPersistentListener(button.onClick, new UnityAction(chairMicroControllerCommandButton.SendButtonToController));

            // Set text
            TMPro.TextMeshProUGUI textMeshPro = newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            textMeshPro.text = buttonValue.ToString();
        }
    }
#endif
    [Button]
    public void DeleteButtons()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform b in transform)
        {
            if (b.GetComponent<Button>() != null)
                children.Add(b);
        }
        foreach (Transform b in children)
        {
            GameObject.DestroyImmediate(b.gameObject);
        }
    }
}
