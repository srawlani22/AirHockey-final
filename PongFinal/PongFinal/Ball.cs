using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongFinal
{
    class Ball
    {
        private double xPos;
        private double yPos;
        private bool RightDir;


        public double GetxPos
        {
            get { return xPos;}
            set { xPos = value; }
        }

        public double GetyPos
        {
            get { return yPos; }
            set { yPos = value; }
            
        }

        public bool GetRightDir
        {
            get { return RightDir; }
            set { RightDir = value; }
        }
    }
}
