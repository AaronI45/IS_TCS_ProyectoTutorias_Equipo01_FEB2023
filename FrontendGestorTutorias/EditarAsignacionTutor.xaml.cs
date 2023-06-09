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
    /// Interaction logic for EditarAsignacionTutor.xaml
    /// </summary>
    public partial class EditarAsignacionTutor : Window
    {
        int idProgramaEducativo;
        public EditarAsignacionTutor(int idProgramaEducativo)
        {
            InitializeComponent();
            this.idProgramaEducativo = idProgramaEducativo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
