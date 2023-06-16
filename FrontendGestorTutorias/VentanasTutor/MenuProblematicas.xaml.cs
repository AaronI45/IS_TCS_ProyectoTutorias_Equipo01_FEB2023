using ServiciosTutorias;
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
    /// Interaction logic for ProblematicasAcademicas.xaml
    /// </summary>
    public partial class MenuProblematicas : Window
    {
        Academico tutorIniciado;
        public MenuProblematicas(Academico tutorIniciado)
        {
            this.tutorIniciado = tutorIniciado;
            InitializeComponent();
        }

    }
}
