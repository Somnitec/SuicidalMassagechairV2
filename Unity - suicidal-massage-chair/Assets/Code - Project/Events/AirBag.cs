using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "NodeScript/Airbag")]
public class AirBag : NodeScriptFunction
{
    public AirBagFlag AirBagsOn;
    public AirBagFlag AirBagsOff;
}

[Flags]
public enum AirBagFlag
{
    Shoulders = (1 << 0),
    Arms = (1 << 1),
    Legs = (1 << 2),
    Outside = (1 << 3),
}