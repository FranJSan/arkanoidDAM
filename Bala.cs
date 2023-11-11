using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arkanoid
{
    class Bala : Label
    {
        private double speed;
        public double Speed
        {
            set
            {
                speed = value;
            }
            get
            {
                return speed;
            }
        }

        private double velocidadX;
        public double VelocidadX
        {
            set
            {
                this.velocidadX = value;
            }
            get
            {
                return velocidadX;
            }
        }

        private double velocidadY;
        public double VelocidadY
        {
            set
            {
                velocidadY = value;
            }
            get
            {
                return velocidadY;
            }
        }
        
        
            
        public Bala()
        {
            AutoSize = false;
            Width = 20;
            Height = 20;
            TextAlign = ContentAlignment.MiddleCenter;
            BorderStyle = BorderStyle.None;
            BackColor = Color.Red;
            speed = 10;
            EstablecerVelocidadEjes();
        }

        private void EstablecerVelocidadEjes()
        {
            velocidadX = speed * Math.Cos(60 * Math.PI / 180);
            velocidadY = speed * Math.Sin(60 * Math.PI / 180);
        }
    }
}
