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


namespace AirHockey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ball playBall;
        Paddle p1, p2;
        DispatcherTimer dt;
        private int scoreP1 = 0;
        private int scoreP2 = 0;

        public MainWindow()
        {
            InitializeComponent();
            /*ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"sampleImages\berries.jpg", UriKind.Relative));
            canvas.Background = ib;*/
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0,30);
            //dt.Start();
            dt.IsEnabled = false;
            dt.Tick += new EventHandler(dt_tick);
            playBall = new Ball(canvas);
            p1 = new Paddle(canvas,15);
            p2 = new Paddle(canvas,915);
            
        }

        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                p1.GoUp();
            }

            if (e.Key == Key.S)
            {
                p1.GoDown();
            }

            if (e.Key == Key.Up)
            {
                p2.GoUp();
            }

            if (e.Key == Key.Down)
            {
                p2.GoDown();
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                p1.Stop();
            }

            if (e.Key == Key.S)
            {
                p1.Stop();
            }

            if (e.Key == Key.Up)
            {
                p2.Stop();
            }

            if (e.Key == Key.Down)
            {
                p2.Stop();
            }

        }

        public void dt_tick(object obj, EventArgs e)
        {
            playBall.move();
            p1.Move();
            p2.Move();

            if (playBall.width <= 0)
            {
                playBall.Xrebound();
                scoreP2++;
                player2.Content = scoreP2;
            }

            if (playBall.width >= 914)
            {
                playBall.Xrebound();
                scoreP1++;
                player1.Content = scoreP1;
            }

            if (playBall.height <= 0 || playBall.height >= 510)
            {
                playBall.Yrebound();
            }

            if (((playBall.width) <= (p1.xaxis + p1.width)) && (playBall.height >= p1.yaxis) && (playBall.height <= (p1.yaxis + p1.height)))
            {
                playBall.Xrebound();
            }

            if (((playBall.width + playBall.size) >= p2.xaxis) && (playBall.height >= p2.yaxis) && (playBall.height <= (p2.yaxis + p2.height)))
            {
                playBall.Xrebound();
            }

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (dt.IsEnabled)
            {
                dt.IsEnabled = false;
                Start.Content = "Start!";
                //dt.Stop();
            }
            else
            {
                dt.IsEnabled = true;
                Start.Content = "Pause!";
                //dt.Start();
            }
        }



       
    }
}
