using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

[HideLabel, InlineProperty, HideReferenceObjectPicker]
public class FunctionList
{
    [TableList(AlwaysExpanded = true, HideToolbar = true)]
    [HideLabel]
    [FoldoutGroup("Chair Functions")]
    [PropertyOrder(10)]
    public List<NodeScriptLine> Functions = new List<NodeScriptLine>();

    public float Duration => Functions.Max(f => f.TimeSec as float?) ?? 0;

    [FoldoutGroup("Chair Functions")]
    [HorizontalGroup("Chair Functions/Buttons")]
    [Button]
    public void Sort()
    {
        Functions = Functions.OrderBy(a => a.TimeSec).ToList();
    }

    [FoldoutGroup("Chair Functions")]
    [HorizontalGroup("Chair Functions/Buttons")]
    [Button]
    private void Add()
    {
        Functions.Add(new NodeScriptLine());
    }
}