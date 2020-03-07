bool readingMessage = false;
String readString;
unsigned int maxStringLength = 64;
String last_command = "";

void readSerial() {
  if (Serial.available())  {
    char c = Serial.read();  //gets one byte from serial buffer
    if (c == '{' && !readingMessage)readingMessage = true;
    if (readingMessage) {
      if (c == '}') {  //looks for end of data packet marker
        readString += c;
        receiveMessage(readString); //prints string to serial port out
        //do stuff with captured readString
        readString = ""; //clears variable for new input
        readingMessage = false;
      }
      else {
        readString += c; //makes the string readString
        if (readString.length() > maxStringLength) {//preventing buffer overflow
          readString = "";
          readingMessage = false;
          printError("overflow");
        }
      }

    }
  }
}

void printError(String error) {
  Serial.print(F("{\n\t\"error\":\""));
  Serial.print(error);
  Serial.println(F("\"\n}"));
}


bool validateInput(String command, int expectedArguments) {
  if ( expectedArguments == 0) return false;//always get one argument (for now)
  else {
    for (int i = 0; i < expectedArguments; i++) {
      String item = doc[command][i];
      if (item.equals("null"))return false;
    }
  }
  last_command = command;
  return true;
}

void receiveMessage( String message) {
  last_command = "";

  DeserializationError error = deserializeJson(doc, message);
  // Test if parsing succeeds.
  if (error) {
    printError(error.c_str());
    return;
  }




  if (validateInput(F("test"), 1)) {
    sendCommand("test","received");
  }
}
