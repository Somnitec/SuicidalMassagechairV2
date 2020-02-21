using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class BackLightColor : NodeScriptFunction
{
    public Color Color;

    public override List<string> Serialize()
    {
        return ToList(
            "backlight_color:" + ColorUtility.ToHtmlStringRGB(Color)
        );
    }
}