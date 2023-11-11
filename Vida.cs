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
            Width = 20;
            Height = 20;
            TextAlign = ContentAlignment.MiddleCenter;
            BackColor = Color.Yellow;
        }
    }
}
