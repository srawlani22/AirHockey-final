using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Converters;
using System.Windows.Shapes;

namespace AirHockey
{
    class Ball
    {
        Ellipse ellip = new Ellipse();
        
        public int width { get; set; }
        public int height { get; set; }

        public int size { get; set; }

        int mov = 5;

        public int xaxis, yaxis;

        public Ball(Canvas c)
        {
            SolidColorBrush BallColor = new SolidColorBrush();
            width = 80;
            height = 80;
            size = 40;
            xaxis = mov;
            yaxis = mov;
            BallColor.Color = Color.FromRgb(255, 0, 0);

            ellip.Fill = BallColor;
            ellip.Width = size;
            ellip.Height = size;
            Canvas.SetTop(ellip, height);
            Canvas.SetLeft(ellip,width);
            c.Children.Add(ellip);
        }

        public void move()
        {
            width += xaxis;
            height += yaxis;
            Canvas.SetTop(ellip,height);
            Canvas.SetLeft(ellip,width);
        }

        public void Xrebound()
        {
            if (xaxis == 5)
            {
                xaxis = -5;
            }
            else
            {
                xaxis = 5;
            }
        }

        public void Yrebound()
        {
            if (yaxis == 5)
            {
                yaxis = -5;
            }
            else
            {
                yaxis = 5;
            }

        }
    }
}
