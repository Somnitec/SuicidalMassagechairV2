void ledStates() {
  if (LEDSOn)
  {
    if (digitalRead(buttonSettings))
      analogWrite(LEDSettings, buttonBrightnessSettings);
    else
      analogWrite(LEDSettings, 0);

    if (digitalRead(buttonYes))
      analogWrite(LEDYes, buttonBrightnessSettings);
    else
      analogWrite(LEDYes, 0);

    if (digitalRead(buttonNo))
      analogWrite(LEDNo, buttonBrightnessSettings);
    else
      analogWrite(LEDNo, 0);
  }
  else
    for (int i = 0; i < ledAmount; i++)
      analogWrite(LEDs[i], 0);
}
