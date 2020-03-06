using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeSystem.Blackboard;
using NodeSystem.BlackBoard;
using Sirenix.OdinInspector;
using XNode;


public class VariableComparisonNode : BaseNode
{
    public Comparison Comparison = new Comparison();

    [Output()] public Connection True;
    [Output()] public Connection False;

    private NodePort TruePort => GetOutputPort("True");
    private NodePort FalsePort => GetOutputPort("False");
    private BlackBoard bb => NodeGraph.BlackBoard;

    public override void OnNodeEnable()
    {
        if (Comparison.Compare())
            GoToNode(TruePort);
        else
            GoToNode(FalsePort);
    }

    protected override bool HasConnections()
    {
        return TruePort.IsConnected || FalsePort.IsConnected;
    }

    protected override void Init()
    {
        Comparison.BlackBoard = bb;
        Comparison.Name = bb.Values.Keys.First();
    }
}

[Serializable, InlineProperty, HideLabel, Title("Comparison")]
public class Comparison
{
    [HorizontalGroup("Comparison", MinWidth = 0.4f)] [ValueDropdown("GetKeys"), PropertyOrder(-2)] [HideLabel]
    public string Name;

    [HorizontalGroup("Comparison", Width = 0.2f)]
    [HideLabel]
    [ShowInInspector, InlineProperty, PropertyOrder(-1)]
    private BlackBoardValue value => GetBlackBoardValue();

    private BlackBoardValue GetBlackBoardValue()
    {
        if(string.IsNullOrEmpty(Name))
            BlackBoard.Values.First();
        
        return BlackBoard != null ? BlackBoard.Values[Name].Value : new BlackBoardValue();
    }

    [HorizontalGroup("Comparison", Width = 0.15f)] [HideLabel] [ValueDropdown("ComparatorStrings")]
    public Comparator Comparator;

    [HorizontalGroup("Comparison", Width = 0.2f)] [HideLabel]
    public BlackBoardValue CompareValue;

    [ReadOnly, PropertyOrder(10)] public BlackBoard BlackBoard;

    [ShowInInspector] private bool Result => Compare();

    public bool Compare()
    {
        return Compare(value, Comparator, CompareValue);
    }

    private IEnumerable<string> GetKeys()
    {
        return BlackBoard.Values.Keys;
    }

    private IEnumerable ComparatorStrings = new ValueDropdownList<Comparator>()
    {
        {"==", Comparator.Equals},
        {"!=", Comparator.NotEquals},
        {">", Comparator.LargerThan},
        {">=", Comparator.LargerOrEquals},
        {"<", Comparator.SmallerThan},
        {"<=", Comparator.SmallerOrEquals},
    };
    
    public bool Compare(BlackBoardValue value, Comparator comparator, BlackBoardValue compareValue)
    {
        compareValue.Type = value.Type;
            
        switch (comparator)
        {
            case Comparator.LargerThan:
                return CompareTo(value, compareValue) == 1;
            case Comparator.LargerOrEquals:
                return CompareTo(value, compareValue) == -1 || Equals(value, compareValue);
            case Comparator.SmallerThan:
                return CompareTo(value, compareValue) == -1;
            case Comparator.SmallerOrEquals:
                return CompareTo(value, compareValue) == -1 || Equals(value, compareValue);
            case Comparator.NotEquals:
                return !Equals(value,compareValue);
            case Comparator.Equals:
            default:
                return Equals(value,compareValue);
        }
    }

    private int CompareTo(BlackBoardValue blackBoardValue, BlackBoardValue compareValue)
    {
        return blackBoardValue.Comparable.CompareTo(compareValue.Comparable);
    }

    private bool Equals(BlackBoardValue blackBoardValue, BlackBoardValue compareValue)
    {
        return blackBoardValue.Comparable.Equals(compareValue.Comparable);
    }
}

public enum Comparator
{
    SmallerThan,
    SmallerOrEquals,
    LargerThan,
    LargerOrEquals,
    Equals,
    NotEquals,
}