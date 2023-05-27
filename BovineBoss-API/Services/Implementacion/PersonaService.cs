using Microsoft.EntityFrameworkCore;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using System.Globalization;

namespace BovineBoss_API.Services.Implementacion
{
    public class PersonaService : IAdminService, ITrabajadorService
    {


        private BovineBossContext dbContext;

        public PersonaService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<EmployeeDto>> GetListAdmin()
        {
            try
            {
                var listaPersona = dbContext.Personas.Where(p => p.TipoPersona == "A").ToList();
                List<EmployeeDto> listaAdminDto = listaPersona.Select(a => new EmployeeDto
                {
                    Id = a.IdPersona,
                    NombrePersona = a.NombrePersona,
                    ApellidoPersona = a.ApellidoPersona,
                    Cedula = a.Cedula,
                    Salario = a.Salario,
                    FechaContratacion = a.FechaContratacion,
                    Usuario = a.Usuario,
                    PhoneNumber = a.TelefonoPersona
                }).ToList();
           
                return listaAdminDto;
            }
            catch 
            {
                return null;

            }
        }
        public async Task<List<EmployeeDto>> GetListWorker(int estateId)
        {
            try
            {
                var estateWorkerList = dbContext.TrabajadorFincas.Where(w=> w.IdFinca == estateId);
                var listaPersona = estateWorkerList.Select(p=> new EmployeeDto()
                {
                    Id = p.IdTrabajadorNavigation.IdPersona,
                    NombrePersona = p.IdTrabajadorNavigation.NombrePersona,
                    ApellidoPersona = p.IdTrabajadorNavigation.ApellidoPersona,
                    Cedula = p.IdTrabajadorNavigation.Cedula,
                    Salario = p.IdTrabajadorNavigation.Salario,
                    FechaContratacion = p.IdTrabajadorNavigation.FechaContratacion,
                    Usuario = p.IdTrabajadorNavigation.Usuario,
                    PhoneNumber = p.IdTrabajadorNavigation.TelefonoPersona
                }).ToList();
                return listaPersona;
            }
            catch
            {
                return null;

            }
        }
        private  async Task<Persona> AddAdministrator(CreateEmployeeDto Admin)
        {
            //TODO Agregar autogeneración de usuario
            Persona persona;
            try
            {
                persona = new ()
                {
                    NombrePersona = Admin.NombrePersona,
                    ApellidoPersona = Admin.ApellidoPersona,
                    Cedula = Admin.Cedula,
                    TipoPersona = "A",
                    Salario = Admin.Salario,
                    FechaContratacion = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy"), "MM-dd-yyyy", CultureInfo.InvariantCulture),
                    Usuario = Admin.Usuario,
                    Contrasenia = BCrypt.Net.BCrypt.HashPassword(Admin.Contrasenia),
                    TelefonoPersona = Admin.TelefonoPersona,
                };
                dbContext.Personas.Add(persona);
                await dbContext.SaveChangesAsync();
                return persona;
            }
            catch 
            {
                return null;
            }
        }
        private async Task<AdministradorFinca> AddAdminFinca(int idAdmin,int IdFinca)
        {
            AdministradorFinca administradorFinca;
            try
            {
                administradorFinca = new()
                {
                    EstadoAdministrador = true,
                    IdFinca = IdFinca,
                    IdAdministrador = idAdmin,
                    FechaCambioAdmin = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss"), "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                };

                dbContext.AdministradorFincas.Add(administradorFinca);
                await dbContext.SaveChangesAsync();
                return administradorFinca;

            }
            catch { 
      
                return null;
                 
            }

        }
        public async Task<ActiveAdminDto> ActiveAdmin(CreateEmployeeDto Admin)
        {

            try {
                var exist = dbContext.Fincas.Where(f => f.IdFinca == Admin.IdFinca).FirstOrDefault();

                if (exist != null)
                {
                    Persona personaAgreggate = await AddAdministrator(Admin);
                    AdministradorFinca adminFincaAgregate = await AddAdminFinca(personaAgreggate.IdPersona, Admin.IdFinca);

                    var activeAdminConsult = from a in dbContext.Personas
                                             join af in dbContext.AdministradorFincas
                                             on a.IdPersona equals af.IdAdministrador
                                             join f in dbContext.Fincas
                                             on af.IdFinca equals f.IdFinca
                                             where a.IdPersona == adminFincaAgregate.IdAdministrador
                                             select new ActiveAdminDto
                                             {
                                                 NombrePersona = a.NombrePersona,
                                                 ApellidoPersona = a.ApellidoPersona,
                                                 Cedula = a.Cedula,
                                                 NombreFinca = f.NombreFinca,
                                                 EstadoAdministrador = af.EstadoAdministrador
                                             };

                    return activeAdminConsult.FirstOrDefault();

                }

                return null;
            }
            catch 
            {

                return null;
            }

        }

