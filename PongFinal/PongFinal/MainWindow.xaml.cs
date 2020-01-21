using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Media;
using System.Net.Mime;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using MessageBox = System.Windows.MessageBox;

namespace PongFinal
{
    
    public partial class MainWindow : Window
    {
        private Paddle myPaddle = new Paddle();
        DispatcherTimer timer;
        MediaPlayer player = new MediaPlayer();

        private string name1, name2;
        //double input;


        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            MessageBox.Show("Enter the two names in the empty boxes below!");
            PlaySound();
            DataContext = myPaddle;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += dt_tick;
        }

        private double BallAngle = 155;
        private double BallSpeed = 15;
        private int PadSpeed = 50;

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
                if (myPaddle.p1ScoreCount > 11)
                {
                    changeBallSpeed();
                   
                }
                ResetBall();
                winnerDecider();
            }

            if (myPaddle.BallXPos <= -10)
            {
                myPaddle.p2ScoreCount += 1;
                if (myPaddle.p2ScoreCount > 11)
                {
                    changeBallSpeed();
                }
                ResetBall();
                winnerDecider();
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
            if (pos > (int) MainCanvas.ActualHeight - 100)
                pos = (int) MainCanvas.ActualHeight - 100;

            return pos;
        }

        private void PlaySound()
        {
            var uri = new Uri(@"bgmusic.wav", UriKind.RelativeOrAbsolute);
            player.Open(uri);
            player.Play();
            player.MediaEnded += reStartMusic;
        }

        private void reStartMusic(object sender, EventArgs e)
        {
            player.Position = TimeSpan.Zero;
            player.Play();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            name1 = text1.Text;
            name2 = text2.Text;
            if (timer.IsEnabled == true)
            {
                //timer.IsEnabled = false;
                timer.Stop();
                button.Content = "Start";
            }
            else
            {
                //timer.IsEnabled = true;
                timer.Start();
                button.Content = "Pause";
            }
        }

        public void changeBallSpeed()
        {
            if (BallSpeed == 15)
            {
                BallSpeed = 30;
                ball.Fill = Brushes.CornflowerBlue;
                ball.Effect = new BlurEffect();
            }

        }

        public void winnerDecider()
        {
            if (myPaddle.p1ScoreCount == 25)
            {
                MessageBox.Show( name1 + " Wins, Click OK to exit the game");
                timer.Stop();
            }
            else if (myPaddle.p2ScoreCount == 25)
            {
                MessageBox.Show(name2 + " Wins, Click OK to exit the game");
                timer.Stop();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            MessageBox.Show("IF YOU REALLY WANNA RE-START THEN CLICK OK");
            MessageBox.Show("Click Start to start again");
            ResetBall();

            myPaddle.p1ScoreCount = 0;
            myPaddle.p1ScoreCount = 0;

            if (myPaddle.BallXPos >= MainCanvas.ActualWidth - 10)
            {
                myPaddle.p1ScoreCount += 1;
                if (myPaddle.p1ScoreCount > 11)
                {
                    changeBallSpeed();

                }
                ResetBall();
                winnerDecider();
            }

            if (myPaddle.BallXPos <= -10)
            {
                myPaddle.p2ScoreCount += 1;
                if (myPaddle.p2ScoreCount > 11)
                {
                    changeBallSpeed();
                }
                ResetBall();
                winnerDecider();
            }
        }

        
    }
}
