namespace UaiDasTp4Ej1
{
    partial class FormPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
  private System.ComponentModel.IContainer components = null;

        /// <summary>
     ///  Clean up any resources being used.
    /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
    if (disposing && (components != null))
            {
       components.Dispose();
  }
    base.Dispose(disposing);
 }

        #region Windows Form Designer generated code

    /// <summary>
       ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
 gestionToolStripMenuItem = new ToolStripMenuItem();
  alumnosToolStripMenuItem = new ToolStripMenuItem();
            obrasToolStripMenuItem = new ToolStripMenuItem();
            ejemplaresToolStripMenuItem = new ToolStripMenuItem();
 prestamosToolStripMenuItem = new ToolStripMenuItem();
      toolStripSeparator1 = new ToolStripSeparator();
       salirToolStripMenuItem = new ToolStripMenuItem();
        ventanasToolStripMenuItem = new ToolStripMenuItem();
     cascadaToolStripMenuItem = new ToolStripMenuItem();
       horizontalToolStripMenuItem = new ToolStripMenuItem();
  verticalToolStripMenuItem = new ToolStripMenuItem();
   ayudaToolStripMenuItem = new ToolStripMenuItem();
       acercaDeToolStripMenuItem = new ToolStripMenuItem();
statusStrip1 = new StatusStrip();
 toolStripStatusLabel1 = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
  SuspendLayout();
 // 
       // menuStrip1
            // 
 menuStrip1.ImageScalingSize = new Size(20, 20);
         menuStrip1.Items.AddRange(new ToolStripItem[] { gestionToolStripMenuItem, ventanasToolStripMenuItem, ayudaToolStripMenuItem });
 menuStrip1.Location = new Point(0, 0);
menuStrip1.MdiWindowListItem = ventanasToolStripMenuItem;
        menuStrip1.Name = "menuStrip1";
  menuStrip1.Size = new Size(1200, 28);
menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
       // 
            // gestionToolStripMenuItem
   // 
      gestionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { alumnosToolStripMenuItem, obrasToolStripMenuItem, ejemplaresToolStripMenuItem, prestamosToolStripMenuItem, toolStripSeparator1, salirToolStripMenuItem });
       gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            gestionToolStripMenuItem.Size = new Size(73, 24);
   gestionToolStripMenuItem.Text = "&Gestión";
     // 
     // alumnosToolStripMenuItem
 // 
        alumnosToolStripMenuItem.Name = "alumnosToolStripMenuItem";
   alumnosToolStripMenuItem.Size = new Size(224, 26);
      alumnosToolStripMenuItem.Text = "&Alumnos";
            alumnosToolStripMenuItem.Click += AlumnosToolStripMenuItem_Click;
     // 
   // obrasToolStripMenuItem
         // 
         obrasToolStripMenuItem.Name = "obrasToolStripMenuItem";
 obrasToolStripMenuItem.Size = new Size(224, 26);
      obrasToolStripMenuItem.Text = "&Obras";
            obrasToolStripMenuItem.Click += ObrasToolStripMenuItem_Click;
       // 
        // ejemplaresToolStripMenuItem
 // 
      ejemplaresToolStripMenuItem.Name = "ejemplaresToolStripMenuItem";
            ejemplaresToolStripMenuItem.Size = new Size(224, 26);
            ejemplaresToolStripMenuItem.Text = "&Ejemplares";
       ejemplaresToolStripMenuItem.Click += EjemplaresToolStripMenuItem_Click;
          // 
 // prestamosToolStripMenuItem
            // 
   prestamosToolStripMenuItem.Name = "prestamosToolStripMenuItem";
   prestamosToolStripMenuItem.Size = new Size(224, 26);
      prestamosToolStripMenuItem.Text = "&Préstamos";
 prestamosToolStripMenuItem.Click += PrestamosToolStripMenuItem_Click;
         // 
  // toolStripSeparator1
 // 
       toolStripSeparator1.Name = "toolStripSeparator1";
     toolStripSeparator1.Size = new Size(221, 6);
  // 
       // salirToolStripMenuItem
 // 
     salirToolStripMenuItem.Name = "salirToolStripMenuItem";
       salirToolStripMenuItem.Size = new Size(224, 26);
            salirToolStripMenuItem.Text = "&Salir";
    salirToolStripMenuItem.Click += SalirToolStripMenuItem_Click;
            // 
          // ventanasToolStripMenuItem
