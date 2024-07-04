using SuperApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.Utilities
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Paginar<T>(this IEnumerable<T> list,PaginacionDTO paginacion){
            return list.Skip((paginacion.Pagina-1)*paginacion.RecordsPorPagina).Take(paginacion.RecordsPorPagina);
        }
    }
}
