using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface ITrabajadorService
    {
        Task<List<EmployeeDto>> GetListWorker(int estateId);
        Task<IEnumerable<OwnerDTO>> GetOwners();

        Task<ActiveTrabajadorDto> ActiveTrabajador(CreateEmployeeDto trabajador);

        Task<bool> UpdateTrabajador(ModifyTrabajadorDto trabajador);


        Task<Persona> GetPersona(int id);


        Task<bool> UpdateTrabajador(ModifyTrabajadorAdminDto trabajador);


        Task<bool> addNewEstate(CreateNewEstateDto createNewEstateDto);

    }
}
