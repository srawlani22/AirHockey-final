/*
 * Final Project CS- 371
 * Main Window 
 * Authors: Sparsh Rawlani - Xavier Betancourt
 * Whitworth University
 * Last Modified on: 1/22/ 2020
 */
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Effects;
using MessageBox = System.Windows.MessageBox;

namespace PongFinal
{ 
    public partial class MainWindow : Window
    {
        private Paddle myPaddle = new Paddle();
        DispatcherTimer timer;                                      // time in ticks
        MediaPlayer player = new MediaPlayer();                     // Background music during the game
        private string NameOfPlayer1, NameOfPlayer2;
<<<<<<< HEAD
        private double BallAngle = 155;                             // Starting degree angle 
        private double BallSpeed = 20;                              // Starting speed of ball
        private int PadSpeed = 75;                                  // Starting speed of paddle
        private string PathToScores = "BestScoresPlayers.txt";      // To append results to the file
        
=======
        private double BallAngle = 155;                 // Starting degree angle 
        private double BallSpeed = 20;                  // Starting speed of ball
        private int PadSpeed = 75;                      // Starting speed of paddle


>>>>>>> b7fc8ffaae58a5d3f12dc3c87da00f7ca652d50b
        public MainWindow()
        {
            InitializeComponent();  
            timer = new DispatcherTimer();                          // Updates the content each time tick
            MessageBox.Show("Enter the two names in the empty boxes below!");
            PlaySound();
            DataContext = myPaddle;
            timer.Interval = TimeSpan.FromMilliseconds(10);        // set the interval of the elapsed event
            timer.Tick += dt_tick;                                 // Increment the time ticks
        }

        // Set the W and S keyboards to be the ones that contorl the left paddle and Up and Down arrows to control the right paddle
        private void MainWindow_OnKeyDown(object sender, KeyboardEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W)) myPaddle.P1PadPosition = checkBounds(myPaddle.P1PadPosition, -PadSpeed);
            if (Keyboard.IsKeyDown(Key.S)) myPaddle.P1PadPosition = checkBounds(myPaddle.P1PadPosition, PadSpeed);

