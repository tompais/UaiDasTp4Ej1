namespace UaiDasTp4Ej1
{
    partial class FormAlumnos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
 {
          if (disposing && (components != null))
            {
  components.Dispose();
            }
          base.Dispose(disposing);
      }

        private void InitializeComponent()
        {
            dgvAlumnos = new DataGridView();
       groupBox1 = new GroupBox();
 chkActivo = new CheckBox();
       dtpFechaNacimiento = new DateTimePicker();
  label6 = new Label();
   txtTelefono = new TextBox();
         label5 = new Label();
      txtEmail = new TextBox();
   label4 = new Label();
  txtDni = new TextBox();
    label3 = new Label();
         txtApellido = new TextBox();
     label2 = new Label();
 txtNombre = new TextBox();
  label1 = new Label();
       panel1 = new Panel();
btnCancelar = new Button();
 btnEliminar = new Button();
     btnModificar = new Button();
      btnGuardar = new Button();
     btnNuevo = new Button();
     ((System.ComponentModel.ISupportInitialize)dgvAlumnos).BeginInit();
       groupBox1.SuspendLayout();
       panel1.SuspendLayout();
SuspendLayout();
      // 
  // dgvAlumnos
       // 
  dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    dgvAlumnos.Dock = DockStyle.Fill;
  dgvAlumnos.Location = new Point(0, 200);
     dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.RowHeadersWidth = 51;
  dgvAlumnos.Size = new Size(1000, 350);
     dgvAlumnos.TabIndex = 0;
   // 
    // groupBox1
   // 
    groupBox1.Controls.Add(chkActivo);
   groupBox1.Controls.Add(dtpFechaNacimiento);
  groupBox1.Controls.Add(label6);
      groupBox1.Controls.Add(txtTelefono);
    groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtEmail);
       groupBox1.Controls.Add(label4);
     groupBox1.Controls.Add(txtDni);
      groupBox1.Controls.Add(label3);
groupBox1.Controls.Add(txtApellido);
       groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(label1);
     groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 60);
groupBox1.Name = "groupBox1";
     groupBox1.Size = new Size(1000, 140);
      groupBox1.TabIndex = 1;
  groupBox1.TabStop = false;
    groupBox1.Text = "Datos del Alumno";
 // 
       // chkActivo
  // 
chkActivo.AutoSize = true;
      chkActivo.Checked = true;
   chkActivo.CheckState = CheckState.Checked;
       chkActivo.Location = new Point(580, 100);
        chkActivo.Name = "chkActivo";
       chkActivo.Size = new Size(72, 24);
    chkActivo.TabIndex = 12;
    chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
       // 
    // dtpFechaNacimiento
   // 
  dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
       dtpFechaNacimiento.Location = new Point(580, 65);
     dtpFechaNacimiento.Name = "dtpFechaNacimiento";
   dtpFechaNacimiento.Size = new Size(150, 27);
  dtpFechaNacimiento.TabIndex = 11;
        // 
            // label6
         // 
    label6.AutoSize = true;
  label6.Location = new Point(420, 70);
   label6.Name = "label6";
      label6.Size = new Size(154, 20);
     label6.TabIndex = 10;
            label6.Text = "Fecha de Nacimiento:";
         // 
// txtTelefono
  // 
 txtTelefono.Location = new Point(580, 30);
   txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(200, 27);
   txtTelefono.TabIndex = 9;
// 
  // label5
            // 
        label5.AutoSize = true;
  label5.Location = new Point(420, 33);
   label5.Name = "label5";
            label5.Size = new Size(70, 20);
    label5.TabIndex = 8;
  label5.Text = "Teléfono:";
     // 
        // txtEmail
            // 
      txtEmail.Location = new Point(120, 100);
   txtEmail.Name = "txtEmail";
      txtEmail.Size = new Size(250, 27);
  txtEmail.TabIndex = 7;
// 
        // label4
  // 
            label4.AutoSize = true;
    label4.Location = new Point(20, 103);
            label4.Name = "label4";
  label4.Size = new Size(49, 20);
            label4.TabIndex = 6;
   label4.Text = "Email:";
 // 
// txtDni
            // 
      txtDni.Location = new Point(120, 65);
            txtDni.Name = "txtDni";
    txtDni.Size = new Size(150, 27);
  txtDni.TabIndex = 5;
  // 
       // label3
      // 
            label3.AutoSize = true;
    label3.Location = new Point(20, 68);
     label3.Name = "label3";
            label3.Size = new Size(38, 20);
            label3.TabIndex = 4;
  label3.Text = "DNI:";
