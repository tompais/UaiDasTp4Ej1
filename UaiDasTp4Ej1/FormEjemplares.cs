using DOM;
using SERV;

namespace UaiDasTp4Ej1
{
    public partial class FormEjemplares : Form
    {
        private readonly EjemplarService _ejemplarService;
        private readonly ObraService _obraService;
        private Ejemplar? _ejemplarSeleccionado;

        public FormEjemplares(EjemplarService ejemplarService, ObraService obraService)
        {
            InitializeComponent();
            _ejemplarService = ejemplarService;
            _obraService = obraService;
        }

        private void FormEjemplares_Load(object sender, EventArgs e)
        {
            CargarObras();
            CargarEjemplares();
            LimpiarCampos();
            HabilitarCampos(false);
        }

        private void CargarObras()
        {
            try
            {
                var obras = _obraService.GetAllObras();
                cmbObra.DataSource = obras.Where(o => o.Activo).ToList();
                cmbObra.DisplayMember = "Titulo";
                cmbObra.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar obras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEjemplares()
        {
            try
            {
                var ejemplares = _ejemplarService.GetAllEjemplares();
                dgvEjemplares.DataSource = ejemplares.ToList();
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ejemplares: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvEjemplares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEjemplares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEjemplares.MultiSelect = false;
            dgvEjemplares.ReadOnly = true;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            _ejemplarSeleccionado = null;
            LimpiarCampos();
            HabilitarCampos(true);
            txtNumeroInventario.Focus();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                var ejemplar = new Ejemplar
                {
                    Id = _ejemplarSeleccionado?.Id ?? 0,
                    ObraId = (int)cmbObra.SelectedValue,
                    NumeroInventario = txtNumeroInventario.Text.Trim(),
                    Precio = nudPrecio.Value,
                    Disponible = chkDisponible.Checked,
                    Activo = chkActivo.Checked
                };

                if (_ejemplarSeleccionado == null)
                {
                    _ejemplarService.CreateEjemplar(ejemplar);
                    MessageBox.Show("Ejemplar creado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _ejemplarService.UpdateEjemplar(ejemplar);
                    MessageBox.Show("Ejemplar actualizado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarEjemplares();
                LimpiarCampos();
                HabilitarCampos(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEjemplares.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un ejemplar para modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _ejemplarSeleccionado = (Ejemplar)dgvEjemplares.SelectedRows[0].DataBoundItem;
            CargarDatosEnCampos(_ejemplarSeleccionado);
            HabilitarCampos(true);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEjemplares.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un ejemplar para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro que desea eliminar este ejemplar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var ejemplar = (Ejemplar)dgvEjemplares.SelectedRows[0].DataBoundItem;
                _ejemplarService.DeleteEjemplar(ejemplar.Id);
                MessageBox.Show("Ejemplar eliminado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEjemplares();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(false);
            _ejemplarSeleccionado = null;
        }

        private void LimpiarCampos()
        {
            if (cmbObra.Items.Count > 0)
            {
                cmbObra.SelectedIndex = 0;
            }

            txtNumeroInventario.Clear();
            nudPrecio.Value = 0;
            chkDisponible.Checked = true;
            chkActivo.Checked = true;
        }

        private void CargarDatosEnCampos(Ejemplar ejemplar)
        {
            cmbObra.SelectedValue = ejemplar.ObraId;
            txtNumeroInventario.Text = ejemplar.NumeroInventario;
            nudPrecio.Value = ejemplar.Precio;
            chkDisponible.Checked = ejemplar.Disponible;
            chkActivo.Checked = ejemplar.Activo;
        }

        private void HabilitarCampos(bool habilitar)
        {
            cmbObra.Enabled = habilitar;
            txtNumeroInventario.Enabled = habilitar;
            nudPrecio.Enabled = habilitar;
            chkDisponible.Enabled = habilitar;
            chkActivo.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }

        private bool ValidarCampos()
        {
            if (cmbObra.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una obra", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbObra.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroInventario.Text))
            {
                MessageBox.Show("El número de inventario es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroInventario.Focus();
                return false;
            }

            if (nudPrecio.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a cero", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrecio.Focus();
                return false;
            }

            return true;
        }
    }
}
