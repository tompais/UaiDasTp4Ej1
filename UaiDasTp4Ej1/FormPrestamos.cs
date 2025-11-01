using SERV;
using DOM;

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

        private async void FormPrestamos_Load(object sender, EventArgs e)
  {
      await CargarAlumnosAsync();
     await CargarEjemplaresAsync();
         await CargarPrestamosAsync();
     HabilitarCampos(false);
        }

     private async Task CargarAlumnosAsync()
        {
     try
       {
 var alumnos = await _alumnoService.GetAllAlumnosAsync();
      cmbAlumno.DataSource = alumnos.Where(a => a.Activo).ToList();
   cmbAlumno.DisplayMember = "Apellido";
     cmbAlumno.ValueMember = "Id";
       }
      catch (Exception ex)
       {
     MessageBox.Show($"Error al cargar alumnos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
   }

  private async Task CargarEjemplaresAsync()
 {
        try
       {
      var ejemplares = await _ejemplarService.GetEjemplaresDisponiblesAsync();
     cmbEjemplar.DataSource = ejemplares.ToList();
         cmbEjemplar.DisplayMember = "NumeroInventario";
            cmbEjemplar.ValueMember = "Id";
     }
       catch (Exception ex)
            {
 MessageBox.Show($"Error al cargar ejemplares: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
 }

   private async Task CargarPrestamosAsync()
 {
       try
    {
   var prestamos = await _prestamoService.GetAllPrestamosAsync();
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

private void btnNuevo_Click(object sender, EventArgs e)
{
   HabilitarCampos(true);
    if (cmbAlumno.Items.Count > 0)
       cmbAlumno.SelectedIndex = 0;
   if (cmbEjemplar.Items.Count > 0)
     cmbEjemplar.SelectedIndex = 0;
    dtpFechaPrestamo.Value = DateTime.Now;
       cmbAlumno.Focus();
 }

        private async void btnGuardar_Click(object sender, EventArgs e)
   {
       if (!ValidarCampos()) return;

       try
         {
 int alumnoId = (int)cmbAlumno.SelectedValue;
    int ejemplarId = (int)cmbEjemplar.SelectedValue;

 await _prestamoService.CreatePrestamoAsync(alumnoId, ejemplarId);
            MessageBox.Show("Préstamo registrado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

       await CargarEjemplaresAsync();
    await CargarPrestamosAsync();
            HabilitarCampos(false);
  }
catch (Exception ex)
  {
     MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
   }
        }

private async void btnDevolver_Click(object sender, EventArgs e)
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
     return;

            try
       {
        await _prestamoService.DevolverPrestamoAsync(prestamo.Id);
       MessageBox.Show("Devolución registrada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

   await CargarEjemplaresAsync();
    await CargarPrestamosAsync();
      }
  catch (Exception ex)
{
     MessageBox.Show($"Error al devolver: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
        }

  private void btnCancelar_Click(object sender, EventArgs e)
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
