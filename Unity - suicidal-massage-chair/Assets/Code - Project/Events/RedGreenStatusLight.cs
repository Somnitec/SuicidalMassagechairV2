using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using static ChairMicroControllerState;

public class RedGreenStatusLight : NodeScriptFunction
{
    public StatusLight Status = StatusLight.Green;

    public override List<string> SerializeToJson()
    {
        return ToList(ToJson(
            "redgreen_statuslight", ChairMessageParser.ConvertFromRedGreen(Status))
        );
    }
}