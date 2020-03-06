using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using XNodeEditor;

[XNodeEditor.NodeGraphEditor.CustomNodeGraphEditorAttribute(typeof(NodeGraph))]
public class NodeGraphEditor : XNodeEditor.NodeGraphEditor
{
    public override string GetNodeMenuName(Type type)
    {
        if (type.IsSubclassOf(typeof(BaseNode)))
            return base.GetNodeMenuName(type);
        return null;
    }
}