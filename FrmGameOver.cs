using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arkanoid
{
    public partial class FrmGameOver : Form
    {
        public FrmGameOver()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FormBorderStyle = FormBorderStyle.None;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reiniciar
            FrmMain.reset = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            //Application.Exit();
        }
    }
}
