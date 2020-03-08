using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using NodeSystem.Blackboard;
using NodeSystem.BlackBoard;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

public class InterruptionHandler : MonoBehaviour
{
    private Settings settings => SettingsHolder.Instance.Settings;
    private NodeGraph graph => settings.Graph;
    private BlackBoard blackBoard => graph.BlackBoard;
    private BlackBoardValue interruptedCount =>  blackBoard.Values[Settings.InterruptedCountName].Value;
    private BlackBoardValue interruptionsBeforeGoingToNode =>  blackBoard.Values[Settings.InterruptionsBeforeGoingToNodeName].Value;

    private void OnEnable()
    {
        SetupBlackBoard();
        Events.Instance.AddListener<InterruptedInput>(HandleInterruption);
    }

    private void OnDisable()
    {
        Events.Instance.RemoveListener<InterruptedInput>(HandleInterruption);
    }

    [Button]
    private void SetupBlackBoard()
    {
        SetupBlackBoardValue(Settings.InterruptedCountName, 0);
        SetupBlackBoardValue(Settings.InterruptionsHandledName, 0);
        SetupBlackBoardValue(Settings.InterruptionsBeforeGoingToNodeName,
            settings.InitialInterruptCountBeforeGoingToSpecialNode);
    }

    private void SetupBlackBoardValue(string key, int value)
    {
        if (!blackBoard.Values.ContainsKey(key))
        {
            var blackBoardValue = new BlackBoardValue(value);
            var blackBoardTypeAndValue = new BlackBoardTypeAndValue(BlackBoardValue.ValueType.Int, blackBoardValue);
            blackBoard.Values.Add(key, blackBoardTypeAndValue);
        }
        
        blackBoard.Values[key].Type = BlackBoardValue.ValueType.Int;
        blackBoard.Values[key].Value.Int = value;
        blackBoard.Values[key].Value.Type = BlackBoardValue.ValueType.Int;
    }

    [Button]
    private void HandleInterruption(InterruptedInput e)
    {
        interruptedCount.Int++;
        
        if (interruptedCount.Int < interruptionsBeforeGoingToNode.Int) return;
        
        graph.PlaySpecialNode(Settings.InteruptedNodeName);
        interruptedCount.Int = 0;
    }
}