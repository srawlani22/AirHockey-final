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

        DispatcherTimer dt;

        public MainWindow()
        {
            InitializeComponent();
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0,100);
            //dt.Start();
            dt.IsEnabled = false;
            dt.Tick += dt_tick;
            playBall = new Ball(canvas);
            
        }

        public void dt_tick(object obj, EventArgs e)
        {
            playBall.move();
            if (playBall.width <= 0)
            {
                playBall.Xrebound();
            }

            if (playBall.width >= 730)
            {
                playBall.Xrebound();
            }

            if (playBall.height <= 0 || playBall.height >= 295)
            {
                playBall.Yrebound();
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
