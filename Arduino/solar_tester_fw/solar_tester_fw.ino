
#include <LiquidCrystal.h>
const int rs = 13, en = 12, d4 = 11, d5 = 10, d6 = 9, d7 = 8;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

const int pass_led = 5, curr_en = 7; 
#define   adc_vref        A0
#define   adc_sol_volts   A1
#define   adc_sol_amps    A2
#define   adc_trimpot     A6


bool dataReq = false;
bool conn = false;

uint32_t millisLast = 0;
uint32_t mtick = 0;

const int aveCount = 50;
uint32_t amps[aveCount];
uint32_t ampAve;
uint32_t volts = 1000;

uint32_t thresh = 24;

void setup() {
  pinMode(pass_led, OUTPUT);
  pinMode(curr_en, OUTPUT);
  Serial.begin(115200);
  lcd.begin(16, 2);
  lcd.setCursor(0, 0);
  lcd.print("  GALCOM SOLAR");
  lcd.setCursor(0,1);
  lcd.print("     TESTER   ");
  
}

void loop() {
  
  mtick = millis();
  if (millisLast == mtick) {return;}
  millisLast = mtick;
  
  
  if (1) {
    checkSerial();
  }

  if (!(mtick%30)){
    updateAmps();
    updateVolts();
    sendAmps();
  }
  if (!(mtick%200)) {
    updateLCD();
  }

  if (!(mtick%2000)) {
    updateThresh();
  }

}

void updateVolts()
{
  disableCurrent();
  for (int i = 0; i<20; i++) {analogRead(adc_sol_volts);} //flush adc
  uint32_t v=0;
  for (int i = 0; i<10; i++) {  //average 10 readings
    v += analogRead(adc_sol_volts);
  }  
  v/=10;
  //do the math
  v = map(v, 0, 1024, 0, 2500); //convert 10-bit value to 2.5volts
  v *= (499+20+1000) / (499.0+20.0);   //adjust for voltage divider (+ leakeage from FET)
  volts = v; 
  enableCurrent();
}

void updateLCD()
{
  if (conn) {
    digitalWrite(pass_led,LOW);
    lcd.clear();
    lcd.setCursor(0,0);
    lcd.print("   CONNECTED  ");
    lcd.setCursor(0,1);
    lcd.print("    to PC!  ");
    return;
  } else if (volts <= 2) {
    lcd.clear();
    lcd.setCursor(0,0);
    lcd.print("   NO SOLAR   ");
    lcd.setCursor(0,1);
    lcd.print("   CONNECTED");
    return;
  }

  
  lcd.setCursor(0,0);
  lcd.print(" -SOLAR TESTER- ");
  lcd.setCursor(0,1);
  lcd.print((ampAve+5)/10);
  
  lcd.print(" /");
  lcd.print(thresh);
  lcd.print("mA    ");

  if (((ampAve+5))/10 >= thresh) {
    digitalWrite(pass_led, HIGH);
    lcd.setCursor(11,1);
    lcd.print("PASS");
  } else {
    digitalWrite(pass_led, LOW);
    lcd.setCursor(11,1);
    lcd.print("FAIL");
  }
  
}

void sendAmps()
{
  if (!conn) {return;}
  if (!dataReq) {return;}

  if (volts <= 2) {
    Serial.print('!');
    Serial.print(0);
    Serial.print('%');
    Serial.print(0);
    Serial.print('$');
    dataReq = false;
  }

  Serial.print('!');
  Serial.print(ampAve);
  Serial.print('%');
  Serial.print(ampAve);
  Serial.print('$');
  dataReq = false;
  
}

uint32_t updateThresh()
{
  analogReference(DEFAULT); //use 5v line
  for (int i = 0; i<10; i++) {analogRead(adc_trimpot);} //flush adc
  thresh = analogRead(adc_trimpot);
  thresh = map(thresh, 0, 1023, 100, 0);  //user adjustable from 0mA - 100mA
  thresh /= 2;
  thresh *= 2; //step by 2
  analogReference(EXTERNAL);
  for (int i = 0; i<100; i++) {analogRead(adc_sol_amps);}  //flush adc
}

void updateAmps()
{
  for (int i = (aveCount-1); i>0; i--) { amps[i] = amps[i-1];}
  amps[0] = getAmps();
  ampAve = 0;
  for (int i = 0; i<aveCount; i++) {ampAve += amps[i];}
  ampAve = ampAve / (aveCount/10);
  //if (ampAve <= 1) {ampAve = 0;}
}

uint32_t getAmps()
{
  //analogReference(EXTERNAL);
  enableCurrent();

  uint32_t a = 0;
  for (int i = 0; i<50; i++) {analogRead(adc_sol_amps);}  //flush adc
  for (int i = 0; i<50; i++) {  //average 10 readings
    a += analogRead(adc_sol_amps);
  }
  a /= 50;

  //do the math
  a = map(a, 0, 1024, 0, 2500); //convert 10-bit value to 2.5volts
  a /= 10;    //convert to amps
  a -= ((0.06 * a) - 1.36); //adjust for FET leakage
  return (a);
}

void checkSerial()
{
  if (!Serial.available()) {return;}
  char c = Serial.read();

  if (c == 'n') {
    dataReq = true;
    conn = true;
    
  } else if (c == 'x') {
    dataReq = false;
    conn = false;
    
  }
}

void enableCurrent()
{
  digitalWrite(curr_en, HIGH);
}

void disableCurrent()
{
  digitalWrite(curr_en, LOW);
}
