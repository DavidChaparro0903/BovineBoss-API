using Microsoft.EntityFrameworkCore;
using BovineBoss_API.Models;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Models.DB;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace BovineBoss_API.Services.Implementacion
{
    public class PersonaService : IPersonaService
    {


        private BovineBossContext dbContext;

        public PersonaService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Persona>> GetList()
        {

            try
            {
                List<Persona> listaPersona = new List<Persona>();
                listaPersona = await dbContext.Personas.ToListAsync();
                return listaPersona;
            }
            catch (Exception ex)
            {
                throw ex;

            }



        }

        public async Task<Persona> GetPersona(int idPersona)
        {
            try
            {
                Persona? persona = new Persona();
                persona = await dbContext.Personas.Where(e => idPersona == idPersona).FirstOrDefaultAsync();
                return persona;

            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        public async Task AddPersona(Persona persona)
        {
            try
            {
                dbContext.Personas.AddAsync(persona);
                await dbContext.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}