/*
 * Definition of the paddle that both players use in the game and its position
 * in the 2-D coordinate system
 */
using System;
using System.ComponentModel;

namespace PongFinal
{
    class Paddle : INotifyPropertyChanged 
    {
        private int p1PadPosition = 160;
        private int p2PadPosition = 160;
        private int p1Score = 0;
        private int p2Score = 0;
        private Ball playBall = new Ball {GetxPos = 380, GetyPos = 210, GetRightDir = true};

        public int P1PadPosition
        {
            get { return p1PadPosition; }
            set
            {
                p1PadPosition = value;
                OnPropertyChanged("P1PadPosition");
            }
        }

        public int P2PadPosition
        {
            get { return p2PadPosition; }
            set
            {
                p2PadPosition = value;
                OnPropertyChanged("P2PadPosition");
            }
        }

        public int p1ScoreCount
        {
            get { return p1Score; }
            set
            {
                p1Score = value;
                OnPropertyChanged("p1ScoreCount"); // Label that shows the score of player 1
            }
        }

        public int p2ScoreCount
        {
            get { return p2Score; }
            set
            {
                p2Score = value;
                OnPropertyChanged("p2ScoreCount"); // Label that shows the score of player 2
            }
        }

        public double BallXPos
        {
            get { return playBall.GetxPos; }
            set
            {
                playBall.GetxPos = value;
                OnPropertyChanged("BallXPos");
            }
        }

        public double BallYPos
        {
            get { return playBall.GetyPos; }
            set
            {
                playBall.GetyPos = value;
                OnPropertyChanged("BallYPos");
            }
        }

        public bool IsDirRight
        {
            get { return playBall.GetRightDir; }
            set
            {
                playBall.GetRightDir = value;
                OnPropertyChanged("IsDirRight");
            }
        }

        // If the ball is going the right, once it hits the paddle it should go to the left, including a beep
        // to differentiate
        public void ChangeBallDir()
        {
            IsDirRight = !IsDirRight;
            Console.Beep(500, 40);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
