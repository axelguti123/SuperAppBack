using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    public class CrearPartidaDTO
    {
        public string IDPadre { get; set; }
        public int IDEspecialidad {  get; set; }
        public string CodPartida { get; set; }
        public string partida { get; set; }
        public string Und { get; set; }
        public decimal Total { get; set; }

    }
}
