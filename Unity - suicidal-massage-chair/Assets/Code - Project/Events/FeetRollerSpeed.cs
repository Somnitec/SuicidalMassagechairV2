using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class FeetRollerSpeed : NodeScriptFunction
{
    [InfoBox("0 is off 1 is full speed")] [Range(0, 1f)]
    public float Speed = 0f;

    public override List<string> SerializeToJson()
    {
        return ToList(ToJson(
            "feet_roller_speed", (int)(Speed * 255))
        );
    }
}