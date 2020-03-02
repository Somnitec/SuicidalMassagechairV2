using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;


public class VariableChoiceNode : BaseNode
{
    [InfoBox("This temporarily represent some condition till implemented")]
    public String Condition;

    [Output()]
    public Connection True, False;

    protected override bool HasConnections()
    {
        return True == null && False == null;
    }
}