void sendCommand(String command, String variable){
  Serial.print(F("{\n\t\""));
  Serial.print(command);
  Serial.print(F("\":[\""));
  Serial.print(variable);
  Serial.println(F("\"]\n}"));
}
