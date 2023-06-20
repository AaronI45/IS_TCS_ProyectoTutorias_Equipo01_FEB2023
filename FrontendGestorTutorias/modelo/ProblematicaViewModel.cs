using ServiciosTutorias;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendGestorTutorias.modelo
{
    internal class ProblematicaViewModel
    {
        public ObservableCollection<Problematica> problematicasBD { get; set; }
        public ProblematicaViewModel()
        {
            problematicasBD = new ObservableCollection<Problematica>();
            solicitarInformacionServicio();
        }
        private async void solicitarInformacionServicio()
        {
            var conexionServicios = new Service1Client();
            if (conexionServicios != null)
            {
                Problematica[] problematicas = await conexionServicios.obtenerProblematicasAsync();
                foreach (Problematica problematica in problematicas)
                {
                    problematicasBD.Add(problematica);
                }
            }
        }
    }
}
