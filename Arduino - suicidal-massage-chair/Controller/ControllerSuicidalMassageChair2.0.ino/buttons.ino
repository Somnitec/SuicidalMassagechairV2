elapsedMillis sliderTimer;
bool sliderMoved = false;

void readButtons() {
  for (int i = 0; i < buttonAmount; i++)
  {
    // Update the Bounce instance :
    debouncedButtons[i].update();
    if (i == 0)
    { //kill is NC
      if (debouncedButtons[i].rose())
      {
        //Serial.print(buttonsString[i]);
        //Serial.println();
        sendCommand( buttonsString[i], true);
      }
    }
    else if (i == 4)
    { //language is switch
      if (debouncedButtons[i].rose() || debouncedButtons[i].fell())
      {
        //Serial.print(buttonsString[i]);
        //Serial.print(" = ");
        //Serial.print(debouncedButtons[i].read());
        //Serial.println();
        sendCommand( buttonsString[i], debouncedButtons[i].read());

      }
    }
    else if (debouncedButtons[i].fell())
    {
      //Serial.print(buttonsString[i]);
      //Serial.println();
      sendCommand( buttonsString[i], true);
      //cmdMessenger.sendCmd(kSendInput, "triggered");
    }
  }
  

  int newSliderValue = sliderConversion(analogRead(sliderNumbers));
  if (lastSliderValue != newSliderValue)
  {
    sliderMoved = true;
    sliderTimer = 0;
    lastSliderValue = newSliderValue;
    //Serial.print("sliderNumbers = ");
    //Serial.print(newSliderValue);
    //Serial.println();
  }
  if (sliderTimer > debounceTime && sliderMoved) {
    sliderMoved = false;
    //cmdMessenger.sendCmdStart(kSendInputValue);
    //cmdMessenger.sendCmdArg("sliderNumbers");
    sendCommand( "ButtonSlider", newSliderValue);
    //    cmdMessenger.sendCmdStart(kSendInput);
    //    cmdMessenger.sendCmdArg(newSliderValue);
    //    cmdMessenger.sendCmdEnd();
  }

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
