using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Vida : System.Windows.Forms.Label
    {

        public Vida()
        {
            AutoSize = false;
            Width = 30;
            Height = 5;
            TextAlign = ContentAlignment.MiddleCenter;
            BackColor = Color.Green;
        }
    }
}
