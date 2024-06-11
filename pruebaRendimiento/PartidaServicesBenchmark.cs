using AutoMapper;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SuperApp.Services.Sevices;
using SuperApp.Services.Utilities;

namespace pruebaRendimiento
{
    public class PartidaServicesBenchmark
    {
        private PartidaServices _partidaServices;
        public PartidaServicesBenchmark()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddTransient<UOF>();
            services.AddScoped<EspecialidadServices>();
            services.AddScoped<UsuarioServices>();
            services.AddScoped<PartidaServices>();
            var serviceProvider = services.BuildServiceProvider();

            // Resolver el servicio PartidaServices del contenedor
            _partidaServices = serviceProvider.GetRequiredService<PartidaServices>();
        }
        [Benchmark]
        public async Task<ResponseDTO<IEnumerable<MostrarPartidaDTO>>> GetAllBenchmark()
        {
            var result = await _partidaServices.GetAll();
            Console.WriteLine(result);
            return result;
        }
        [Benchmark]
        public async Task<ResponseDTO> UpdateBenchMark()
        {
            MostrarPartidaDTO partida = new MostrarPartidaDTO()
            {
                CodPartida="07.01.01",
                partida="Comunicaciones",
                Und="Und",
                Total=25
            };
            var response = await _partidaServices.Update(partida);
            return response;
        }
    }
}
