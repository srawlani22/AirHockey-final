using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AirHockey
{
    class Paddle
    {
        Rectangle paddle = new Rectangle();
        public int width, height, xaxis, yaxis;
        public int Ydir = 0;

        public Paddle(Canvas c, int input)
        {
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromRgb(0, 0, 255);
            paddle.Fill = color;
            this.xaxis= input; 
            yaxis = 80;// paddle only moves on the y-axis 
            width = 30; // w
            height = 130; // h
            paddle.Width = width;
            paddle.Height = height;
            Canvas.SetLeft(paddle,this.xaxis);
            Canvas.SetTop(paddle,yaxis);
            c.Children.Add(paddle);
        }

        public void Move()
        {
            yaxis += Ydir;
            Canvas.SetTop(paddle,yaxis);
        }

        public void GoUp()
        {
            
            if (yaxis <=0)
            {
                Ydir = 0;
            }
            else
            {
                Ydir = -20;
            }

        }

        public void GoDown()
        {
            if (yaxis >= 434)
            {
                Ydir = 0;
            }
            else
            {
                Ydir = 20;
            }
        }

        public void Stop()
        {
            Ydir = 0;
        }
    }
}
