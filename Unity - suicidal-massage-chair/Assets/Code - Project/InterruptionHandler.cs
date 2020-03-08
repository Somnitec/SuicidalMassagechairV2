using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using NodeSystem.Blackboard;
using NodeSystem.BlackBoard;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

public class InterruptionHandler : BlackBoardValueModifier
{
    private BlackBoardValue interruptedCount =>  blackBoard.Values[Settings.InterruptedCountBBName].Value;
    private BlackBoardValue interruptionsBeforeGoingToNode =>  blackBoard.Values[Settings.InterruptionsBeforeGoingToNodeBBName].Value;

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
        SetupBlackBoardValue(Settings.InterruptedCountBBName, 0);
        SetupBlackBoardValue(Settings.InterruptionsHandledBBName, 0);
        SetupBlackBoardValue(Settings.InterruptionsBeforeGoingToNodeBBName,
            settings.InitialInterruptCountBeforeGoingToSpecialNode);
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