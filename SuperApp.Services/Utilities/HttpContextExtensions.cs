using Microsoft.AspNetCore.Http;
namespace SuperApp.Services.Utilities
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarPaginacion<T>(this HttpContext httpcontext,IEnumerable<T> list)
        {
            ArgumentNullException.ThrowIfNull(httpcontext);
            var cantidad = list.Count();
            httpcontext.Response.Headers.Add("CantidadTotalRegistro",cantidad.ToString());
        }
    }
}
