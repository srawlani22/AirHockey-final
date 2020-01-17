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
            dt.Interval = new TimeSpan(0, 0, 0, 1);
            dt.Tick += dt_tick;
            playBall = new Ball(canvas);
            
        }

        public void dt_tick(object obj, EventArgs e)
        {
            
            playBall.move();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dt.IsEnabled)
            {
                dt.IsEnabled = true;
                dt.Start();
            }
            else
            {
                dt.IsEnabled = false;
                dt.Stop();
            }
        }
    }
}
