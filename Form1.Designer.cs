
namespace arkanoid
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelPiezas = new System.Windows.Forms.Panel();
            this.TimerMain = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panelPiezas
            // 
            this.panelPiezas.BackColor = System.Drawing.Color.Navy;
            this.panelPiezas.Location = new System.Drawing.Point(0, -1);
            this.panelPiezas.Name = "panelPiezas";
            this.panelPiezas.Size = new System.Drawing.Size(614, 568);
            this.panelPiezas.TabIndex = 1;
            // 
            // TimerMain
            // 
            this.TimerMain.Interval = 40;
            this.TimerMain.Tick += new System.EventHandler(this.TimerMain_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(612, 564);
            this.Controls.Add(this.panelPiezas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelPiezas;
        private System.Windows.Forms.Timer TimerMain;
    }
}

