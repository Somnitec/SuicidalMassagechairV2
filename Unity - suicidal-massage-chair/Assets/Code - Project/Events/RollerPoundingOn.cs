﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class RollerPoundingOn : NodeScriptFunction
{
    public bool On;

    public override List<string> SerializeToJson()
    {
        return ToList(ToJson(
            "roller_pounding_on", BoolToString(On))
        );
    }
}