namespace UaiDasTp4Ej1
{
    partial class FormObras
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
        dgvObras = new DataGridView();
  groupBox1 = new GroupBox();
  chkActivo = new CheckBox();
   txtGenero = new TextBox();
       label7 = new Label();
     nudAnioPublicacion = new NumericUpDown();
    label6 = new Label();
  txtIsbn = new TextBox();
  label4 = new Label();
    txtEditorial = new TextBox();
  label3 = new Label();
      txtAutor = new TextBox();
     label2 = new Label();
   txtTitulo = new TextBox();
         label1 = new Label();
   panel1 = new Panel();
btnCancelar = new Button();
   btnEliminar = new Button();
btnModificar = new Button();
 btnGuardar = new Button();
     btnNuevo = new Button();
  ((System.ComponentModel.ISupportInitialize)dgvObras).BeginInit();
 groupBox1.SuspendLayout();
  ((System.ComponentModel.ISupportInitialize)nudAnioPublicacion).BeginInit();
 panel1.SuspendLayout();
  SuspendLayout();
 // dgvObras
 dgvObras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
     dgvObras.Dock = DockStyle.Fill;
      dgvObras.Location = new Point(0, 200);
       dgvObras.Name = "dgvObras";
   dgvObras.Size = new Size(1000, 350);
dgvObras.TabIndex = 0;
    // groupBox1
  groupBox1.Controls.Add(chkActivo);
       groupBox1.Controls.Add(txtGenero);
       groupBox1.Controls.Add(label7);
 groupBox1.Controls.Add(nudAnioPublicacion);
   groupBox1.Controls.Add(label6);
  groupBox1.Controls.Add(txtIsbn);
groupBox1.Controls.Add(label4);
       groupBox1.Controls.Add(txtEditorial);
            groupBox1.Controls.Add(label3);
    groupBox1.Controls.Add(txtAutor);
 groupBox1.Controls.Add(label2);
   groupBox1.Controls.Add(txtTitulo);
       groupBox1.Controls.Add(label1);
   groupBox1.Dock = DockStyle.Top;
  groupBox1.Location = new Point(0, 60);
   groupBox1.Name = "groupBox1";
       groupBox1.Size = new Size(1000, 140);
      groupBox1.TabIndex = 1;
 groupBox1.TabStop = false;
       groupBox1.Text = "Datos de la Obra";
   // chkActivo
  chkActivo.AutoSize = true;
   chkActivo.Checked = true;
    chkActivo.CheckState = CheckState.Checked;
  chkActivo.Location = new Point(750, 103);
 chkActivo.Name = "chkActivo";
     chkActivo.Size = new Size(72, 24);
  chkActivo.TabIndex = 12;
     chkActivo.Text = "Activo";
   chkActivo.UseVisualStyleBackColor = true;
   // txtGenero
     txtGenero.Location = new Point(580, 65);
     txtGenero.Name = "txtGenero";
 txtGenero.Size = new Size(200, 27);
 txtGenero.TabIndex = 11;
       // label7
label7.AutoSize = true;
label7.Location = new Point(500, 68);
   label7.Name = "label7";
label7.Size = new Size(60, 20);
    label7.TabIndex = 10;
label7.Text = "Género:";
   // nudAnioPublicacion
       nudAnioPublicacion.Location = new Point(580, 30);
nudAnioPublicacion.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
  nudAnioPublicacion.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
       nudAnioPublicacion.Name = "nudAnioPublicacion";
     nudAnioPublicacion.Size = new Size(120, 27);
     nudAnioPublicacion.TabIndex = 9;
       nudAnioPublicacion.Value = new decimal(new int[] { 2025, 0, 0, 0 });
  // label6
  label6.AutoSize = true;
    label6.Location = new Point(420, 32);
   label6.Name = "label6";
label6.Size = new Size(142, 20);
     label6.TabIndex = 8;
       label6.Text = "Año de Publicación:";
       // txtIsbn
       txtIsbn.Location = new Point(120, 100);
  txtIsbn.Name = "txtIsbn";
     txtIsbn.Size = new Size(200, 27);
  txtIsbn.TabIndex = 7;
     // label4
      label4.AutoSize = true;
      label4.Location = new Point(20, 103);
       label4.Name = "label4";
label4.Size = new Size(42, 20);
  label4.TabIndex = 6;
  label4.Text = "ISBN:";
  // txtEditorial
       txtEditorial.Location = new Point(320, 65);
 txtEditorial.Name = "txtEditorial";
  txtEditorial.Size = new Size(250, 27);
txtEditorial.TabIndex = 5;
  // label3
      label3.AutoSize = true;
       label3.Location = new Point(240, 68);
     label3.Name = "label3";
            label3.Size = new Size(67, 20);
   label3.TabIndex = 4;
  label3.Text = "Editorial:";
     // txtAutor
   txtAutor.Location = new Point(120, 65);
 txtAutor.Name = "txtAutor";
  txtAutor.Size = new Size(200, 27);
  txtAutor.TabIndex = 3;
       // label2
      label2.AutoSize = true;
      label2.Location = new Point(20, 68);
label2.Name = "label2";
     label2.Size = new Size(49, 20);
label2.TabIndex = 2;
   label2.Text = "Autor:";
      // txtTitulo
       txtTitulo.Location = new Point(120, 30);
txtTitulo.Name = "txtTitulo";
      txtTitulo.Size = new Size(450, 27);
     txtTitulo.TabIndex = 1;
    // label1
    label1.AutoSize = true;
     label1.Location = new Point(20, 33);
     label1.Name = "label1";
  label1.Size = new Size(50, 20);
   label1.TabIndex = 0;
  label1.Text = "Título:";
   // panel1
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
       // btnCancelar
     btnCancelar.Location = new Point(420, 15);
  btnCancelar.Name = "btnCancelar";
  btnCancelar.Size = new Size(90, 35);
         btnCancelar.TabIndex = 4;
 btnCancelar.Text = "Cancelar";
btnCancelar.UseVisualStyleBackColor = true;
       btnCancelar.Click += btnCancelar_Click;
      // btnEliminar
  btnEliminar.Location = new Point(320, 15);
  btnEliminar.Name = "btnEliminar";
       btnEliminar.Size = new Size(90, 35);
  btnEliminar.TabIndex = 3;
       btnEliminar.Text = "Eliminar";
   btnEliminar.UseVisualStyleBackColor = true;
    btnEliminar.Click += BtnEliminar_Click;
    // btnModificar
       btnModificar.Location = new Point(220, 15);
btnModificar.Name = "btnModificar";
  btnModificar.Size = new Size(90, 35);
     btnModificar.TabIndex = 2;
     btnModificar.Text = "Modificar";
   btnModificar.UseVisualStyleBackColor = true;
  btnModificar.Click += BtnModificar_Click;
   // btnGuardar
       btnGuardar.Location = new Point(120, 15);
       btnGuardar.Name = "btnGuardar";
     btnGuardar.Size = new Size(90, 35);
     btnGuardar.TabIndex = 1;
       btnGuardar.Text = "Guardar";
       btnGuardar.UseVisualStyleBackColor = true;
       btnGuardar.Click += BtnGuardar_Click;
       // btnNuevo
btnNuevo.Location = new Point(20, 15);
  btnNuevo.Name = "btnNuevo";
btnNuevo.Size = new Size(90, 35);
btnNuevo.TabIndex = 0;
       btnNuevo.Text = "Nuevo";
       btnNuevo.UseVisualStyleBackColor = true;
         btnNuevo.Click += BtnNuevo_Click;
  // FormObras
 AutoScaleDimensions = new SizeF(8F, 20F);
       AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 550);
   Controls.Add(dgvObras);
       Controls.Add(groupBox1);
       Controls.Add(panel1);
  Name = "FormObras";
       StartPosition = FormStartPosition.CenterParent;
  Text = "Gestión de Obras";
       Load += FormObras_Load;
  ((System.ComponentModel.ISupportInitialize)dgvObras).EndInit();
     groupBox1.ResumeLayout(false);
       groupBox1.PerformLayout();
 ((System.ComponentModel.ISupportInitialize)nudAnioPublicacion).EndInit();
  panel1.ResumeLayout(false);
   ResumeLayout(false);
        }

        private DataGridView dgvObras;
     private GroupBox groupBox1;
     private TextBox txtTitulo;
private Label label1;
        private TextBox txtAutor;
  private Label label2;
private TextBox txtEditorial;
     private Label label3;
private TextBox txtIsbn;
     private Label label4;
     private NumericUpDown nudAnioPublicacion;
        private Label label6;
private TextBox txtGenero;
     private Label label7;
   private CheckBox chkActivo;
   private Panel panel1;
   private Button btnNuevo;
   private Button btnGuardar;
  private Button btnModificar;
  private Button btnEliminar;
        private Button btnCancelar;
  }
}
