﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*
 * todo: mejorar el rebote con la nave y con los bloques
 *       en oocasiones da la casualidad de que la bala rebota indefinidamente entre las dos paredes
 *       mejorar estética
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
            LblNave.Top = panelPiezas.Width - LblNave.Height - 100;
            panelPiezas.Controls.Add(LblNave);
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
        private void BorrarVida()
        {
            foreach (Vida vida in panelPiezas.Controls.OfType<Vida>().ToList())
            {
                panelPiezas.Controls.Remove(vida);
            }
        }

        private void TimerMain_Tick(object sender, EventArgs e)
        {
            MoverBala();
            ColisionPieza();
        }

        private void ResetGame()
        {
            if (LblNave.Vidas > 0 && !gameOver)
            {
                LblNave.Vidas --;
                BorrarVida();
                DibujarVidas();
                CrearBala();
            }
            else
            {
                FrmGameOver FrmGO = new FrmGameOver();
                TimerMain.Stop();
                FrmGO.ShowDialog();
                
                if (reset)
                {
                    panelPiezas.Controls.Clear();
                    CrearNave();
                    CrearPiezas();
                    CrearBala();
                    DibujarVidas();
                    reset = false;
                    gameOver = false;
                    TimerMain.Start();
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
            if (LblBala.Location.Y >= panelPiezas.Height)
            {
                ResetGame();                
            }
            
        }

        private void CalcularColisionBala(Label label)
        {
            // Colisión Nave
            Rectangle nave = label.Bounds;
            Rectangle bala = LblBala.Bounds;
            

            if (nave.IntersectsWith(bala))
            {
                TimerMain.Stop();
                LblBala.Enabled = false;
                // Con esto a veces la bala hace cosas raras por el centro de la nave
                // double relativeIntersection = (nave.X - bala.X + (bala.Width / 2));

                //double relativeIntersectionX =  ((nave.X + nave.Width) / 2) - (bala.X);
                //double relativeIntersectionY = (nave.Y) - ((bala.Y + bala.Height) / 2);




                /*
                 * Calcular el nuevo ángulo ha sido costoso. He tenido que tirar de ayuda de IA y luego ir ajustando los valores 
                 * hasta obtener un calculo esperado. La idea es dividir la nave en dos mitades, calcular el punto de intersección
                 * para saber a que distancia del centro ha golpeado la bala. Cuanto más lejos, mayor será el nuevo ángulo en dirección
                 * contraria a la que venía la bala. Cuando más al centro pegue la bala, el rebote tenderá a ser de 90º
                 * 
                 * He conseguido el rebote aceptable tras muchas y muchas pruebas y corrección del código. Este es el que de momento
                 * ha dado mejor resulado
                 * 
                 */
                
                // Calcular intersección en X e Y y hayar el ángulo de la tangente. En Y será constante, el rebote siempre es a
                // a la misma altura, y X variará según la intersección de la bala.
                double relativeIntersectionX = ((nave.X + nave.Width / 2) - (bala.X + bala.Width / 2)) / (bala.Width / 2);
                double relativeIntersectionY = ((bala.Y + bala.Height / 2) - (nave.Y + nave.Height / 2)) / (bala.Height / 2);
                double newAngle = Math.Atan2(relativeIntersectionX, relativeIntersectionY);

                newAngle = (newAngle + Math.PI) % (2 * Math.PI) - Math.PI/2;
                
                LblBala.SetAnguloRad(newAngle);
                LblBala.EstablecerVelocidadEjes();

                    //  LblBala.VelocidadX = LblBala.Speed * Math.Cos(newAngle);
                      //LblBala.VelocidadY = LblBala.Speed * Math.Sin(newAngle);

                /*
                double normalX = (nave.Left + nave.Width / 2) - (bala.Left + bala.Width / 2);
                double normalY = (nave.Top + nave.Height / 2) - (bala.Top + bala.Height / 2);

                // Normalizar la normal
                double length = Math.Sqrt(normalX * normalX + normalY * normalY);
                normalX /= length;
                normalY /= length;

                // Calcular el producto escalar entre la velocidad de la bala y la normal
                double dotProduct = LblBala.VelocidadX * normalX + LblBala.VelocidadY * normalY;

                // Calcular el vector de reflexión
                double reflectX = LblBala.VelocidadX - 2 * dotProduct * normalX;
                double reflectY = LblBala.VelocidadY - 2 * dotProduct * normalY;

                // Asignar el nuevo vector de velocidad reflejado
                LblBala.VelocidadX = reflectX;
                LblBala.VelocidadY = -reflectY;
                */
                LblBala.Enabled = true;
                TimerMain.Start();
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
