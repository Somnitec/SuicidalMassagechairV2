/*
Protocol:

OUTPUT

{
  'buttonPressed':'...'
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




 IDEALLY:
 {
    'settings':{
        'button':['Settings'],
        'brightness':[255]
     }
 }
      

 
 */


#include <Bounce2.h>
#include <ss_oled.h>
#include <elapsedMillis.h>
#include <ArduinoJson.h>

#define buttonCustomA 0
#define buttonCustomB 1
#define buttonCustomC 2
#define buttonLanguage 3
#define LEDSettings 4
#define buttonSettings 5
#define buttonThumb 6
#define buttonRepeat 7
#define buttonNo 11
#define LEDNo 10
#define LEDYes 9
#define buttonYes 8
#define buttonHorn 14
#define buttonKill 15
#define sliderNumbers 24
#define buttonCross 25
#define volume0 21
#define volume1 20
#define sda0 18
#define scl0 19
#define sda1 23
#define scl1 22

//########SETTINGS
int debounceTime = 100;
int ledFadeTime = 100;
//######


int buttons[] = {
  buttonKill,
  buttonCustomA,
  buttonCustomB,
  buttonCustomC,
  buttonLanguage,
  buttonSettings,
  buttonThumb,
  buttonYes,
  buttonNo,
  buttonRepeat,
  buttonCross,
  buttonHorn,
};
String buttonsString[] = {"buttonKill",
                          "buttonCustomA",
                          "buttonCustomB",
                          "buttonCustomC",
                          "buttonLanguage",
                          "buttonSettings",
                          "buttonThumb",
                          "buttonYes",
                          "buttonNo",
                          "buttonRepeat",
                          "buttonCross",
                          "buttonHorn"
                         };
int buttonAmount = sizeof(buttons) / sizeof(buttons[0]);
int LEDs[] = {LEDSettings, LEDNo, LEDYes, 13};
int ledAmount = sizeof(buttons) / sizeof(buttons[0]);
Bounce *debouncedButtons = new Bounce[buttonAmount];
int lastSliderValue = 0;

boolean LEDSOn = true;


#define bufsize 250
char charBuf1[bufsize];
char charBuf2[bufsize];
char charBuf3[bufsize];
char charBuf4[bufsize];
char charBuf5[bufsize];
char szTemp[32];

void setup() {
  Serial.begin(9600);

  for (int i = 0; i < ledAmount; i++)
    pinMode(LEDs[i], OUTPUT);

  for (int i = 0; i < buttonAmount; i++)
  {
    debouncedButtons[i].attach(buttons[i], INPUT_PULLUP);
    debouncedButtons[i].interval(debounceTime);
  }

  pinMode(sliderNumbers, INPUT);

  writeToScreen(0, "...." );
  writeToScreen(1, "....."  );
  writeToScreen(2, "..." );

  pinMode(13, OUTPUT);
  digitalWrite(13, HIGH);


}

void loop() {
  // put your main code here, to run repeatedly:

}

int sliderConversion(int input)
{
  //1 730
  //    695
  //2 660
  //    570
  //3 480
  //    407
  //4 335
  //    293
  //5 250
  if (input > 695)
    return 1;
  else if (input > 570)
    return 2;
  else if (input > 407)
    return 3;
  else if (input > 293)
    return 4;
  else
    return 5;
}
