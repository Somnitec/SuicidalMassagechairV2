using UnityEngine;

namespace Messaging.Raw
{
    public class RawInput
    {
        public string controllerCommand;
        public string controllerValue;
    }

    
}
/*
OUTPUT
{
  "controllerCommand":"..."
  "controllerValue':"..." 0..1-5
}

InputParse
  buttonKill, => bool
  buttonCustomA, => bool
  buttonCustomB, => bool
  buttonCustomC, => bool
  buttonSettings,  => bool
  buttonThumbUp,
  buttonThumbDown,
  buttonYes,
  buttonNo,
  buttonRepeat,
  buttonHorn,
  buttonLanguage
  slider => int 1..5 
  
InfoParse  
  customScreenA => string
  customScreenB => string
  customScreenC => string
  clearScreen = bool
  buttonBounceTime = int in MS
  buttonFadeTimeSettings = int in MS
  buttonBrightnessSettings = 0 .. 255
  allLeds = bool
  settingsLed = bool
  yesLed  = bool
  noLed = bool
  reset
  
  For example
  {
  	"controllerCommand": "buttonKill",
  	"controllerValue": "1"
  }
  
  {
  	"controllerCommand": "customScreenA",
  	"controllerValue": "Screen blablabalal"
  }

*/