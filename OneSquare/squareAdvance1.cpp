const byte bSensor = IN_4;
const byte lSensor = IN_1;
const byte rSensor = IN_2;

const byte rMotor  = OUT_A;
const byte lMotor  = OUT_B;

const int Black1 = 20;
const int Black2 = 20;
const int Black4 = 20;


const long sqUnit    = 630;
const long lTurn    = 266.25; // 0.425 * 630 - 90 degree turn
const long revTurn  = 535.5 ; // 0.85  * 630 - 180 degree turn
  
const int lMotorBias = 3;
const int rMotorBias = 0;
int moveDir = -100;
long lRot, rRot = 0;

bool SensorLeft = false;
bool SensorRight = false;
bool SensorBack = false;

void setupNXT(){
     SetSensorLight(bSensor);
	 SetSensorLight(lSensor);
	 SetSensorLight(rSensor);
	 ResetTachoCount(OUT_AB);
}

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

void followLine(long pwr = 40){
  int bufferRight = 32-Sensor(IN_1);
  int buffeLeft   = 34-Sensor(IN_2);
  if (moveDir ==0){
  
     OnFwd(OUT_A, pwr + bufferRight);
     OnFwd(OUT_B, pwr-3 + buffeLeft);
  }
  else if (moveDir == -1){
       OnRev(OUT_A, pwr + bufferRight);
       OnRev(OUT_B, pwr-3 + buffeLeft);
  }
}


bool readRotation(long thresh){
	lRot = labs(MotorRotationCount(OUT_A));
	rRot = labs(MotorRotationCount(OUT_B));
	
	if ((lRot > thresh) && (rRot > thresh)){
    return true;
    }
  else
      return false;
}

void smallAdvance(){
     ResetRotationCount(OUT_AB);
     bool finish = false;
     powerMotors(0,50);
     while(!finish){
        finish = readRotation(200);
     }
}

bool turnStatus(){

  if(Sensor(IN_1) < Black1){SensorLeft = true;} //A
  if(Sensor(IN_2) < Black2){SensorRight = true;} //B
  if(Sensor(IN_4) < Black4){SensorBack = true;} //C

  if(SensorLeft + SensorRight + SensorBack > 1){
    Off(OUT_AB);
    SensorLeft = false;
    SensorRight = false;
    SensorBack = false;
    return true;
  }
  else
	return false;
}

void lTurn(){
  bool finish = false;
  powerMotors(1, 50);
  Wait(200);
  while(!finish){
	finish = turnStatus();
  }
}
void oneSquareV2(){
	   powerMotors(0, 75);
     Wait(200);
     bool finished = false;
     while(!finished){
      followLine(75);
      if((Sensor(lSensor) < 20) && (Sensor(rSensor) < 20) ){
       finished = true;
      } else
        finished = readRotation(730);
      }
        ResetRotationCount(OUT_AB);
        Wait(1);
        smallAdvance();
      }

task main(){
	setupNXT();
  oneSquareV2();

 Off(OUT_AB);
}
