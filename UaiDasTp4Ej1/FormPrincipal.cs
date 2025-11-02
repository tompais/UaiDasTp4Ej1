using APP;
using Microsoft.Extensions.DependencyInjection;

namespace UaiDasTp4Ej1
{
    public partial class FormPrincipal : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public FormPrincipal()
        {
            InitializeComponent();

            // Configurar inyección de dependencias
            _serviceProvider = DependencyInjection.ConfigureServices(Configuration.ConnectionString);
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Sistema de Biblioteca - Listo";
        }

        // Métodos para abrir formularios hijos
        private void AlumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormAlumnos>("Gestión de Alumnos");
        }

        private void ObrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormObras>("Gestión de Obras");
        }

        private void EjemplaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormEjemplares>("Gestión de Ejemplares");
        }

        private void PrestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormPrestamos>("Gestión de Préstamos");
        }

        // Métodos de ventanas MDI
        private void CascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void HorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void VerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Sistema de Gestión de Biblioteca\n\n" +
                  "Versión 1.0\n\n" +
          "Trabajo Práctico 4 - Ejercicio 1\n" +
            "Desarrollo y Arquitectura de Software\n\n" +
            "© 2025",
          "Acerca de",
             MessageBoxButtons.OK,
         MessageBoxIcon.Information);
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir?", "Confirmar",
  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Método auxiliar para abrir formularios hijos
        private void AbrirFormularioHijo<T>(string titulo) where T : Form
        {
            // Verificar si ya está abierto
            foreach (Form form in MdiChildren)
            {
                if (form is T)
                {
                    form.Activate();
                    return;
                }
            }

            // Crear nueva instancia usando el service provider
            var formulario = ActivatorUtilities.CreateInstance<T>(_serviceProvider);
            formulario.MdiParent = this;
            formulario.Text = titulo;
            formulario.Show();
        }
    }
}
