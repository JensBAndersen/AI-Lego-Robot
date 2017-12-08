bool turnSens(){
  if(Sensor(lSensor) < Black1){SensorLeft = true;} //A
  if(Sensor(rSensor) < Black2){SensorRight = true;} //B
  if(Sensor(bSensor) < Black4){SensorBack = true;} //C

  while(true){
    dispSens();
    if(SensorLeft){
     if (SensorRight){
        SensorLeft = false;
        SensorRight = false;
        SensorBack = false;
        break;
     }
    }
    else
    if(SensorRight){
     if (SensorLeft){
        SensorLeft = false;
        SensorRight = false;
        SensorBack = false;
        break;
     }
    }
  }
	    return true;
}