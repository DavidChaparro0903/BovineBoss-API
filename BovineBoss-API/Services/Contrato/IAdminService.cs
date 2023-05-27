using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface IAdminService
    {

        Task<List<EmployeeDto>> GetListAdmin();

        Task<EmployeeDto> GetEmployeeDto(int idPersona);

        Task<LoginPersonaDTO> GetUser(string usuario);


        Task<ActiveAdminDto> ActiveAdmin(CreateEmployeeDto Admin);

       // Task<bool> UpdateTrabajador(ModifyTrabajadorAdminDto trabajador);


        Task<bool> UpdateAdmin(ModifyAdminDto Admin);

        Task<bool> addNewEstateAdmin(CreateNewEstateDto createNewEstateDto);


       // Task<bool> addNewEstate(CreateNewEstateDto createNewEstateDto);



    }
}
