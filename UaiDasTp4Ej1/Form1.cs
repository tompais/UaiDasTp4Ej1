using Microsoft.Extensions.DependencyInjection;
using APP;

namespace UaiDasTp4Ej1
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public Form1()
        {
            InitializeComponent();

            // Configurar inyección de dependencias
            _serviceProvider = DependencyInjection.ConfigureServices(Configuration.ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Sistema de Biblioteca - Listo";
        }

        // Métodos para abrir formularios hijos
        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormAlumnos>("Gestión de Alumnos");
        }

        private void obrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormObras>("Gestión de Obras");
        }

        private void ejemplaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormEjemplares>("Gestión de Ejemplares");
        }

        private void prestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo<FormPrestamos>("Gestión de Préstamos");
        }

        // Métodos de ventanas MDI
        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
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
