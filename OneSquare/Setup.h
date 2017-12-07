#ifndef SETUP_H_
#define SETUP_H_
#include "Setup.c"

//---------------- Generic Functions ----------------//
void setupNXT();
//void Executor(string& readCmdList);
void getPath(string& filename, string& PathLoc);

//---------------- Motor Functions ----------------//
void Turn(bool turnRight);
void revSquare();
void oneSquare();
void smallAdvance();
bool readRotation(int thresh);
int powerMotors(int dir, char pwr);

 //---------------- Sensor Functions ----------------//
bool turnStatus();
void followLine(int pwr);

#endif SETUP_H_