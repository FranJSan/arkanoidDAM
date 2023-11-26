using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private double Angulo;

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
            Angulo = 50 * Math.PI / 180;
            EstablecerVelocidadEjes();
        }

        public void SetAnguloGrados (double anguloGrados)
        {
            Angulo = anguloGrados * Math.PI / 180;
        }

        public void SetAnguloRad (double anguloRadianes)
        {
            Angulo = anguloRadianes;
        }

        public double GetAnguloGrados ()
        {
            return Angulo * 180 / Math.PI;
        }

        public double GetAnguloRad ()
        {
            return Angulo;
        }
        public void EstablecerVelocidadEjes()
        {
            velocidadX = speed * Math.Cos(Angulo);
            velocidadY = speed * Math.Sin(Angulo);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(ClientRectangle);
                Region = new Region(path);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(new Pen(ForeColor, 1), ClientRectangle);
            }
        }
    }
}
