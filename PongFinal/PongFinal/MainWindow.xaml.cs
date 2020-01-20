using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PongFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Paddle myPaddle = new Paddle();
        DispatcherTimer timer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = myPaddle;

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
            timer.Tick += dt_tick;
        }

        private double BallAngle = 155;
        private double BallSpeed = 10;
        private int PadSpeed = 30;

        public void dt_tick(object sender, EventArgs e)
        {
            if (myPaddle.BallYPos <= 0)
            
                BallAngle = BallAngle + (180 - 2 * BallAngle);
            

            if (myPaddle.BallYPos >= MainCanvas.ActualHeight - 20)
            
                BallAngle = BallAngle + (180 - 2 * BallAngle);
            

            if (CheckBallCollision())
            {
                CheckBallAngle();
                myPaddle.ChangeBallDir();
            }

            double Rad = (Math.PI / 180) * BallAngle;
            Vector myVec = new Vector{ X = Math.Sin(Rad), Y = -Math.Cos(Rad)};
            myPaddle.BallXPos += myVec.X * BallSpeed;
            myPaddle.BallYPos += myVec.Y * BallSpeed;

            if (myPaddle.BallXPos >= MainCanvas.ActualWidth - 10)
            {
                myPaddle.p1ScoreCount += 1;
                ResetBall();
            }

            if (myPaddle.BallXPos <= -10)
            {
                myPaddle.p2ScoreCount += 1;
                ResetBall();
            }
        }

        public void ResetBall()
        {
            myPaddle.BallXPos = 380;
            myPaddle.BallYPos = 210;
        }

        public void CheckBallAngle()
        {
            if (myPaddle.IsDirRight)
            {
                BallAngle = 270 - ((myPaddle.BallYPos + 10) - (myPaddle.p2Pad + 40));
            }
            else
            {
                BallAngle = 90 - ((myPaddle.BallYPos + 10) - (myPaddle.p1Pad + 40));
            }
        }

        public bool CheckBallCollision()
        {
            if (myPaddle.IsDirRight)
            {
                return myPaddle.BallXPos >= 760 &&
                       (myPaddle.BallYPos > myPaddle.p2Pad - 20 && myPaddle.BallYPos < myPaddle.p2Pad + 80);
            }
            else
            {
                return myPaddle.BallXPos <= 20 &&
                       (myPaddle.BallYPos > myPaddle.p1Pad - 20 && myPaddle.BallYPos < myPaddle.p1Pad + 80);
            }
        }
        
        private void MainWindow_OnKeyDown(object sender, KeyboardEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W)) myPaddle.p1Pad = checkBounds(myPaddle.p1Pad, -PadSpeed);
            if (Keyboard.IsKeyDown(Key.S)) myPaddle.p1Pad = checkBounds(myPaddle.p1Pad, PadSpeed);

            if (Keyboard.IsKeyDown(Key.Up)) myPaddle.p2Pad = checkBounds(myPaddle.p2Pad, -PadSpeed);
            if (Keyboard.IsKeyDown(Key.Down)) myPaddle.p2Pad = checkBounds(myPaddle.p2Pad, PadSpeed);
        }
        
        public int checkBounds(int pos, int change)
        {
            pos += change;

            if (pos < 0)
                pos = 0;
            if (pos > (int) MainCanvas.ActualHeight - 90)
                pos = (int) MainCanvas.ActualHeight - 90;

            return pos;
        }

       
    }
}
