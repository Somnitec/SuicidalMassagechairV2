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

}
