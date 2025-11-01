using SERV;
using DOM;

namespace UaiDasTp4Ej1
{
    public partial class FormObras : Form
    {
 private readonly ObraService _obraService;
        private Obra? _obraSeleccionada;

      public FormObras(ObraService obraService)
 {
            InitializeComponent();
     _obraService = obraService;
   }

   private async void FormObras_Load(object sender, EventArgs e)
{
   await CargarObrasAsync();
 LimpiarCampos();
   HabilitarCampos(false);
 }

     private async Task CargarObrasAsync()
   {
       try
     {
var obras = await _obraService.GetAllObrasAsync();
       dgvObras.DataSource = obras.ToList();
     ConfigurarGrilla();
   }
       catch (Exception ex)
    {
    MessageBox.Show($"Error al cargar obras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
 }

private void ConfigurarGrilla()
        {
dgvObras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
     dgvObras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvObras.MultiSelect = false;
       dgvObras.ReadOnly = true;
 }

    private void btnNuevo_Click(object sender, EventArgs e)
{
       _obraSeleccionada = null;
      LimpiarCampos();
    HabilitarCampos(true);
  txtTitulo.Focus();
        }

   private async void btnGuardar_Click(object sender, EventArgs e)
        {
  if (!ValidarCampos()) return;

    try
      {
 var obra = new Obra
    {
         Id = _obraSeleccionada?.Id ?? 0,
  Titulo = txtTitulo.Text.Trim(),
         Autor = txtAutor.Text.Trim(),
Editorial = txtEditorial.Text.Trim(),
    Isbn = txtIsbn.Text.Trim(),
       AnioPublicacion = (int)nudAnioPublicacion.Value,
   Genero = txtGenero.Text.Trim(),
  Activo = chkActivo.Checked
    };

     if (_obraSeleccionada == null)
    {
      await _obraService.CreateObraAsync(obra);
MessageBox.Show("Obra creada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    else
     {
     await _obraService.UpdateObraAsync(obra);
         MessageBox.Show("Obra actualizada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
  }

    await CargarObrasAsync();
     LimpiarCampos();
       HabilitarCampos(false);
       }
  catch (Exception ex)
 {
      MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
        }

private async void btnModificar_Click(object sender, EventArgs e)
    {
       if (dgvObras.SelectedRows.Count == 0)
  {
MessageBox.Show("Seleccione una obra para modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      return;
         }

     _obraSeleccionada = (Obra)dgvObras.SelectedRows[0].DataBoundItem;
   CargarDatosEnCampos(_obraSeleccionada);
       HabilitarCampos(true);
 }

private async void btnEliminar_Click(object sender, EventArgs e)
{
  if (dgvObras.SelectedRows.Count == 0)
{
       MessageBox.Show("Seleccione una obra para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
return;
 }

      if (MessageBox.Show("¿Está seguro que desea eliminar esta obra?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
    return;

            try
            {
       var obra = (Obra)dgvObras.SelectedRows[0].DataBoundItem;
await _obraService.DeleteObraAsync(obra.Id);
   MessageBox.Show("Obra eliminada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
    await CargarObrasAsync();
     LimpiarCampos();
       }
      catch (Exception ex)
    {
 MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
        }

private void btnCancelar_Click(object sender, EventArgs e)
        {
       LimpiarCampos();
       HabilitarCampos(false);
 _obraSeleccionada = null;
        }

    private void LimpiarCampos()
     {
    txtTitulo.Clear();
txtAutor.Clear();
      txtEditorial.Clear();
txtIsbn.Clear();
     nudAnioPublicacion.Value = DateTime.Now.Year;
            txtGenero.Clear();
        chkActivo.Checked = true;
        }

        private void CargarDatosEnCampos(Obra obra)
        {
      txtTitulo.Text = obra.Titulo;
     txtAutor.Text = obra.Autor;
            txtEditorial.Text = obra.Editorial;
         txtIsbn.Text = obra.Isbn;
            nudAnioPublicacion.Value = obra.AnioPublicacion;
txtGenero.Text = obra.Genero;
chkActivo.Checked = obra.Activo;
 }

        private void HabilitarCampos(bool habilitar)
        {
      txtTitulo.Enabled = habilitar;
            txtAutor.Enabled = habilitar;
txtEditorial.Enabled = habilitar;
     txtIsbn.Enabled = habilitar;
     nudAnioPublicacion.Enabled = habilitar;
            txtGenero.Enabled = habilitar;
     chkActivo.Enabled = habilitar;
btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
     }

        private bool ValidarCampos()
 {
   if (string.IsNullOrWhiteSpace(txtTitulo.Text))
{
 MessageBox.Show("El título es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
  txtTitulo.Focus();
   return false;
 }
  if (string.IsNullOrWhiteSpace(txtAutor.Text))
     {
  MessageBox.Show("El autor es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
     txtAutor.Focus();
       return false;
            }
            if (string.IsNullOrWhiteSpace(txtIsbn.Text))
   {
    MessageBox.Show("El ISBN es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      txtIsbn.Focus();
    return false;
    }
            return true;
        }
    }
}
