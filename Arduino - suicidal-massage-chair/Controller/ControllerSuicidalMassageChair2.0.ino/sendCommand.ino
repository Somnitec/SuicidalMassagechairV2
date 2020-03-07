void sendCommand(String command, String variable){
  Serial.print(F("{\n\t\"controllerCommand\":\""));
  Serial.print(command);
  Serial.print(F("\",\n\t\"controllerCommand\":\""));
  Serial.print(variable);
  Serial.println(F("\"\n}"));
}