            if (Keyboard.IsKeyDown(Key.Up)) myPaddle.P2PadPosition = checkBounds(myPaddle.P2PadPosition, -PadSpeed);
            if (Keyboard.IsKeyDown(Key.Down)) myPaddle.P2PadPosition = checkBounds(myPaddle.P2PadPosition, PadSpeed);
        }

        // Reset the ball to its original position when a player scores or resets, in the middle
        public void ResetBall()
        {
            myPaddle.BallXPos = 370;
            myPaddle.BallYPos = 240;
        }

        // Check the angle of the ball depending on where it's located in the coordinate system
        public void CheckBallAngle()
        {
            if (myPaddle.IsDirRight)
            {
                BallAngle = 270 - ((myPaddle.BallYPos + 10) - (myPaddle.P2PadPosition + 40)); 
            }
            else
            {
                BallAngle = 90 - ((myPaddle.BallYPos + 10) - (myPaddle.P1PadPosition + 40));
            }
        }

        // Make the ball collide making it go to the right or left once it hits the paddle 
        public bool CheckBallCollision()
        {
            if (myPaddle.IsDirRight)
            {
                return myPaddle.BallXPos >= 760 &&
                       (myPaddle.BallYPos > myPaddle.P2PadPosition - 20 && myPaddle.BallYPos < myPaddle.P2PadPosition + 80);
            }
            else
            {
                return myPaddle.BallXPos <= 20 &&
                       (myPaddle.BallYPos > myPaddle.P1PadPosition - 20 && myPaddle.BallYPos < myPaddle.P1PadPosition + 80);
            }
        }

        // Change the speed of the ball once the score from a player gets to 7 and then to 15, also change the colors
        // to notice the difference visually
        public void changeBallSpeed()
        {
            if (BallSpeed == 20)
            {
                BallSpeed = 25;
                ball.Fill = Brushes.GreenYellow;
                ball.Effect = new BlurEffect();
            }         
        }
        public void changeBallSpeedAgain()
        {
            if (BallSpeed == 25)
            {
                BallSpeed = 30;
                ball.Fill = Brushes.Goldenrod;
                ball.Effect = new BlurEffect();
            }
        }
        

        // Decide which players wins based on who got to the 25th point first and stop the game instantly
        public void WinnerDecider()
        {
            if (myPaddle.p1ScoreCount == 25)
            {
                MessageBox.Show(NameOfPlayer1 + " Wins, Click OK to exit the game");

                // Append the score of players and their names to a file (just the winner)
                using (StreamWriter sw = File.AppendText(PathToScores))
                {
                    sw.WriteLine(NameOfPlayer1);
                    sw.WriteLine(myPaddle.p1ScoreCount - myPaddle.p2ScoreCount);
                }
                    timer.Stop();            
            }

            else if (myPaddle.p2ScoreCount == 25)
            {
                MessageBox.Show(NameOfPlayer2 + " Wins, Click OK to exit the game");

                using (StreamWriter sw = File.AppendText(PathToScores))
                {
                    sw.WriteLine(NameOfPlayer2);
                    sw.WriteLine(myPaddle.p2ScoreCount - myPaddle.p1ScoreCount);
                }

                timer.Stop();            
            }      
        }

        // Implementation of collision and angle methods each time tick according to the coordinate system and its boundaries
        // (size of the windows and location of the paddles)
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

            // we store the deviation of the ball that is calculated using the ball angle
            double Rad = (Math.PI / 180) * BallAngle;
            Vector myVec = new Vector { X = Math.Sin(Rad), Y = -Math.Cos(Rad)};// the sin and cosine values of the deviation are used to redirect the ball in a different direction using the x and y position of the ball
            myPaddle.BallXPos += myVec.X * BallSpeed;
            myPaddle.BallYPos += myVec.Y * BallSpeed;


            if (myPaddle.BallXPos >= MainCanvas.ActualWidth - 10)   // right boundary in the game, once ball passes this points, a point is considered
            {
                myPaddle.p1ScoreCount += 1;
                
                // Change the speed of ball and color once score passes 7
                if (myPaddle.p1ScoreCount > 7 && myPaddle.p1ScoreCount <= 15)
                {
                    changeBallSpeed();            
                }

                // Change again after 16 pts
                if (myPaddle.p1ScoreCount >= 16)
                {
                    changeBallSpeedAgain();
                }
                ResetBall();
                WinnerDecider();
            }

            if (myPaddle.BallXPos <= -10) // left boundary in the game
            {
                myPaddle.p2ScoreCount += 1;
                if (myPaddle.p2ScoreCount > 7 && myPaddle.p2ScoreCount <= 15)
                {
                    changeBallSpeed();
                }
                if (myPaddle.p2ScoreCount >= 16)
                {
                    changeBallSpeedAgain();
                }
                ResetBall();
                WinnerDecider();
            }
        }          
        
        // we check the upper and lower bounds based on the height of the canvas
        public int checkBounds(int pos, int change)
        {
            pos += change;// we add the position to change so that it gets counted everytime the position changes

            if (pos < 0)
                pos = 0;
            if (pos > (int) MainCanvas.ActualHeight - 100)
                pos = (int) MainCanvas.ActualHeight - 100;

            return pos;
        }      

        // START- PUSE Button and boxes that store the names of the player in the game
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NameOfPlayer1 = text1.Text;
            NameOfPlayer2 = text2.Text;
            
            // Stop the game and change the label to 'START'
            if (timer.IsEnabled == true)
            {              
                timer.Stop();
                button.Content = "START";
            }
            // Pause the game and change the label to 'STOP'
            else
            {
                timer.Start();
                button.Content = "PAUSE";
            }
        }


        // EXIT Button 
        // Shutdowns the application when hit
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        // RESTART Button
        // Set the scores of both players to zero, center the ball, set the speed and color to the first definition
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            timer.Stop();                                           // Pause the game
            MessageBox.Show("Click Start to start again");

            ResetBall();                                            // Center the ball in the middle again
            BallSpeed = 20;                                         // First definition of ball speed
            ball.Fill = Brushes.White;                              // First definition of ball color
            myPaddle.p1ScoreCount = 0;
            myPaddle.p2ScoreCount = 0;

            if (myPaddle.BallXPos >= MainCanvas.ActualWidth - 10)
            {
                myPaddle.p1ScoreCount += 1;
                if (myPaddle.p1ScoreCount > 7 && myPaddle.p1ScoreCount <= 15)
                {
                    changeBallSpeed();

                }

                if (myPaddle.p1ScoreCount >= 16)
                {
                    changeBallSpeedAgain();
                }
                ResetBall();
                WinnerDecider();
            }

            if (myPaddle.BallXPos <= -10)
            {
                myPaddle.p2ScoreCount += 1;
                if (myPaddle.p2ScoreCount > 7 && myPaddle.p2ScoreCount <= 15)
                {
                    changeBallSpeed();
                }
                if (myPaddle.p2ScoreCount >= 16)
                {
                    changeBallSpeedAgain();
                }
                ResetBall();
                WinnerDecider();
            }
        }

        // Control the Background Music
        private void PlaySound()
        {
            var uri = new Uri(@"bgmusic.wav", UriKind.RelativeOrAbsolute); // Path to the .wav file with the song to be used
            player.Open(uri);                                              // Open the path and then play
            player.Play();
            player.MediaEnded += ReStartMusic;                             // Restart song when it gets to the end, i.e. loop the song
        }

        private void ReStartMusic(object sender, EventArgs e)
        {
            player.Position = TimeSpan.Zero;                                // Song starting from the beginning and playing again 
            player.Play();
        }


    }
}
