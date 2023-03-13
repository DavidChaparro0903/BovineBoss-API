using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface IAdminService
    {

        Task<List<AdminDto>> GetListAdmin();

        Task<AdminDto> GetPersona(int idPersona);

        Task<CreateEmployeeDto> AddAdministrator(CreateEmployeeDto Admin);





    }
}
