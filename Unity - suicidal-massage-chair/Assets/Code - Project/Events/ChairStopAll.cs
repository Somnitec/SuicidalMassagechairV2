using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ChairStopAll : NodeScriptFunction
{
    public override List<string> SerializeToJson()
    {
        return ToList(ToJson("stopall",-1));
        //we should do this manually instead, there is no stopall function in the microcontroller
    }
}