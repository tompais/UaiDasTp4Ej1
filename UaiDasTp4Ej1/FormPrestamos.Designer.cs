namespace UaiDasTp4Ej1
{
    partial class FormPrestamos
    {
  private System.ComponentModel.IContainer components = null;

protected override void Dispose(bool disposing)
        {
    if (disposing && (components != null))
     components.Dispose();
     base.Dispose(disposing);
        }

        private void InitializeComponent()
     {
      dgvPrestamos = new DataGridView();
   groupBox1 = new GroupBox();
   dtpFechaPrestamo = new DateTimePicker();
     label3 = new Label();
      cmbEjemplar = new ComboBox();
     label2 = new Label();
cmbAlumno = new ComboBox();
    label1 = new Label();
       panel1 = new Panel();
    btnDevolver = new Button();
  btnCancelar = new Button();
       btnGuardar = new Button();
     btnNuevo = new Button();
     ((System.ComponentModel.ISupportInitialize)dgvPrestamos).BeginInit();
   groupBox1.SuspendLayout();
   panel1.SuspendLayout();
SuspendLayout();
       // dgvPrestamos
dgvPrestamos.Dock = DockStyle.Fill;
      dgvPrestamos.Location = new Point(0, 200);
     dgvPrestamos.Name = "dgvPrestamos";
    dgvPrestamos.Size = new Size(1000, 350);
// groupBox1
     groupBox1.Controls.Add(dtpFechaPrestamo);
       groupBox1.Controls.Add(label3);
  groupBox1.Controls.Add(cmbEjemplar);
  groupBox1.Controls.Add(label2);
    groupBox1.Controls.Add(cmbAlumno);
groupBox1.Controls.Add(label1);
    groupBox1.Dock = DockStyle.Top;
       groupBox1.Location = new Point(0, 60);
       groupBox1.Size = new Size(1000, 140);
 groupBox1.Text = "Datos del Préstamo";
    // dtpFechaPrestamo
     dtpFechaPrestamo.Format = DateTimePickerFormat.Short;
dtpFechaPrestamo.Location = new Point(220, 100);
  dtpFechaPrestamo.Name = "dtpFechaPrestamo";
        dtpFechaPrestamo.Size = new Size(150, 27);
   // label3
     label3.AutoSize = true;
    label3.Location = new Point(20, 103);
   label3.Text = "Fecha de Préstamo:";
  // cmbEjemplar
    cmbEjemplar.DropDownStyle = ComboBoxStyle.DropDownList;
       cmbEjemplar.Location = new Point(220, 65);
  cmbEjemplar.Name = "cmbEjemplar";
  cmbEjemplar.Size = new Size(350, 28);
       // label2
    label2.AutoSize = true;
     label2.Location = new Point(20, 68);
       label2.Text = "Ejemplar:";
   // cmbAlumno
     cmbAlumno.DropDownStyle = ComboBoxStyle.DropDownList;
cmbAlumno.Location = new Point(220, 30);
       cmbAlumno.Name = "cmbAlumno";
            cmbAlumno.Size = new Size(350, 28);
   // label1
            label1.AutoSize = true;
label1.Location = new Point(20, 33);
       label1.Text = "Alumno:";
// panel1
       panel1.Controls.Add(btnDevolver);
    panel1.Controls.Add(btnCancelar);
  panel1.Controls.Add(btnGuardar);
     panel1.Controls.Add(btnNuevo);
  panel1.Dock = DockStyle.Top;
       panel1.Size = new Size(1000, 60);
    // btnDevolver, btnCancelar, btnGuardar, btnNuevo
       btnDevolver = new Button { Location = new Point(320, 15), Size = new Size(90, 35), Text = "Devolver" };
btnDevolver.Click += BtnDevolver_Click;
    btnCancelar = new Button { Location = new Point(220, 15), Size = new Size(90, 35), Text = "Cancelar" };
       btnCancelar.Click += BtnCancelar_Click;
     btnGuardar = new Button { Location = new Point(120, 15), Size = new Size(90, 35), Text = "Guardar" };
       btnGuardar.Click += BtnGuardar_Click;
  btnNuevo = new Button { Location = new Point(20, 15), Size = new Size(90, 35), Text = "Nuevo" };
     btnNuevo.Click += BtnNuevo_Click;
       // FormPrestamos
  ClientSize = new Size(1000, 550);
     Controls.Add(dgvPrestamos);
Controls.Add(groupBox1);
     Controls.Add(panel1);
       Name = "FormPrestamos";
       Text = "Gestión de Préstamos";
  Load += FormPrestamos_Load;
  ((System.ComponentModel.ISupportInitialize)dgvPrestamos).EndInit();
groupBox1.ResumeLayout(false);
 panel1.ResumeLayout(false);
ResumeLayout(false);
}

   private DataGridView dgvPrestamos;
      private GroupBox groupBox1;
     private Label label1;
     private ComboBox cmbAlumno;
   private ComboBox cmbEjemplar;
     private Label label2;
  private DateTimePicker dtpFechaPrestamo;
private Label label3;
   private Panel panel1;
   private Button btnNuevo, btnGuardar, btnCancelar, btnDevolver;
    }
}
