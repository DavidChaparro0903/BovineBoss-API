using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface ITrabajadorService
    {
        Task<List<EmployeeDto>> GetListTrabajador();

        Task<EmployeeDto> GetPersona(int idPersona);

        Task<CreateEmployeeDto> AddTrabajador(CreateEmployeeDto trabajador);

        Task<LoginPersonaDTO> GetUser(string usuario);


        Task<ActiveAdminDto> ActiveTrabajador(CreateEmployeeDto trabajador);



    }
}
