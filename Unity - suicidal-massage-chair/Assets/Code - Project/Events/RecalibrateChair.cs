﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RecalibrateChair : NodeScriptFunction
{
    public override List<string> Serialize()
    {
        return ToList("recalibrate");
    }
}