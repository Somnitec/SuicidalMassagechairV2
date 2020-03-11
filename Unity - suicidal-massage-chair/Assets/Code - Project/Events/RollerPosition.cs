using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class RollerPosition : NodeScriptFunction
{
    [InfoBox("0 is down and 1 is up")][Range(0,1)]
    public float NewPosition;

    public override List<string> SerializeToJson()
    {
        return ToList(ToJson("roller_position_target",(int)(10000f * NewPosition)));
    }
}