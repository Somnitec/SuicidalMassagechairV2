﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class RollerKneadingSpeed : NodeScriptFunction
{
    [InfoBox("0 is off 255 is full speed")] [Range(0, 1f)]
    public float Speed = 0f;

    public override List<string> Serialize()
    {
        return ToList("roller_kneading_speed:" + Speed * 255);
    }
}