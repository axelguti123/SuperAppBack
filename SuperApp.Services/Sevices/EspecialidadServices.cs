using AutoMapper;
using Enums;
using Microsoft.Extensions.Logging;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SupperApp.Models;

namespace SuperApp.Services.Sevices
{
    public class EspecialidadServices(IMapper mapper,UOF uof)
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly UOF _uof = uof ?? throw new ArgumentNullException(nameof(uof));

        public async Task<ResponseDTO<IEnumerable<MostrarEspecialidadDTO>>> GetAll()
        {
            var response = new ResponseDTO<IEnumerable<MostrarEspecialidadDTO>>();
            try
            {
                var especialidades = await _uof.Especialidad.GetAll();
                response = _mapper.Map<ResponseDTO<IEnumerable<MostrarEspecialidadDTO>>>(especialidades);
            }
            catch (Exception ex)
            {
                response.Status = Status.Error.ToString();
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO> Create(CrearEspecialidadDTO especialidadDTO)
        {
            var responseDTO=new ResponseDTO();
            try
            {
                var especialidad= _mapper.Map<Especialidad>(especialidadDTO);
                var response = await _uof.Especialidad.Create(especialidad);
                responseDTO=_mapper.Map<ResponseDTO>(response);
            }
            catch (Exception ex)
            {
                responseDTO.Status = Status.Error.ToString();
                responseDTO.Message = ex.Message;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Delete(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var especialidad = await _uof.Especialidad.Delete(id);
                response=_mapper.Map<ResponseDTO>(especialidad);
            }
            catch (Exception ex)
            {
                response.Status = Status.Error.ToString();
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO<MostrarEspecialidadDTO>> Find(int id)
        {
            var response = new ResponseDTO<MostrarEspecialidadDTO>();
            try
            {
                var especialidad = await _uof.Especialidad.Find(id);
                response= _mapper.Map<ResponseDTO<MostrarEspecialidadDTO>>(especialidad);
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO> Update(ModificarEspecialidadDTO especialidadDTO)
        {
            var responseDTO = new ResponseDTO();
            try
            {

                var especialidad = _mapper.Map<Especialidad>(especialidadDTO);
                var response = await _uof.Especialidad.Update(especialidad);
                responseDTO = _mapper.Map<ResponseDTO>(response);
            }catch(Exception ex)
            {
                responseDTO.Status = "Error";
                responseDTO.Message = ex.Message;
            }
            return responseDTO;
        }
    }
}
