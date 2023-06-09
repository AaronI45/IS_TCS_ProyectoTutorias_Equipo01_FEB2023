using ServiciosTutorias;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendGestorTutorias.modelo
{
    internal class EstudianteTutorViewModel
    {
        public ObservableCollection<Estudiante> EstudiantesBd { get; set; }
        public EstudianteTutorViewModel()
        {
            EstudiantesBd = new ObservableCollection<Estudiante>();
            solicitarInformacionServicio();
        }

        private async void solicitarInformacionServicio()
        {
            var conexionServicio = new ServiciosTutorias.Service1Client();
            if(conexionServicio != null)
            {
                Estudiante[] estudiantes = await conexionServicio.recuperarEstudiantesAsync();
                foreach(Estudiante estudiante in estudiantes)
                {
                    if(estudiante.academico_idAcademico != null)
                    {
                        estudiante.Academico = await conexionServicio.recuperarAcademicoPorIdAsync((int)estudiante.academico_idAcademico);
                        EstudiantesBd.Add(estudiante);
                    }
                }
            }
        }
    }
}
