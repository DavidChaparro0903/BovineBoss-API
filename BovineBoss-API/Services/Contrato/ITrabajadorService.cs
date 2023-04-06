using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface ITrabajadorService
    {
        Task<List<EmployeeDto>> GetListTrabajador();

        Task<ActiveTrabajadorDto> ActiveTrabajador(CreateEmployeeDto trabajador);



    }
}
