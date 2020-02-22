﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AirPump : NodeScriptFunction
{
    public Boolean AirPumpOn;

    public override List<string> SerializeToJson()
    {
        return ToList(ToJson("airpump_on",BoolToString(AirPumpOn)));
    }
}