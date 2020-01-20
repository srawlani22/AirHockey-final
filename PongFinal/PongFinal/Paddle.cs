using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongFinal
{
    class Paddle : INotifyPropertyChanged 
    {
        private int p1PadPosition = 160;
        private int p2PadPosition = 160;
        private int p1Score = 0;
        private int p2Score = 0;
        private Ball playBall = new Ball {GetxPos = 380, GetyPos = 210, GetRightDir = true};

        public int p1Pad
        {
            get { return p1PadPosition; }
            set
            {
                p1PadPosition = value;
                OnPropertyChanged("p1Pad");
            }
        }

        public int p2Pad
        {
            get { return p2PadPosition; }
            set
            {
                p2PadPosition = value;
                OnPropertyChanged("p2Pad");
            }
        }

        public int p1ScoreCount
        {
            get { return p1Score; }
            set
            {
                p1Score = value;
                OnPropertyChanged("p1ScoreCount");
            }
        }

        public int p2ScoreCount
        {
            get { return p2Score; }
            set
            {
                p2Score = value;
                OnPropertyChanged("p2ScoreCount");
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

        public void ChangeBallDir()
        {
            IsDirRight = !IsDirRight;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
