using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;

namespace BovineBoss_API.Services.Implementacion
{
    public class ResService : IResService
    {
        private BovineBossContext dbContext;

        public ResService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Raza> AddRaza(RazaDTO nuevaRaza)
        {
            try
            {
                Raza newRaza = new Raza()
                {
                    NombreRaza = nuevaRaza.NombreRaza
                };
                dbContext.Add(newRaza);
                await dbContext.SaveChangesAsync();
                return newRaza;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Inconveniente> AddInconveniente(IncovenienteDTO incovenienteDTO)
        {
            try
            {
                Inconveniente inconveniente = new Inconveniente()
                {
                    NombreInconveniente = incovenienteDTO.NombreInconveniente
                };
                dbContext.Add(inconveniente);
                await dbContext.SaveChangesAsync();
                return inconveniente;
            }
            catch
            {
                return null;
            }
        }
    }
}