// 
ventanasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cascadaToolStripMenuItem, horizontalToolStripMenuItem, verticalToolStripMenuItem });
          ventanasToolStripMenuItem.Name = "ventanasToolStripMenuItem";
  ventanasToolStripMenuItem.Size = new Size(82, 24);
            ventanasToolStripMenuItem.Text = "&Ventanas";
        // 
          // cascadaToolStripMenuItem
  // 
            cascadaToolStripMenuItem.Name = "cascadaToolStripMenuItem";
  cascadaToolStripMenuItem.Size = new Size(167, 26);
            cascadaToolStripMenuItem.Text = "&Cascada";
       cascadaToolStripMenuItem.Click += CascadaToolStripMenuItem_Click;
          // 
       // horizontalToolStripMenuItem
      // 
        horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
    horizontalToolStripMenuItem.Size = new Size(167, 26);
    horizontalToolStripMenuItem.Text = "&Horizontal";
  horizontalToolStripMenuItem.Click += HorizontalToolStripMenuItem_Click;
  // 
// verticalToolStripMenuItem
   // 
      verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            verticalToolStripMenuItem.Size = new Size(167, 26);
  verticalToolStripMenuItem.Text = "&Vertical";
     verticalToolStripMenuItem.Click += VerticalToolStripMenuItem_Click;
// 
            // ayudaToolStripMenuItem
  // 
    ayudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { acercaDeToolStripMenuItem });
   ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
 ayudaToolStripMenuItem.Size = new Size(65, 24);
          ayudaToolStripMenuItem.Text = "A&yuda";
// 
         // acercaDeToolStripMenuItem
     // 
 acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
        acercaDeToolStripMenuItem.Size = new Size(165, 26);
         acercaDeToolStripMenuItem.Text = "&Acerca de...";
     acercaDeToolStripMenuItem.Click += AcercaDeToolStripMenuItem_Click;
       // 
  // statusStrip1
   // 
         statusStrip1.ImageScalingSize = new Size(20, 20);
  statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
statusStrip1.Location = new Point(0, 628);
   statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1200, 22);
   statusStrip1.TabIndex = 2;
  statusStrip1.Text = "statusStrip1";
  // 
            // toolStripStatusLabel1
 // 
  toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      toolStripStatusLabel1.Size = new Size(0, 16);
    // 
            // FormPrincipal
          // 
     AutoScaleDimensions = new SizeF(8F, 20F);
     AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 650);
  Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
 IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
    Name = "FormPrincipal";
       StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Biblioteca";
          WindowState = FormWindowState.Maximized;
            Load += FormPrincipal_Load;
     menuStrip1.ResumeLayout(false);
    menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
    statusStrip1.PerformLayout();
            ResumeLayout(false);
 PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem gestionToolStripMenuItem;
        private ToolStripMenuItem alumnosToolStripMenuItem;
  private ToolStripMenuItem obrasToolStripMenuItem;
   private ToolStripMenuItem ejemplaresToolStripMenuItem;
    private ToolStripMenuItem prestamosToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem salirToolStripMenuItem;
   private ToolStripMenuItem ventanasToolStripMenuItem;
      private ToolStripMenuItem cascadaToolStripMenuItem;
  private ToolStripMenuItem horizontalToolStripMenuItem;
        private ToolStripMenuItem verticalToolStripMenuItem;
        private ToolStripMenuItem ayudaToolStripMenuItem;
      private ToolStripMenuItem acercaDeToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}
