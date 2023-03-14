using BovineBoss_API.Models.DB;
namespace BovineBoss_API.Services.Contrato
{
    public interface IPersonaService
    {

        Task<List<Persona>> GetList();

        Task<Persona> GetPersona(int idPersona);

        Task<Persona> GetPersona(String usuario);

        Task AddPersona(Persona persona);

    }
}
