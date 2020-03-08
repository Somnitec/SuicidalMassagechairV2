using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChairPosition : NodeScriptFunction
{
    // 0 is down and 1 is up
   [Range(0,1)]
    public float NewPosition;
    
    public ChairPosition() { }

    public ChairPosition(float newPosition)
    {
        this.NewPosition = newPosition;
    }

    public override List<string> SerializeToJson()
    {
        return ToList(ToJson("chair_position_target",(int)(10000f * NewPosition)));
    }
}