        public async Task<EmployeeDto> GetEmployeeDto(int idPersona)
        {
            try
            {
                var persona = await dbContext.Personas.Where(e => e.IdPersona == idPersona).FirstOrDefaultAsync();
                EmployeeDto employee = new()
                {
                    Id = persona.IdPersona,
                    NombrePersona = persona.NombrePersona,
                    Cedula = persona.Cedula,
                    ApellidoPersona = persona.ApellidoPersona,
                    Salario = persona.Salario,
                    FechaContratacion = persona.FechaContratacion,
                    Usuario = persona.Usuario
                };

                return employee;

            }
            catch
            {
                return null;
            }
        }

        public async Task<LoginPersonaDTO> GetUser(string usuario)
        {
            try
            {
                var persona = await dbContext.Personas.Where(e => e.Usuario == usuario).FirstOrDefaultAsync();
                LoginPersonaDTO user = new()
                {
                    IdPersona = persona.IdPersona,
                    RolPersona = persona.TipoPersona,
                    Contrasenia = persona.Contrasenia
                };

                return user;

            }
            catch
            {
                return null;


            }
        }
        private async Task<Persona> AddTrabajador(CreateEmployeeDto trabajador)
        {
            Persona persona;
            try
            {
                persona = new()
                {
                    NombrePersona = trabajador.NombrePersona,
                    ApellidoPersona = trabajador.ApellidoPersona,
                    Cedula = trabajador.Cedula,
                    TipoPersona = "T",
                    Salario = trabajador.Salario,
                    FechaContratacion = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy"), "MM-dd-yyyy", CultureInfo.InvariantCulture),
                    Usuario = trabajador.Usuario,
                    Contrasenia = BCrypt.Net.BCrypt.HashPassword(trabajador.Contrasenia),
                    TelefonoPersona = trabajador.TelefonoPersona,
                };
                dbContext.Personas.Add(persona);
                await dbContext.SaveChangesAsync();
                return persona;
            }
            catch
            {
                return null;
            }
        }
        private async Task<TrabajadorFinca> AddTrabajadorFinca(int idTrabajador, int IdFinca)
        {
            TrabajadorFinca trabajadorFinca;
            try
            {
                trabajadorFinca = new()
                {
                    EstadoTrabajador = true,
                    IdFinca = IdFinca,
                    IdTrabajador = idTrabajador,
                    FechaCambioTrabajador = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss"), "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                };

                dbContext.TrabajadorFincas.Add(trabajadorFinca);
                await dbContext.SaveChangesAsync();
                return trabajadorFinca;

            }
            catch
            {

                return null;

            }

        }
        public async Task<ActiveTrabajadorDto> ActiveTrabajador(CreateEmployeeDto trabajador)
        {
            try
            {
                var exist = dbContext.Fincas.Where(f => f.IdFinca == trabajador.IdFinca).FirstOrDefault();


                if (exist != null)
                {
                    Persona personaAgreggate = await AddTrabajador(trabajador);
                    TrabajadorFinca trabajadorFincaAgregate = await AddTrabajadorFinca(personaAgreggate.IdPersona, trabajador.IdFinca);

                    var activeTrabajadorConsult = from a in dbContext.Personas
                                             join at in dbContext.TrabajadorFincas
                                             on a.IdPersona equals at.IdTrabajador
                                             join f in dbContext.Fincas
                                             on at.IdFinca equals f.IdFinca
                                             where a.IdPersona == trabajadorFincaAgregate.IdTrabajador
                                             select new ActiveTrabajadorDto
                                             {
                                                 NombrePersona = a.NombrePersona,
                                                 ApellidoPersona = a.ApellidoPersona,
                                                 Cedula = a.Cedula,
                                                 NombreFinca = f.NombreFinca,
                                                 EstadoTrabajador = at.EstadoTrabajador
                                             };

                    return activeTrabajadorConsult.FirstOrDefault();

                }

                return null;
            }
            catch
            {

                return null;
            }
        }
        public async Task<Persona> GetPersona(int id)
        {

            return await dbContext.Personas.Where(e => e.IdPersona == id).FirstOrDefaultAsync();
     
        }
        public async Task<Finca> GetFinca(int idNuevaFinca)
        {
            return await dbContext.Fincas.Where(f => f.IdFinca == idNuevaFinca).FirstOrDefaultAsync();

        }
        public async Task<bool> UpdateTrabajador(ModifyTrabajadorDto trabajador)
        {

            try
            {
                /*Se observa que el id de la persona exista y se recupera sus otros datos que no se pueden 
             *modificar
             */
                Persona personFound = await GetPersona(trabajador.Id);
                if (personFound != null && personFound.TipoPersona == "T")
                {
                    await saveChangesTrabajador(trabajador, personFound);
                    return true;
                }
                return false;

            }catch (DbUpdateConcurrencyException )
            {
                throw;

            }

         }
        public async Task<bool> UpdateTrabajador(ModifyTrabajadorAdminDto trabajador)
        {

            try
            {
                /*Se observa que el id de la persona exista y se recupera sus otros datos que no se pueden 
             *modificar
             */
                Persona personFound = await GetPersona(trabajador.Id);
                //Finca fincaFound = await GetFinca(trabajador.idFincaNuevo);
                if (personFound != null)
                {
                    await saveChangesTrabajador(trabajador, personFound);
                  //  await saveChangesTrabajadorFinca(trabajador);
                    return true;
                }
                return false;

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

        }
        /*
         Metodo utilizado en el rol trabajador para poder modificarse
        */
        public async Task saveChangesTrabajador(ModifyTrabajadorDto trabajador, Persona personFound)
        {
            personFound.NombrePersona = trabajador.NombrePersona;
            personFound.ApellidoPersona = trabajador.ApellidoPersona;
            personFound.Cedula = trabajador.Cedula;
            personFound.TelefonoPersona = trabajador.TelefonoPersona;
            personFound.Usuario = trabajador.Usuario;
            personFound.Salario = trabajador.Salario;
            dbContext.Personas.Update(personFound);
            await dbContext.SaveChangesAsync();
        }
        /*
         Metodo utilizado en el rol administrador para modificar un trabajador
         */
        public async Task saveChangesTrabajador(ModifyTrabajadorAdminDto trabajador, Persona personFound)
        {
            personFound.NombrePersona = trabajador.NombrePersona;
            personFound.ApellidoPersona = trabajador.ApellidoPersona;
            personFound.Cedula = trabajador.Cedula;
            personFound.TelefonoPersona = trabajador.TelefonoPersona;
            personFound.Usuario = trabajador.Usuario;
            personFound.Contrasenia = BCrypt.Net.BCrypt.HashPassword(trabajador.Contrasenia);
            personFound.Salario = trabajador.Salario;
            dbContext.Personas.Update(personFound);

            await dbContext.SaveChangesAsync();
        }
        public async Task<bool> UpdateAdmin(ModifyAdminDto Admin)
        {
            try
            {
                Persona persona = await GetPersona(Admin.IdPersona);
                if (persona != null && persona.TipoPersona == "A")
                {
                    persona.NombrePersona = Admin.NombrePersona;
                    persona.ApellidoPersona = Admin.ApellidoPersona;
                    persona.Cedula = Admin.Cedula;
                    persona.TelefonoPersona = Admin.TelefonoPersona;
                    persona.Salario = Admin.Salario;
                    persona.Usuario = Admin.Usuario;
                    dbContext.Personas.Update(persona);
                    await dbContext.SaveChangesAsync();
                    return true;

                }
                return false;

            }
            catch
            {
                return false;

            }
        }
        /**
        Observamos que el empleado no este activo en una finca, si esta activo en almenos una
        retorna false de lo contrario retorna true
        */
        public async Task<bool> employeeIsNotActiveInStates(int idEmployee, List<int> listIdState)
        {
            foreach (int idState in listIdState)
            {
                bool result = await employeeIsActiveInState(idEmployee, idState);
                if (result)
                {
                    return false;
                }

            }

            return true;

        }


        public async Task<bool> adminIsNotActiveInStates(int idAdmin, List<int> listIdState)
        {
            foreach (int idState in listIdState)
            {
                bool result = await adminIsActiveInState(idAdmin, idState);
                if (result)
                {
                    return false;
                }

            }

            return true;

        }

        /*
         Tenemos que comprobar que el trabajador no se pueda agregar a una finca en la que ya se encuentra y esta habilitado
        */
        public async Task<bool> addNewEstate(CreateNewEstateDto createNewEstateDto)
        {
            bool existIdState = await stateExists(createNewEstateDto.ListIdState);
            bool employeeIsNotActiveListState = await employeeIsNotActiveInStates(createNewEstateDto.IdEmployee, createNewEstateDto.ListIdState);
            bool resultIsEmployee = await isEmployee(createNewEstateDto.IdEmployee);
            if (existIdState && employeeIsNotActiveListState == true && resultIsEmployee)
            {
                foreach(int idState  in createNewEstateDto.ListIdState)
                {
                    TrabajadorFinca trabajadorFinca = new()
                    {
                        IdFinca = idState,
                        IdTrabajador = createNewEstateDto.IdEmployee,
                        FechaCambioTrabajador = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss"), "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        EstadoTrabajador = true
                    };
                    dbContext.TrabajadorFincas.Add(trabajadorFinca);
                }

                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<bool> addNewEstateAdmin(CreateNewEstateDto createNewEstateDto)
        {
            bool existIdState = await stateExists(createNewEstateDto.ListIdState);
            bool AdminIsNotActiveListState = await adminIsNotActiveInStates(createNewEstateDto.IdEmployee, createNewEstateDto.ListIdState);
            bool resultIsAdmin = await isAdmin(createNewEstateDto.IdEmployee);
            if (existIdState && AdminIsNotActiveListState == true && resultIsAdmin)
            {
                foreach (int idState in createNewEstateDto.ListIdState)
                {
                    AdministradorFinca administradorFinca = new()
                    {
                        IdFinca = idState,
                        IdAdministrador = createNewEstateDto.IdEmployee,
                        FechaCambioAdmin = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss"), "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        EstadoAdministrador = true
                    };
                    dbContext.AdministradorFincas.Add(administradorFinca);
                }
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        private async Task<bool> stateExists(List<int> listIdState)
        {
            foreach(int idState in listIdState)
            {
                Finca finca = await GetFinca(idState);
                if (finca == null)
                {

                    return false;
                }
            }
            return true;

        }
        private async Task<bool> employeeIsActiveInState(int idEmployee, int idState)
        {
            var isActive = from p in dbContext.Personas
                           join t in dbContext.TrabajadorFincas
                           on p.IdPersona equals t.IdTrabajador
                           where p.TipoPersona == "T" &&
                           t.EstadoTrabajador == true &&
                           t.IdTrabajador == idEmployee &&
                           t.IdFinca == idState
                           select new {p.IdPersona, p.NombrePersona, t.EstadoTrabajador,t.IdFinca };
    

            if (isActive.FirstOrDefault() != null)
            {
              
                return true;
            }
            return false;
        }


        private async Task<bool> adminIsActiveInState(int idEmployee, int idState)
        {
            var isActive = from p in dbContext.Personas
                           join a in dbContext.AdministradorFincas
                           on p.IdPersona equals a.IdAdministrador
                           where p.TipoPersona == "A" &&
                           a.EstadoAdministrador == true &&
                           a.IdAdministrador == idEmployee &&
                           a.IdFinca == idState
                           select new { p.IdPersona, p.NombrePersona, a.EstadoAdministrador, a.IdFinca };


            if (isActive.FirstOrDefault() != null)
            {

                return true;
            }
            return false;
        }


        public async Task<bool> isEmployee(int idEmployee)
        {
            var employee = from p in dbContext.Personas
                           where p.IdPersona == idEmployee
                             && p.TipoPersona == "T"
                           select new { p.IdPersona, p.TipoPersona };
      
            if (await employee.FirstOrDefaultAsync() != null)
            {
                return true;
            }
            return false;
                
         }


        public async Task<bool> isAdmin(int idAdmin)
        {
            var employee = from p in dbContext.Personas
                           where p.IdPersona == idAdmin
                             && p.TipoPersona == "A"
                           select new { p.IdPersona, p.TipoPersona };

            if (await employee.FirstOrDefaultAsync() != null)
            {
                return true;
            }
            return false;

        }

        public Task<IEnumerable<OwnerDTO>> GetOwners()
        {
            try
            {
                var estateWorkerList = dbContext.Personas.Where(w => w.TipoPersona== "P");
                var listaPersona = estateWorkerList.Select(p => new OwnerDTO()                
	{
                    Id = p.IdPersona,
                    NombrePersona = p.NombrePersona,
                    ApellidoPersona = p.ApellidoPersona,
                    Cedula = p.Cedula
                }).AsEnumerable();
                return Task.FromResult(listaPersona);
            }
            catch
            {
                return null;

            }
        }

        //public async Task saveChangesTrabajadorFinca(ModifyTrabajadorAdminDto trabajador)
        //{
        //    TrabajadorFinca trabajadorFinca = new()
        //    {
        //        IdFinca = trabajador.idFincaNuevo,
        //        IdTrabajador = trabajador.Id,
        //        FechaCambioTrabajador = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss"), "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
        //        EstadoTrabajador = true
        //    };
        //    dbContext.Add(trabajadorFinca);
        //    await dbContext.SaveChangesAsync();
        //}

    }

}
