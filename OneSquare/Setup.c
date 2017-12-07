#include "Setup.h" 
#include "NXCDefs.h"

// Port Constants
const byte bSensor = IN_4;
const byte lSensor = IN_1;
const byte rSensor = IN_2;
const byte canSensor = IN_3;

const byte rMotor  = OUT_A;
const byte lMotor  = OUT_B;


// Light Sensor Constants
const int Black1 = 25;
const int Black2 = 25;
const int Black4 = 35;

// Motor Constants

const int lSenLimit = 25;
const int rSenLimit = 25;
const int bSenLimit = 35;

const long sqUnit    = 630;
const long lhTurn    = 266.25; // 0.425 * 630 - 90 degree turn
const long revTurn  = 535.5 ; // 0.85  * 630 - 180 degree turn

const int lMotorBias = 3;
const int rMotorBias = 0;
//---------------- Generic Functions ----------------//

void setupNXT(){
     SetSensorLight(bSensor);
	   SetSensorLight(lSensor);
	 SetSensorLight(rSensor);
	 SetSensorTouch(canSensor);
	 ResetRotationCount(OUT_AB);
	 
}

void getPath(string& filename, string& PathLoc){
    byte handle;
    string inString;
    handle = fopen(filename , "r" );
    TextOut(0, LCD_LINE1, filename);
    if (handle != NULL)
    {
      ReadLnString(handle, PathLoc);
      TextOut(0, LCD_LINE2, "Commands Read:" );
      TextOut(0, LCD_LINE3, PathLoc);
    } else{
      TextOut(0, LCD_LINE1, "ERROR!");
      }
    fclose(handle);
}
void dispSens(){
	int lSenVal = Sensor(lSensor);
	int rSenVal = Sensor(rSensor);
	int bSenVal = Sensor(bSensor);

  TextOut(0, LCD_LINE1, "L Sensor: ");
  TextOut(0, LCD_LINE2, "R Sensor: ");
  TextOut(0, LCD_LINE3, "B Sensor: ");

  NumOut(60, LCD_LINE1, lSenVal);
  NumOut(60, LCD_LINE2, rSenVal);
  NumOut(60, LCD_LINE3, bSenVal);

 }

//---------------- Motor Functions ----------------//
int powerMotors(int dir, char pwr){
	if (moveDir != dir){
		Off(OUT_AB);
		ResetRotationCount(OUT_AB);
		moveDir = dir;
		lRot = 0;
		rRot = 0;
	}
	switch (dir){
		case -1: //backwards
			OnRev(rMotor, pwr-rMotorBias);
			OnRev(lMotor, pwr-lMotorBias);
     return dir;
     break;
		case 0:	//forwards
      OnFwd(rMotor, pwr-rMotorBias);
			OnFwd(lMotor, pwr-lMotorBias);
     return dir;
     break;
		case 1:	//left
			OnRev(rMotor, pwr-rMotorBias);
			OnFwd(lMotor, pwr-lMotorBias);
     return dir;
     break;
		case 2: //right
			OnFwd(rMotor, pwr-rMotorBias);
			OnRev(lMotor, pwr-lMotorBias);
			break;

		default:
			Off(OUT_AB);
     return dir;
     break;
	}
}

