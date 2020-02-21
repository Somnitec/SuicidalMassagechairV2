using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class RollerKneadingOn : NodeScriptFunction
{
    public bool On;

    public override List<string> Serialize()
    {
        return ToList("roller_kneading_on:" + BoolToString(On));
    }
}