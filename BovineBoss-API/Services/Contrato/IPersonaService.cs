using BovineBoss_API.Models.DB;
namespace BovineBoss_API.Services.Contrato
{
    public interface IPersonaService
    {

        Task<List<Persona>> GetList();

        Task<Persona> GetPersona(int idPersona);



    }
}
