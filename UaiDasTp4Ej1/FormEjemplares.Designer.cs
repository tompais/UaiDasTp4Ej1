namespace UaiDasTp4Ej1
{
  partial class FormEjemplares
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
    dgvEjemplares = new DataGridView();
  groupBox1 = new GroupBox();
       chkActivo = new CheckBox();
       chkDisponible = new CheckBox();
  nudPrecio = new NumericUpDown();
  label4 = new Label();
            txtNumeroInventario = new TextBox();
       label3 = new Label();
    cmbObra = new ComboBox();
     label1 = new Label();
   panel1 = new Panel();
       btnCancelar = new Button();
       btnEliminar = new Button();
  btnModificar = new Button();
btnGuardar = new Button();
       btnNuevo = new Button();
    ((System.ComponentModel.ISupportInitialize)dgvEjemplares).BeginInit();
     groupBox1.SuspendLayout();
((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
panel1.SuspendLayout();
       SuspendLayout();
   // dgvEjemplares
dgvEjemplares.Dock = DockStyle.Fill;
      dgvEjemplares.Location = new Point(0, 200);
      dgvEjemplares.Name = "dgvEjemplares";
 dgvEjemplares.Size = new Size(1000, 350);
       // groupBox1
groupBox1.Controls.Add(chkActivo);
    groupBox1.Controls.Add(chkDisponible);
    groupBox1.Controls.Add(nudPrecio);
  groupBox1.Controls.Add(label4);
       groupBox1.Controls.Add(txtNumeroInventario);
      groupBox1.Controls.Add(label3);
  groupBox1.Controls.Add(cmbObra);
       groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
  groupBox1.Location = new Point(0, 60);
       groupBox1.Size = new Size(1000, 140);
      groupBox1.Text = "Datos del Ejemplar";
       // chkActivo
     chkActivo.AutoSize = true;
       chkActivo.Checked = true;
    chkActivo.CheckState = CheckState.Checked;
       chkActivo.Location = new Point(340, 100);
       chkActivo.Name = "chkActivo";
    chkActivo.Text = "Activo";
       // chkDisponible
chkDisponible.AutoSize = true;
    chkDisponible.Checked = true;
     chkDisponible.CheckState = CheckState.Checked;
chkDisponible.Location = new Point(220, 100);
      chkDisponible.Name = "chkDisponible";
   chkDisponible.Text = "Disponible";
       // nudPrecio
       nudPrecio.DecimalPlaces = 2;
     nudPrecio.Location = new Point(220, 65);
     nudPrecio.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
     nudPrecio.Name = "nudPrecio";
     nudPrecio.Size = new Size(150, 27);
       // label4
  label4.AutoSize = true;
     label4.Location = new Point(20, 68);
            label4.Text = "Precio:";
   // txtNumeroInventario
            txtNumeroInventario.Location = new Point(520, 30);
txtNumeroInventario.Name = "txtNumeroInventario";
       txtNumeroInventario.Size = new Size(200, 27);
       // label3
       label3.AutoSize = true;
   label3.Location = new Point(360, 33);
  label3.Text = "Número Inventario:";
   // cmbObra
       cmbObra.DropDownStyle = ComboBoxStyle.DropDownList;
 cmbObra.Location = new Point(100, 30);
     cmbObra.Name = "cmbObra";
            cmbObra.Size = new Size(250, 28);
       // label1
       label1.AutoSize = true;
    label1.Location = new Point(20, 33);
label1.Text = "Obra:";
     // panel1
     panel1.Controls.Add(btnCancelar);
    panel1.Controls.Add(btnEliminar);
            panel1.Controls.Add(btnModificar);
       panel1.Controls.Add(btnGuardar);
       panel1.Controls.Add(btnNuevo);
       panel1.Dock = DockStyle.Top;
    panel1.Size = new Size(1000, 60);
   // btnCancelar, btnEliminar, btnModificar, btnGuardar, btnNuevo
btnCancelar = new Button { Location = new Point(420, 15), Size = new Size(90, 35), Text = "Cancelar" };
          btnCancelar.Click += BtnCancelar_Click;
     btnEliminar = new Button { Location = new Point(320, 15), Size = new Size(90, 35), Text = "Eliminar" };
    btnEliminar.Click += BtnEliminar_Click;
       btnModificar = new Button { Location = new Point(220, 15), Size = new Size(90, 35), Text = "Modificar" };
btnModificar.Click += BtnModificar_Click;
btnGuardar = new Button { Location = new Point(120, 15), Size = new Size(90, 35), Text = "Guardar" };
     btnGuardar.Click += BtnGuardar_Click;
    btnNuevo = new Button { Location = new Point(20, 15), Size = new Size(90, 35), Text = "Nuevo" };
    btnNuevo.Click += BtnNuevo_Click;
  // FormEjemplares
  ClientSize = new Size(1000, 550);
     Controls.Add(dgvEjemplares);
       Controls.Add(groupBox1);
       Controls.Add(panel1);
    Name = "FormEjemplares";
     Text = "Gestión de Ejemplares";
 Load += FormEjemplares_Load;
   ((System.ComponentModel.ISupportInitialize)dgvEjemplares).EndInit();
       groupBox1.ResumeLayout(false);
 ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
panel1.ResumeLayout(false);
ResumeLayout(false);
        }

  private DataGridView dgvEjemplares;
     private GroupBox groupBox1;
       private Label label1;
     private ComboBox cmbObra;
        private TextBox txtNumeroInventario;
        private Label label3;
  private NumericUpDown nudPrecio;
     private Label label4;
   private CheckBox chkDisponible;
     private CheckBox chkActivo;
     private Panel panel1;
        private Button btnNuevo, btnGuardar, btnModificar, btnEliminar, btnCancelar;
    }
}
