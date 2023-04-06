using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface IAdminService
    {

        Task<List<EmployeeDto>> GetListAdmin();

        Task<EmployeeDto> GetPersona(int idPersona);

       // Task<Persona> AddAdministrator(CreateEmployeeDto Admin);

        Task<LoginPersonaDTO> GetUser(string usuario);


        Task<ActiveAdminDto> ActiveAdmin(CreateEmployeeDto Admin);



    }
}
