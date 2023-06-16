using ServiciosTutorias;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendGestorTutorias.modelo
{
    internal class EstudiantesAsistentesViewModel
    {
        public ObservableCollection<Estudiante> EstudiantesAsistentesBd { get; set; }
        public EstudiantesAsistentesViewModel(int idTutoria, int idTutor)
        {
            EstudiantesAsistentesBd = new ObservableCollection<Estudiante>();
            solicitarInformacionServicio(idTutoria, idTutor);
        }

        private async void solicitarInformacionServicio(int idTutoria, int idTutor)
        {
            var conexionServicio = new ServiciosTutorias.Service1Client();
            if (conexionServicio != null)
            {
                Estudiante[] estudiantes = await conexionServicio.recuperarEstudiantesAsistentesAsync(idTutoria);
                foreach (Estudiante estudiante in estudiantes)
                {
                    if (idTutor == estudiante.academico_idAcademico)
                    {
                        EstudiantesAsistentesBd.Add(estudiante);
                    }
                }
            }
        }
    }
}
