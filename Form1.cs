using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*
 * todo: mejorar el rebote
 *       intentar quitar la vibración
 *       mejorar estética
 *       implementar fin bala / partida
 *       
 */
namespace arkanoid
{
    public partial class FrmMain : Form 
    {
        public static bool reset = false;
        private Pieza[] piezas;
        private Nave LblNave;
        private Bala LblBala;
        private bool gameOver = false;

        public FrmMain()
        {
            InitializeComponent();
            panelPiezas.MouseMove += new MouseEventHandler(SeguirMouse);
            panelLimite.MouseMove += new MouseEventHandler(SeguirMouse);
            
            panelPiezas.Padding = new Padding(300);
            CrearNave();
            CrearPiezas();
            CrearBala();
            DibujarVidas();
            TimerMain.Start();            
        }

        private void SeguirMouse(object sender, MouseEventArgs e)
        {
            // Ha sido necesario sacar el punto para un calculo pasable.
            Point posMouse = this.PointToClient(MousePosition);
            Point centroLabel = new Point(LblNave.Location.X + LblNave.Width / 2, LblNave.Location.Y);
            int xMouse = posMouse.X;
            int xNave = centroLabel.X;

            if (Math.Abs(xMouse - xNave) <= 1) return;

            int velocidad;
            if (Math.Abs(xMouse - xNave) < LblNave.Velocidad) velocidad = Math.Abs(xMouse - xNave);
            else velocidad = LblNave.Velocidad;

            // dirección
            if (LblNave.Location.X <= 5) LblNave.Location = new Point(6, LblNave.Location.Y);
            if (LblNave.Location.X >= panelPiezas.Width - LblNave.Width - 5) LblNave.Location = new Point(panelPiezas.Width - LblNave.Width - 5, LblNave.Location.Y);
            if (xMouse > xNave)
            { 
                LblNave.Location = new Point(LblNave.Location.X + velocidad, LblNave.Location.Y);
            } 
            else
            {
                LblNave.Location = new Point(LblNave.Location.X - velocidad, LblNave.Location.Y);
            }
        }

        public void CrearPiezas()
        {
            int size = 30;
            int piezasFila = 10;
            int filas = size / piezasFila;
            piezas = new Pieza[size];
            for (int i = 0; i < piezas.Length; i++)
            {
                piezas[i] = new Pieza();               
                
                int fila = i / piezasFila;
                int columna = i % piezasFila;

                piezas[i].Top = fila * (piezas[i].Height + 20) + 50;
                piezas[i].Left = columna * (piezas[i].Width + 15) + 40;

                piezas[i].Visible = true;
                panelPiezas.Controls.Add(piezas[i]); 
            }
        }

        public void CrearNave()
        {
            LblNave = new Nave();
            LblNave.Left = this.Width / 2 - LblNave.Width / 2;
            panelLimite.Controls.Add(LblNave);
        }

        public void CrearBala()
        {
            LblBala = new Bala();
            LblBala.Location = new Point(this.Width / 2, this.Height / 2);
            panelPiezas.Controls.Add(LblBala);
            
        }
        private void DibujarVidas()
        {
            if (LblNave.Vidas == 0) return;
           //BorrarVidas();
            for (int i = 0; i < LblNave.Vidas; i++)
            {
                Vida vida = new Vida();
                vida.Top = 10;
                vida.Left = 10 + ((vida.Width + 20) * i);
                panelPiezas.Controls.Add(vida);
            }
        }
        private void BorrarVidas()
        {         
            foreach (Vida vida in panelPiezas.Controls.OfType<Vida>().ToList())
            {
                vida.Visible = false;
                panelPiezas.Controls.Remove(vida);
            }                        
        }

        private void TimerMain_Tick(object sender, EventArgs e)
        {
            ColisionPieza();
            MoverBala();
        }

        private void ResetGame()
        {
            if (LblNave.Vidas > 0 && !gameOver)
            {
                LblNave.Vidas --;
                BorrarVidas();
                CrearBala();
                DibujarVidas();
            }
            else
            {
                FrmGameOver FrmGO = new FrmGameOver();
                TimerMain.Stop();
                FrmGO.ShowDialog();
                TimerMain.Start();
                
                if (reset)
                {
                    panelLimite.Controls.Clear();
                    panelPiezas.Controls.Clear();
                    CrearNave();
                    CrearPiezas();
                    CrearBala();
                    DibujarVidas();
                    TimerMain.Start();
                    reset = false;
                }
                
            }            
        }

        private void MoverBala()
        {
            // Colisión techo
            if (LblBala.Location.Y <= 0)
            {
                LblBala.VelocidadY = -LblBala.VelocidadY;
            }

            // Colisión paredes
            if (LblBala.Location.X + LblBala.Width >= panelPiezas.Width || LblBala.Location.X <= 0)
            {
                LblBala.VelocidadX = -LblBala.VelocidadX;
            }

            // Colisión nave
            CalcularColisionBala(LblNave);

            // Fin de juego
            if (LblBala.Location.Y > panelPiezas.Height)
            {
                ResetGame();
                gameOver = true;
            }
            
        }

        private void CalcularColisionBala(Label label)
        {
            // Colisión Nave
            Rectangle nave = label.Bounds;
            Rectangle bala = LblBala.Bounds;
            Point supIzq = panelLimite.PointToClient(panelPiezas.PointToScreen(bala.Location));
            Rectangle transBala = new Rectangle(supIzq, bala.Size);

            if (nave.IntersectsWith(transBala))
            {
                // Con esto a veces la bala hace cosas raras por el centro de la nave
                // double relativeIntersection = (nave.X - bala.X + (bala.Width / 2));

                double relativeIntersection = (nave.X - bala.X);

                double newAngle = (relativeIntersection / (nave.Width / 2)) * Math.PI / 4;

                LblBala.VelocidadX = LblBala.Speed * Math.Cos(newAngle);
                LblBala.VelocidadY = LblBala.Speed * Math.Sin(newAngle);
            }

            LblBala.Location = new Point((int)(LblBala.Location.X + LblBala.VelocidadX), (int)(LblBala.Location.Y + LblBala.VelocidadY));
        }

        private bool IsEmptyPiezas()
        {
            return (panelPiezas.Controls.OfType<Pieza>().ToList().Count() == 0) ? true : false;
        }

        private void ColisionPieza()
        {
            for (int i = 0; i < piezas.Length; i++) 
            {
                if (piezas[i].Enabled == false) continue;
                Rectangle rectBala = LblBala.Bounds;
                Rectangle rectPieza = piezas[i].Bounds;
                if (rectBala.IntersectsWith(rectPieza))
                {
                    if (piezas[i].Location.X == LblBala.Location.X)
                    {
                        panelPiezas.Controls.Remove(piezas[i]);
                        piezas[i].Enabled = false;
                        // CalcularColisionBala(piezas[i]);
                        LblBala.VelocidadX = -LblBala.VelocidadX;
                    }
                    else
                    {
                        panelPiezas.Controls.Remove(piezas[i]);
                        piezas[i].Enabled = false;
                        // CalcularColisionBala(piezas[i]);
                        LblBala.VelocidadY = -LblBala.VelocidadY;
                    }
                    
                }
            }

            if (IsEmptyPiezas())
            {
                gameOver = true;
                TimerMain.Stop();
                ResetGame();
            }
        }

        
    }
}
