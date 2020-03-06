namespace Messaging.Raw
{
    public class RawInput
    {
        public RawButtons buttonPressed;
        public int slider;
        public LanguageStick languageSet;
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
  'buttonPressed':"buttonKill'
}


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

  
{
  'slider':['...']
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