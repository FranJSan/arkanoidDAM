using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Pieza : System.Windows.Forms.Label
    {

        public Pieza()
        {
            AutoSize = false;            
            BackColor = Color.FromArgb(192, 64, 0);
            Size = new Size(40, 15);
        }
    }
}
