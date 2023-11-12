using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Nave : System.Windows.Forms.Label
    {
        private int velocidad;
        public int Velocidad
        {
            get
            {
                return velocidad;
            }
            set
            {
                velocidad = value;
            }
        }

        private int vidas;
        public int Vidas
        {
            get
            {
                return vidas;
            }
            set
            {
                vidas = value;
            }
        }

        public Nave()
        {
            Size = new Size(90, 15);
            BackColor = Color.Green;
            Visible = true;
            Velocidad = 5;
            vidas = 2;
        }

        public void AumentarVelocidad(int value)
        {
            Velocidad += value;
        }
    }
}
