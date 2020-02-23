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

    public override List<string> SerializeToJson()
    {
        int[] color = new[] { (int)(Color.r * 255), (int)(Color.g * 255), (int)(Color.b * 255)};
        return ToList(ToJson("backlight_color", color));
    }
}