void Turn(bool turnRight = false){
	bool finish = false;
	bool found = false;
	int rotSpeed = 50;

	ResetRotationCount(OUT_AB);
	Wait(1);
	if (!turnRight)
		powerMotors(2, rotSpeed); //rotate left
	else{
		powerMotors(1, rotSpeed); // turn Right
	}
	Wait(300);

	while(!finish){
		if(readRotation(400))
			finish  = true;

		found = turnStatus();
		if (found)
			finish = true;
	}

	if (!found){			// If the line has not been found, rotate the other direction until it is found
		finish = false;
		ResetRotationCount(OUT_AB);
		rotSpeed = 30;
		Wait(1);

		if (!turnRight)		// turn opposite direction at a slower pace
			powerMotors(1, rotSpeed); //rotate right
		else
			powerMotors(2, rotSpeed); // turn left

		while(!finish){
			if(readRotation(400))
				finish  = true;
			found = turnStatus();
			if (found)
				finish = true;
		}
	}
	if (found){       //if found, power motors briefly in opposite direction to compensate for momentum
		if (!turnRight)
			powerMotors(1, rotSpeed); //rotate right
		else
			powerMotors(2, rotSpeed); // turn left
		Wait(5);
		Off(OUT_AB);
	}else{
	if (!turnRight)		// turn opposite direction at a slower pace
		powerMotors(2, rotSpeed); //rotate right
	else
		powerMotors(1, rotSpeed); // turn left
	Wait(100);
	Off(OUT_AB);
  Wait(100);
	}
 }
void revSquare(){
     ResetRotationCount(OUT_AB);
     Wait(1);
     bool finished = false;
       powerMotors(-1, 75);
       Wait(1000);
	while(!finished){
		followLine(75);
		if((Sensor(lSensor) < 20) && (Sensor(rSensor) < 20) ){
			finished = true;
		}else{
			finished = readRotation(730);
     }
	}
  smallAdvance();
	Off(OUT_AB);
}
void oneSquare(){  // true = fwds, false = back
     ResetRotationCount(OUT_AB);
     bool canPresent = Sensor(canSensor);
     bool finished = false;
     
     powerMotors(0, 75);
     Wait(200);
     
	if(!canPresent){
	while(!finished){
		followLine(75);
		if((Sensor(lSensor) < 20) && (Sensor(rSensor) < 20) ){
			finished = true;
		}else{
			finished = readRotation(730);
		}
	}
	smallAdvance();
	Off(OUT_AB);
	Wait(1);
	}
  else {
		PlayTone(500, 500);
		while(!finished){
			followLine(75);
			finished = readRotation(330);
		}
		Off(OUT_AB);
	}
}
void smallAdvance(){
     ResetRotationCount(OUT_AB);
     Wait(1);
     bool finish = false;
     powerMotors(0,50);
     while(!finish){
        finish = readRotation(200);
     }

}
bool readRotation(int thresh){
	lRot = labs(MotorRotationCount(OUT_A));
	rRot = labs(MotorRotationCount(OUT_B));

	if ((lRot > thresh) && (rRot > thresh)){
    return true;
    }
  else
      return false;
}

 //---------------- Sensor Functions ----------------//
 
bool turnStatus(){
  if(Sensor(IN_1) < Black1){SensorLeft = true;} //A
  if(Sensor(IN_2) < Black2){SensorRight = true;} //B
  if(Sensor(IN_4) < Black4){SensorBack = true;} //C



//  if(SensorLeft + SensorRight + SensorBack > 1){
    //if(SensorLeft + SensorRight > 1){
      if((SensorLeft || SensorRight) && SensorBack ){
    Off(OUT_AB);
    SensorLeft = false;
    SensorRight = false;
    SensorBack = false;
    return true;
  }
  else
	return false;
}
void followLine(int pwr = 40){
  int bufRight  = 40-Sensor(IN_1);
  int bufLeft   = 40-Sensor(IN_2);

  if (moveDir ==0){

       int lSpeed = ((pwr-lMotorBias)+ ((bufLeft-bufRight)*lineFollowBias));
       int rSpeed = ((pwr-rMotorBias)+ ((bufRight-bufLeft)*lineFollowBias));

      OnFwd(rMotor, rSpeed);
			OnFwd(lMotor, lSpeed);
  }
  else if (moveDir == -1){
       int lSpeed = ((pwr+lMotorBias)+bufLeft)*lineFollowBias;
       int rSpeed = ((pwr+rMotorBias)+bufRight)*lineFollowBias;
       
       OnRev(lMotor, lSpeed);
       OnRev(rMotor, rSpeed);
  }
}


