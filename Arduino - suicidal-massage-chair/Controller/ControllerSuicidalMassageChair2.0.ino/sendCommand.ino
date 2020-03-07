void sendCommand(String command, String variable){
  Serial.print(F("{\n\t\"controllerCommand\":\""));
  Serial.print(command);
  Serial.print(F("\",\n\t\"controllerValue\":\""));
  Serial.print(variable);
  Serial.println(F("\"\n}"));
}
