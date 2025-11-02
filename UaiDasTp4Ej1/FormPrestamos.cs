using DOM;
using SERV;

namespace UaiDasTp4Ej1
{
    public partial class FormPrestamos : Form
    {
        private readonly PrestamoService _prestamoService;
        private readonly AlumnoService _alumnoService;
        private readonly EjemplarService _ejemplarService;

        public FormPrestamos(PrestamoService prestamoService, AlumnoService alumnoService, EjemplarService ejemplarService)
        {
            InitializeComponent();
            _prestamoService = prestamoService;
            _alumnoService = alumnoService;
            _ejemplarService = ejemplarService;
        }

        private void FormPrestamos_Load(object sender, EventArgs e)
        {
            CargarAlumnos();
            CargarEjemplares();
            CargarPrestamos();
            HabilitarCampos(false);
        }

        private void CargarAlumnos()
        {
            try
            {
                var alumnos = _alumnoService.GetAllAlumnos();
                cmbAlumno.DataSource = alumnos.Where(a => a.Activo).ToList();
                cmbAlumno.DisplayMember = "Apellido";
                cmbAlumno.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alumnos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEjemplares()
        {
            try
            {
                var ejemplares = _ejemplarService.GetEjemplaresDisponibles();
                cmbEjemplar.DataSource = ejemplares.ToList();
                cmbEjemplar.DisplayMember = "NumeroInventario";
                cmbEjemplar.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ejemplares: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPrestamos()
        {
            try
            {
                var prestamos = _prestamoService.GetAllPrestamos();
                dgvPrestamos.DataSource = prestamos.ToList();
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar préstamos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrestamos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrestamos.MultiSelect = false;
            dgvPrestamos.ReadOnly = true;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarCampos(true);

            if (cmbAlumno.Items.Count > 0)
            {
                cmbAlumno.SelectedIndex = 0;
            }

            if (cmbEjemplar.Items.Count > 0)
            {
                cmbEjemplar.SelectedIndex = 0;
            }

            dtpFechaPrestamo.Value = DateTime.Now;
            cmbAlumno.Focus();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                int alumnoId = (int)cmbAlumno.SelectedValue;
                int ejemplarId = (int)cmbEjemplar.SelectedValue;

                _prestamoService.CreatePrestamo(alumnoId, ejemplarId);
                MessageBox.Show("Préstamo registrado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEjemplares();
                CargarPrestamos();
                HabilitarCampos(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un préstamo para devolver", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var prestamo = (Prestamo)dgvPrestamos.SelectedRows[0].DataBoundItem;

            if (prestamo.Devuelto)
            {
                MessageBox.Show("Este préstamo ya fue devuelto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Confirma la devolución de este préstamo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _prestamoService.DevolverPrestamo(prestamo.Id);
                MessageBox.Show("Devolución registrada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEjemplares();
                CargarPrestamos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al devolver: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarCampos(false);
        }

        private void HabilitarCampos(bool habilitar)
        {
            cmbAlumno.Enabled = habilitar;
            cmbEjemplar.Enabled = habilitar;
            dtpFechaPrestamo.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }

        private bool ValidarCampos()
        {
            if (cmbAlumno.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un alumno", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAlumno.Focus();
                return false;
            }

            if (cmbEjemplar.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un ejemplar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEjemplar.Focus();
                return false;
            }

            return true;
        }
    }
}
