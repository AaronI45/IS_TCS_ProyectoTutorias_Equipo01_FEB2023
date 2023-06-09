using ServiciosTutorias;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendGestorTutorias.modelo
{
    internal class ComentarioGeneralViewModel
    {
        public ObservableCollection<Comentario> ComentariosBD { get; set; }

        public ComentarioGeneralViewModel()
        {
            ComentariosBD = new ObservableCollection<Comentario>();
            solicitarInformacionServicio();
        }

        private async void solicitarInformacionServicio()
        {
            var conexionServicio = new ServiciosTutorias.Service1Client();
            if (conexionServicio != null)
            {
                Comentario[] comentarios = await conexionServicio.recuperarComentariosAsync();
                foreach (Comentario comentario in comentarios)
                {
                    ComentariosBD.Add(comentario);
                }
            }
        }
    }
}
