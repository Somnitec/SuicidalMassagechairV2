
#include <Arduino.h>
#include <U8g2lib.h>

#ifdef U8X8_HAVE_HW_SPI
#include <SPI.h>
#endif
#ifdef U8X8_HAVE_HW_I2C
#include <Wire.h>
#endif
long randNumber0;
long randNumber1;
long randNumber2;

U8G2_SH1106_128X64_NONAME_1_SW_I2C OLED0(U8G2_R0, /* clock=*/ 16, /* data=*/ 17, /* reset=*/ U8X8_PIN_NONE);   // All Boards without Reset of the Display
U8G2_SH1106_128X64_NONAME_1_SW_I2C OLED1(U8G2_R0, /* clock=*/ 22, /* data=*/ 23, /* reset=*/ U8X8_PIN_NONE);   // All Boards without Reset of the Display
U8G2_SH1106_128X64_NONAME_1_SW_I2C OLED2(U8G2_R0, /* clock=*/ 19, /* data=*/ 18, /* reset=*/ U8X8_PIN_NONE);   // All Boards without Reset of the Display

void setup() {
  Serial.begin(115200);
  OLED0.begin();
  OLED1.begin();
  OLED2.begin();
  // put your setup code here, to run once:

}
#define STARTSPACE 11
#define LINESPACE 13
void draw0(void) {

  OLED0.setFont(u8g2_font_courB12_tr   );
  //65 characters
  OLED0.drawStr( 0, 0*LINESPACE+STARTSPACE, "0123456789012");
  OLED0.drawStr( 0, 1*LINESPACE+STARTSPACE, "1234567890123");
  OLED0.drawStr( 0, 2*LINESPACE+STARTSPACE, "2345678901234");
  OLED0.drawStr( 0, 3*LINESPACE+STARTSPACE, "3456789012345");
  OLED0.drawStr( 0, 4*LINESPACE+STARTSPACE, "4567890123456");  
  //OLED0.setFont(u8g_font_6x10);
  OLED0.setCursor(1, 40);
  OLED0.print(randNumber0);
}


void draw1(void) {

  OLED1.setFont(u8g2_font_courB12_tr    );
  OLED1.drawStr( 0, 0*LINESPACE+STARTSPACE, "So lets put s");
  OLED1.drawStr( 0, 1*LINESPACE+STARTSPACE, "ome nice litt");
  OLED1.drawStr( 0, 2*LINESPACE+STARTSPACE, "le text on he");
  OLED1.drawStr( 0, 3*LINESPACE+STARTSPACE, "re for everyo");
  OLED1.drawStr( 0, 4*LINESPACE+STARTSPACE, "ne to read!  ");  
  //OLED1.setFont(u8g_font_unifont);
  OLED1.setCursor(1, 40);
  OLED1.print(randNumber1);
}
void draw2(void) {
  OLED2.setFont(u8g_font_6x10);
  OLED2.drawStr( 0, 22, "DISPLAY 2");
  //OLED2.setFont(u8g2_font_7x14B_mr);
  OLED2.setCursor(1, 40);
  OLED2.print(randNumber2);
}
void loop(void) {
  // put your main code here, to run repeatedly:

  randNumber0 = random(0, 255);
  randNumber1 = random(0, 255);
  randNumber2 = random(0, 255);


  OLED0.firstPage();
  do {
    draw0();
  } while ( OLED0.nextPage() );


  OLED1.firstPage();
  do {
    draw1();
  } while ( OLED1.nextPage() );
  OLED2.firstPage();
  do {
    draw2();
  } while ( OLED2.nextPage() );

  delay(100);
}
