using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResetChair : NodeScriptFunction
{
    public override List<string> SerializeToJson()
    {
        return ToList(ToJson("reset",-1));
    }
}