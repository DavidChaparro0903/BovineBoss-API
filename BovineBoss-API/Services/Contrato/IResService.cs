using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface IResService
    {
        Task<Raza> AddRaza(RazaDTO nuevaRaza);

        Task<Inconveniente> AddInconveniente(IncovenienteDTO incovenienteDTO);
    }
}
