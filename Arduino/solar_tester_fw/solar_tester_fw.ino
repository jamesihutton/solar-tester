// include the library code:
#include <LiquidCrystal.h>

// initialize the library by associating any needed LCD interface pin
// with the arduino pin number it is connected to
const int rs = 13, en = 12, d4 = 11, d5 = 10, d6 = 9, d7 = 8;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

const int pass_led = 5, curr_en = 7; 


#define   adc_vref        A0
#define   adc_sol_volts   A1
#define   adc_sol_amps    A2
#define   adc_trimpot     A6

uint32_t pass_thresh = 0;
bool pass = false;
bool pass_led_state = false;
bool serial_connected = false;
void setup() {
  //set adc reference to external vref (2.5v)
  analogReference(EXTERNAL);
  
  // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  lcd.setCursor(0, 0);
  pinMode(pass_led, OUTPUT);
  pinMode(curr_en, OUTPUT);

  disableCurrent();

  Serial.begin(9600);
}




void loop() {
  togglePassLed();
  
  //re-check the trimpot for pass threshold
  updatePassThresh();

  checkSerial();
  
  lcd.setCursor(0,0);
  float v = checkVolts();

  //if 0V... not connected....
  /*
  if (v <= 0.1) {
    pass = false;
    lcd.clear();
    lcd.setCursor(0,0);
    lcd.print("      NOT");
    lcd.setCursor(0,1);
    lcd.print("   CONNECTED");
    return;
  }
  */
  
  lcd.print(v);
  lcd.print("Volts");
  togglePassLed();
  checkSerial();
  lcd.setCursor(0,1);
  uint32_t mA = checkAmps();
  lcd.print(mA);
  lcd.print("/");
  lcd.print(pass_thresh);
  lcd.print("mA         ");

  //CHECK IF PASS
  if (mA >= pass_thresh){
    pass = true;
    lcd.setCursor(11, 1);
    lcd.print("PASS!");
    digitalWrite(pass_led, HIGH);
  } else {
    lcd.setCursor(11, 1);
    lcd.print("FAIL!");
    digitalWrite(pass_led, LOW);
    pass = false;
  }
  

}

void checkSerial()
{
  if (Serial.available()) {
    char c = Serial.read();
    if (c == '*') {
      Serial.print('!');
      Serial.print(pass_thresh);
      serial_connected = true;
      lcd.clear();
      lcd.setCursor(0,0);
      lcd.print("   CONNECTED  ");
      lcd.setCursor(0,1);
      lcd.print("    to PC!  ");
      
      //stream amps forever (until disconnected and unplugged)
      analogReference(EXTERNAL);
      enableCurrent();
      while(1){
       
        //get adc reading
        uint32_t a = 0;
      
        for (int i = 0; i<10; i++) {  //average 1000 readings
          a += analogRead(adc_sol_amps);
        }
        a /= 10;
      
        //do the math
        a = map(a, 0, 1024, 0, 2500); //convert 10-bit value to 2.5volts
        a /= 10;    //convert to amps
        a -= ((0.06 * a) - 1.36); //adjust for FET leakage
        Serial.print('!');
        Serial.print(a);
        Serial.print('%');
        Serial.print(a);
        Serial.print('$');
        delay(25);
      }
      
    }
  }
}


void togglePassLed()
{
  if (!pass) {digitalWrite(pass_led, LOW); pass_led_state = 0;}
  else {
    if (pass_led_state) {digitalWrite(pass_led, LOW); pass_led_state = 0;}
    else {digitalWrite(pass_led, HIGH); pass_led_state = 1;}
  }
}

void updatePassThresh()
{
  analogReference(DEFAULT); //use 5v line
  delay (100);  //settle
  for (int i = 0; i<10; i++) {analogRead(adc_trimpot);} //flush adc
  pass_thresh = analogRead(adc_trimpot);
  pass_thresh = map(pass_thresh, 0, 1023, 100, 0);  //user adjustable from 0mA - 100mA
  pass_thresh /= 2;
  pass_thresh *= 2; //step by 2
}


//Check the open-circuit voltage on panel
//Return: voltage as float
float checkVolts()
{
  analogReference(EXTERNAL);
  disableCurrent();
  //delay(2000); //wait for line to settle

  //get adc reading
  uint32_t v = 0;
  
  for (int i = 0; i<10; i++){   //flush out ADC
    delay(1);
    analogRead(adc_sol_volts);
  }

  for (int i = 0; i<1000; i++) {  //average 1000 readings
    v += analogRead(adc_sol_volts);
  }
  v /= 1000;

  //do the math
  v = map(v, 0, 1024, 0, 2500); //convert 10-bit value to 2.5volts
  v *= (499+20+1000) / (499.0+20.0);   //adjust for voltage divider (+ leakeage from FET)
    
  return (v/1000.0); //convert from mV to V and return float
}


//Check the closed-circuit current on panel
//Return: amps as float
uint32_t checkAmps()
{
  analogReference(EXTERNAL);
  enableCurrent();
  //delay(2000); //wait for line to settle

  //get adc reading
  uint32_t a = 0;
  
  for (int i = 0; i<10; i++){   //flush out ADC
    delay(1);
    analogRead(adc_sol_amps);
  }

  for (int i = 0; i<1000; i++) {  //average 1000 readings
    a += analogRead(adc_sol_amps);
  }
  a /= 1000;

  //do the math
  a = map(a, 0, 1024, 0, 2500); //convert 10-bit value to 2.5volts
 
  a /= 10;    //convert to amps
  a -= ((0.06 * a) - 1.36); //adjust for FET leakage
  return (a);
}




void enableCurrent()
{
  digitalWrite(curr_en, HIGH);
}

void disableCurrent()
{
  digitalWrite(curr_en, LOW);
}
