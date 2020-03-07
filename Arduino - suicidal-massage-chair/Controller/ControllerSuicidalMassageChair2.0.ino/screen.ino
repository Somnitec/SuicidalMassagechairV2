
void OnSetScreen() {
  String screenraw = "";//cmdMessenger.readStringArg();
  int screen = screenraw.toInt();
  String text = "";//cmdMessenger.readStringArg();
  if(text)  writeToScreen(screen, text);
  else writeToScreen(screen, " ");

}
const int characterwidth = 14;
//3,screen,text;
void writeToScreen(int screen, String text) {
  Serial.println(text);

  String text1 = text.substring(0 * characterwidth, 1 * characterwidth);
  String text2 = text.substring(1 * characterwidth, 2 * characterwidth);
  String text3 = text.substring(2 * characterwidth, 3 * characterwidth);
  String text4 = text.substring(3 * characterwidth, 4 * characterwidth);  
  String text5 = text.substring(4 * characterwidth, 5 * characterwidth);


  text1.toCharArray(charBuf1, bufsize) ;
  text2.toCharArray(charBuf2, bufsize) ;
  text3.toCharArray(charBuf3, bufsize) ;
  text4.toCharArray(charBuf4, bufsize) ;  
  text5.toCharArray(charBuf5, bufsize) ;

  //something broken with new oled library
  if (screen == 0)oledInit(OLED_128x64, 0, 0, 17, 16,-1, 400000L); // use standard I2C bus at 400Khz
  if (screen == 1)oledInit(OLED_128x64, 0, 0, 23, 22, -1,400000L); // use standard I2C bus at 400Khz
  if (screen == 2)oledInit(OLED_128x64, 0, 0, 18, 19,-1, 400000L); // use standard I2C bus at 400Khz
  oledFill(0, 1);
  if (screen > 3) {
    // clear the screens
    return;
  }
  int mod = 0;
  if (screen == 0)mod=-1;
  oledFill(0x0, 1);
  oledWriteString(0, 5, 2+mod, (char *)charBuf1, FONT_NORMAL, 0, 1);
  oledWriteString(0, 5, 3+mod, (char *)charBuf2, FONT_NORMAL, 0, 1);
  oledWriteString(0, 5, 4+mod, (char *)charBuf3, FONT_NORMAL, 0, 1);
  oledWriteString(0, 5, 5+mod, (char *)charBuf4, FONT_NORMAL, 0, 1);
  oledWriteString(0, 5, 6+mod, (char *)charBuf5, FONT_NORMAL, 0, 1);
  Serial.println(text1);
  Serial.println(text2);
  Serial.println(text3);
  Serial.println(text4);
  Serial.println(text5);
  //
  //cmdMessenger.sendCmd(kAcknowledge, "screen set");
  //delay(100);
}
