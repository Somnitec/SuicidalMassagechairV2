﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class ButtVibration : NodeScriptFunction
{
    public bool On;

    public override List<string> SerializeToJson()
    {
        return ToList(ToJson("butt_vibration_on" + BoolToString(On)));
    }
}