using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class AirBag : NodeScriptFunction
{
    public AirBagFlag AirBagsOn;
    public AirBagFlag AirBagsOff;
    private string on = "on";
    private string off = "off";

    public override List<string> Serialize()
    {
        var on = ToStringList(AirBagsOn, true);
        var off = ToStringList(AirBagsOff, false);
        on.AddRange(off);
        return on;
    }

    private List<string> ToStringList(AirBagFlag flag, bool on)
    {
        List<string> airbags = new List<string>();

        if (flag.HasFlag(AirBagFlag.Shoulders))
            airbags.Add(AddArgument("airbag_shoulders_on", on));
        if (flag.HasFlag(AirBagFlag.Arms))
            airbags.Add(AddArgument("airbag_arms_on", on));
        if (flag.HasFlag(AirBagFlag.Legs))
            airbags.Add(AddArgument("airbag_legs_on", on));
        if (flag.HasFlag(AirBagFlag.Outside))
            airbags.Add(AddArgument("airbag_outside_on", on));

        return airbags;
    }

    private string AddArgument(string param, bool on)
    {
        return $"{param}:{BoolToString(on)}";
    }
}

[Flags]
public enum AirBagFlag
{
    Shoulders = (1 << 0),
    Arms = (1 << 1),
    Legs = (1 << 2),
    Outside = (1 << 3),
}