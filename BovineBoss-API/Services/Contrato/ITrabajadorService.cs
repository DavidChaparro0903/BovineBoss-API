using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface ITrabajadorService
    {
        Task<List<EmployeeDto>> GetListTrabajador();

        Task<ActiveTrabajadorDto> ActiveTrabajador(CreateEmployeeDto trabajador);

        Task<bool> UpdateTrabajador(ModifyTrabajadorDto trabajador);


        Task<Persona> GetPersona(int id);




    }
}
