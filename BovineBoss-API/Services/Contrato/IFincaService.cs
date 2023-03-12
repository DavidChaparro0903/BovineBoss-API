using BovineBoss_API.Models.DB;
namespace BovineBoss_API.Services.Contrato
{
    public interface IFincaService
    {
        Task<List<Finca>> GetList();

        Task<Finca> GetFinca(int idFinca);

        Task<Finca> AddFinca(Finca finca);

        Task<bool> UpdateFinca(Finca finca);

        Task<bool> DeleteFinca(Finca Finca);

    }
}
