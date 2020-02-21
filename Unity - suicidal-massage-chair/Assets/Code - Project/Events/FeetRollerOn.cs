using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class FeetRollerOn : NodeScriptFunction
{
    public bool On;

    public override List<string> Serialize()
    {
        return ToList(
            "feet_roller_on:" + BoolToString(On)
        );
    }
}