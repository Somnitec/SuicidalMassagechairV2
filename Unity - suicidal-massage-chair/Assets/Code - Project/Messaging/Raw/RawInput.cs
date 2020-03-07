namespace Messaging.Raw
{
    public class RawInput
    {
        public string controllerCommand;
        public string controllerValue; 
    }

    public enum RawButtons
    {
        buttonKill,
        buttonCustomA,
        buttonCustomB,
        buttonCustomC,
        buttonSettings,
        buttonThumb,
        buttonYes,
        buttonNo,
        buttonRepeat,
        buttonCross,
        buttonHorn,
    }

    public enum LanguageStick
    {
      Up,
      Down
    }
}
/*
OUTPUT

{
  'buttonPressed':'...'
}

{
  'controllerCommand':'...'
  'controllerValue':"...' 0..1-5
}

{
  'buttonPressed':"buttonKill'
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
  languageSet
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
  
{
  '':['...']
}
 1 - 5

  
{
  'languageSet':['...']
}
 up, down

INPUT

{
  'customScreenA':['...']
}
 string

 ... A, B, C


{ 
  'clearScreens':[]
}

{
  'buttonBrightnessSettings':[..]
}
 int 0-255
 Settings, No, Yes

 {
  'buttonFadeTimeSettings':[..]
}
 int
 Settings, No, Yes

{
  'buttonBounceTime':[..]
}
 int 




*/