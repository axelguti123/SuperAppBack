using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int _recordsPorPagina = 10;
        private readonly int _cantidadMaxima = 50;
        public int RecordsPorPagina
        {
            get { return _recordsPorPagina; }
            set { _recordsPorPagina = (value>50) ? _cantidadMaxima:value;}
        }
    }
}
