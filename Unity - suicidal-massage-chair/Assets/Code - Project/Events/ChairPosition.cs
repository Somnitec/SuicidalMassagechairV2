using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChairPosition : NodeScriptFunction
{
    [InfoBox("0 is up and 1 is down")][Range(0,1)]
    public float NewPosition;

    public override List<string> Serialize()
    {
        return ToList($"chair_estimated_position:{(10000f * NewPosition)}");
    }
}