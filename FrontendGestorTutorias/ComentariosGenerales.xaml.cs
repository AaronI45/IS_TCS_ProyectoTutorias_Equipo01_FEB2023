using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontendGestorTutorias
{
    /// <summary>
    /// Lógica de interacción para ComentariosGenerales.xaml
    /// </summary>
    public partial class ComentariosGenerales : Window
    {
        public ComentariosGenerales()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cancelar la operación?", "Cancelar",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                ReporteTutoriaAcademica ventanaReporte = new ReporteTutoriaAcademica();
                ventanaReporte.Show();
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
