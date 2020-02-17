using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "NodeScript/ChairPosition")]
public class ChairPosition : NodeScriptFunction
{
    public float Duration;
    public ChairPositionDirection Direction;
}

public enum ChairPositionDirection
{
    Up,
    Down
}