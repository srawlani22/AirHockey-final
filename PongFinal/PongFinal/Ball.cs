/*
 * Final Project CS- 371
 * Authors: Sparsh Rawlani - Xavier Betancourt
 * Whitworth University
 * Last Modified on: 1/22/ 2020
 * 
 * Definition of the ball used in the game and its position in a 
 * 2-D coordinate system
 */
namespace PongFinal
{
    class Ball
    {
        private double XaxisPosition;
        private double YaxisPosition;   
        private bool RightDirection;    // Ball is going to the right (->)

        public double GetxPos
        {
            get { return XaxisPosition;}
            set { XaxisPosition = value; }
        }

        public double GetyPos
        {
            get { return YaxisPosition; }
            set { YaxisPosition = value; }
            
        }

        public bool GetRightDir
        {
            get { return RightDirection; }
            set { RightDirection = value; }
        }
    }
}
