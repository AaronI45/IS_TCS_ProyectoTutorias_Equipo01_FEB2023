using ServiciosTutorias;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendGestorTutorias.modelo
{
    
    internal class ReporteTutoriaViewModel
    {
        public ObservableCollection<ReporteTutoria> TutoriasBD { get; set; }
        public ReporteTutoriaViewModel(int idTutor)
        {
            TutoriasBD = new ObservableCollection<ReporteTutoria>();
            solicitarInformacionServicio(idTutor);
        }

        private async void solicitarInformacionServicio(int idTutor)
        {
            var conexionServicio = new ServiciosTutorias.Service1Client();
            if (conexionServicio != null)
            {
                ReporteTutoria[] listaTutorias = await conexionServicio.recuperarReportesPorTutorAsync(idTutor);
                foreach (ReporteTutoria reporteTutoria in listaTutorias)
                {
                    reporteTutoria.ProgramaEducativo = await conexionServicio.recuperarProgramaEducativoPorIdAsync(reporteTutoria.programa_educativo_idPrograma_educativo);
                    reporteTutoria.Tutoria = await conexionServicio.recuperarTutoriaPorIdAsync(reporteTutoria.tutoria_idTutoria);
                    reporteTutoria.Tutoria.PeriodoEscolar = await conexionServicio.recuperarPeriodoEscolarPorIdAsync(reporteTutoria.Tutoria.periodo_escolar_idPeriodo_escolar);
                    TutoriasBD.Add(reporteTutoria);
                }
            }
        }
    }
}