// 
 // txtApellido
// 
txtApellido.Location = new Point(320, 30);
      txtApellido.Name = "txtApellido";
     txtApellido.Size = new Size(250, 27);
 txtApellido.TabIndex = 3;
            // 
         // label2
 // 
         label2.AutoSize = true;
    label2.Location = new Point(290, 33);
       label2.Name = "label2";
label2.Size = new Size(69, 20);
label2.TabIndex = 2;
    label2.Text = "Apellido:";
      // 
     // txtNombre
     // 
      txtNombre.Location = new Point(120, 30);
       txtNombre.Name = "txtNombre";
txtNombre.Size = new Size(150, 27);
    txtNombre.TabIndex = 1;
    // 
  // label1
   // 
   label1.AutoSize = true;
 label1.Location = new Point(20, 33);
  label1.Name = "label1";
  label1.Size = new Size(67, 20);
         label1.TabIndex = 0;
       label1.Text = "Nombre:";
       // 
          // panel1
    // 
       panel1.Controls.Add(btnCancelar);
    panel1.Controls.Add(btnEliminar);
       panel1.Controls.Add(btnModificar);
   panel1.Controls.Add(btnGuardar);
            panel1.Controls.Add(btnNuevo);
    panel1.Dock = DockStyle.Top;
      panel1.Location = new Point(0, 0);
  panel1.Name = "panel1";
       panel1.Size = new Size(1000, 60);
   panel1.TabIndex = 2;
      // 
         // btnCancelar
            // 
  btnCancelar.Location = new Point(420, 15);
       btnCancelar.Name = "btnCancelar";
     btnCancelar.Size = new Size(90, 35);
     btnCancelar.TabIndex = 4;
 btnCancelar.Text = "Cancelar";
      btnCancelar.UseVisualStyleBackColor = true;
       btnCancelar.Click += btnCancelar_Click;
     // 
            // btnEliminar
    // 
            btnEliminar.Location = new Point(320, 15);
      btnEliminar.Name = "btnEliminar";
       btnEliminar.Size = new Size(90, 35);
 btnEliminar.TabIndex = 3;
     btnEliminar.Text = "Eliminar";
        btnEliminar.UseVisualStyleBackColor = true;
   btnEliminar.Click += btnEliminar_Click;
       // 
    // btnModificar
       // 
       btnModificar.Location = new Point(220, 15);
       btnModificar.Name = "btnModificar";
       btnModificar.Size = new Size(90, 35);
            btnModificar.TabIndex = 2;
     btnModificar.Text = "Modificar";
       btnModificar.UseVisualStyleBackColor = true;
    btnModificar.Click += btnModificar_Click;
      // 
    // btnGuardar
       // 
            btnGuardar.Location = new Point(120, 15);
       btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(90, 35);
btnGuardar.TabIndex = 1;
   btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
       btnGuardar.Click += btnGuardar_Click;
       // 
 // btnNuevo
            // 
            btnNuevo.Location = new Point(20, 15);
  btnNuevo.Name = "btnNuevo";
       btnNuevo.Size = new Size(90, 35);
   btnNuevo.TabIndex = 0;
      btnNuevo.Text = "Nuevo";
   btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
  // 
    // FormAlumnos
            // 
       AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 550);
            Controls.Add(dgvAlumnos);
       Controls.Add(groupBox1);
  Controls.Add(panel1);
            Name = "FormAlumnos";
            StartPosition = FormStartPosition.CenterParent;
       Text = "Gestión de Alumnos";
      Load += FormAlumnos_Load;
       ((System.ComponentModel.ISupportInitialize)dgvAlumnos).EndInit();
groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
   panel1.ResumeLayout(false);
   ResumeLayout(false);
 }

        private DataGridView dgvAlumnos;
        private GroupBox groupBox1;
        private TextBox txtNombre;
    private Label label1;
        private TextBox txtApellido;
        private Label label2;
   private TextBox txtDni;
        private Label label3;
        private TextBox txtEmail;
   private Label label4;
      private TextBox txtTelefono;
        private Label label5;
        private DateTimePicker dtpFechaNacimiento;
   private Label label6;
      private CheckBox chkActivo;
        private Panel panel1;
        private Button btnNuevo;
   private Button btnGuardar;
        private Button btnModificar;
        private Button btnEliminar;
   private Button btnCancelar;
    }
}
