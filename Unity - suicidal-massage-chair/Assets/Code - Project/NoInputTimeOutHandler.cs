using System;
using System.Collections;
using Framework;
using Input;
using NodeSystem.Blackboard;
using NodeSystem.BlackBoard;
using Sirenix.OdinInspector;
using UnityEngine;

public class NoInputTimeOutHandler : BlackBoardValueModifier
{
    private BlackBoardValue noInputCounter => blackBoard.Values[Settings.NoInputCounterBBName].Value;
    private BlackBoardValue noInputTimeout => blackBoard.Values[Settings.NoInputGoToNodeBBName].Value;
    private BlackBoardValue noInputCanGoToSpecial => blackBoard.Values[Settings.NoInputCanGotToNodeBBName].Value;

    private void OnEnable()
    {
        Events.Instance.AddListener<WaitingForInput>(StartNoInputTimeout);
        Events.Instance.AddListener<AllInput>(StopWaiting);
        Events.Instance.AddListener<NewNode>(StopWaiting);
        SetupBlackBoard();
    }

    private void StopWaiting(NewNode e)
    {
        StopWaiting();
    }

    private void StopWaiting(AllInput e)
    {
        StopWaiting();
    }
    
    private void StopWaiting()
    {
        noInputCounter.Float = 0f;
        noInputCanGoToSpecial.Bool = false;
        StopAllCoroutines();
    }

    [Button]
    private void SetupBlackBoard()
    {
        SetupBlackBoardValue(Settings.NoInputCounterBBName, 0.0f);
        SetupBlackBoardValue(Settings.NoInputGoToNodeBBName, settings.InitialTimeOutBeforeGoingToSpecialNode);
        SetupBlackBoardValue(Settings.NoInputCanGotToNodeBBName, true);
    }

    private void OnDisable()
    {
        Events.Instance.AddListener<WaitingForInput>(StartNoInputTimeout);
    }

    private void StartNoInputTimeout(WaitingForInput e)
    {
        StopAllCoroutines();
        StartCoroutine(NoInputTimeOutTracker());
    }

    private IEnumerator NoInputTimeOutTracker()
    {
        noInputCounter.Float = 0f;
        noInputCanGoToSpecial.Bool = true;
        Debug.Log("StartWaitingNoInput");
        while (true)
        {
            noInputCounter.Float += Time.deltaTime;

            if (noInputCanGoToSpecial.Bool && noInputCounter.Float >= noInputTimeout.Float)
            {
                graph.PlaySpecialNode(Settings.NoInputNodeName);
                noInputCanGoToSpecial.Bool = false;
            }

            yield return null;
        }
    }
}

public class BlackBoardValueModifier : MonoBehaviour
{
    protected Settings settings => SettingsHolder.Instance.Settings;
    protected NodeGraph graph => settings.Graph;
    protected BlackBoard blackBoard => graph.BlackBoard;

    protected void SetupBlackBoardValue(string key, int value)
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

    protected void SetupBlackBoardValue(string key, float value)
    {
        if (!blackBoard.Values.ContainsKey(key))
        {
            var blackBoardValue = new BlackBoardValue(value);
            var blackBoardTypeAndValue = new BlackBoardTypeAndValue(BlackBoardValue.ValueType.Float, blackBoardValue);
            blackBoard.Values.Add(key, blackBoardTypeAndValue);
        }

        blackBoard.Values[key].Type = BlackBoardValue.ValueType.Float;
        blackBoard.Values[key].Value.Float = value;
        blackBoard.Values[key].Value.Type = BlackBoardValue.ValueType.Float;
    }

    protected void SetupBlackBoardValue(string key, bool value)
    {
        if (!blackBoard.Values.ContainsKey(key))
        {
            var blackBoardValue = new BlackBoardValue(value);
            var blackBoardTypeAndValue = new BlackBoardTypeAndValue(BlackBoardValue.ValueType.Bool, blackBoardValue);
            blackBoard.Values.Add(key, blackBoardTypeAndValue);
        }

        blackBoard.Values[key].Type = BlackBoardValue.ValueType.Bool;
        blackBoard.Values[key].Value.Bool = value;
        blackBoard.Values[key].Value.Type = BlackBoardValue.ValueType.Bool;
    }
}