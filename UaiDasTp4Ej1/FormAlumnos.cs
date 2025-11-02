using DOM;
using SERV;

namespace UaiDasTp4Ej1
{
    public partial class FormAlumnos : Form
    {
        private readonly AlumnoService _alumnoService;
        private Alumno? _alumnoSeleccionado;

        public FormAlumnos(AlumnoService alumnoService)
        {
            InitializeComponent();
            _alumnoService = alumnoService;
        }

        private void FormAlumnos_Load(object sender, EventArgs e)
        {
            CargarAlumnos();
            LimpiarCampos();
        }

        private void CargarAlumnos()
        {
            try
            {
                var alumnos = _alumnoService.GetAllAlumnos();
                dgvAlumnos.DataSource = alumnos.ToList();
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alumnos: {ex.Message}", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlumnos.MultiSelect = false;
            dgvAlumnos.ReadOnly = true;

            if (dgvAlumnos.Columns.Count > 0)
            {
                dgvAlumnos.Columns["Id"].Width = 50;
                dgvAlumnos.Columns["Dni"].Width = 100;
                dgvAlumnos.Columns["Nombre"].Width = 120;
                dgvAlumnos.Columns["Apellido"].Width = 120;
                dgvAlumnos.Columns["Email"].Width = 180;
                dgvAlumnos.Columns["Telefono"].Width = 100;
                dgvAlumnos.Columns["FechaNacimiento"].Width = 120;
                dgvAlumnos.Columns["Activo"].Width = 60;
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            _alumnoSeleccionado = null;
            LimpiarCampos();
            HabilitarCampos(true);
            txtNombre.Focus();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                var alumno = new Alumno
                {
                    Id = _alumnoSeleccionado?.Id ?? 0,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Dni = txtDni.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    FechaNacimiento = dtpFechaNacimiento.Value.Date,
                    Activo = chkActivo.Checked
                };

                if (_alumnoSeleccionado == null)
                {
                    _alumnoService.CreateAlumno(alumno);
                    MessageBox.Show("Alumno creado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _alumnoService.UpdateAlumno(alumno);
                    MessageBox.Show("Alumno actualizado exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarAlumnos();
                LimpiarCampos();
                HabilitarCampos(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
         MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un alumno para modificar", "Advertencia",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _alumnoSeleccionado = (Alumno)dgvAlumnos.SelectedRows[0].DataBoundItem;
            CargarDatosEnCampos(_alumnoSeleccionado);
            HabilitarCampos(true);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un alumno para eliminar", "Advertencia",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro que desea eliminar este alumno?", "Confirmar",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var alumno = (Alumno)dgvAlumnos.SelectedRows[0].DataBoundItem;
                _alumnoService.DeleteAlumno(alumno.Id);
                MessageBox.Show("Alumno eliminado exitosamente", "Éxito",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarAlumnos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(false);
            _alumnoSeleccionado = null;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            dtpFechaNacimiento.Value = DateTime.Today.AddYears(-18);
            chkActivo.Checked = true;
        }

        private void CargarDatosEnCampos(Alumno alumno)
        {
            txtNombre.Text = alumno.Nombre;
            txtApellido.Text = alumno.Apellido;
            txtDni.Text = alumno.Dni;
            txtEmail.Text = alumno.Email;
            txtTelefono.Text = alumno.Telefono;
            dtpFechaNacimiento.Value = alumno.FechaNacimiento;
            chkActivo.Checked = alumno.Activo;
        }

        private void HabilitarCampos(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtDni.Enabled = habilitar;
            txtEmail.Enabled = habilitar;
            txtTelefono.Enabled = habilitar;
            dtpFechaNacimiento.Enabled = habilitar;
            chkActivo.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es requerido", "Validación",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show("El DNI es requerido", "Validación",
           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDni.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("El email es requerido", "Validación",
       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (dtpFechaNacimiento.Value >= DateTime.Today)
            {
                MessageBox.Show("La fecha de nacimiento debe ser anterior a hoy", "Validación",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaNacimiento.Focus();
                return false;
            }

            return true;
        }
    }
}
