using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BovineBoss_API.Services.Implementacion
{
    public class ResService : IResService
    {
        private BovineBossContext dbContext;

        public ResService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Raza>> GetRazas()
        {
            return await dbContext.Razas.ToListAsync();
        }
        public async Task<IEnumerable<FullBullDto>> GetBullsNotSold(int stateId)
        {
            return await dbContext.Reses.Where(r => r.IdFinca == stateId && r.IdVenta == null).Select(p => new FullBullDto()
            {
                id = p.IdRes,
                idFinca = p.IdFinca,
                NombreRes = p.NombreRes,
                Color = p.Color,
                FechaNacimiento = p.FechaNacimiento,
                listRazas = p.ResRazas.Select(o => new RazaResDTO()
                {
                    idRaza = o.IdRaza,
                    PorcentajeRaza = o.PorcentajeRaza,
                    NombreRaza = o.IdRazaNavigation.NombreRaza
                }).ToList(),
                listOwner = p.Adquisiciones.Select(o => o.IdPropietarioNavigation).ToList(),
                ComisionesPagada = p.Adquisiciones.FirstOrDefault().ComisionesPagada,
                CostoCompraRes = p.Adquisiciones.FirstOrDefault().CostoCompraRes,
                DescripcionAdquisicion = p.Adquisiciones.FirstOrDefault().DescripcionAdquisicion,
                PrecioFlete = p.Adquisiciones.FirstOrDefault().PrecioFlete,
                DrawBacks = p.ResInconvenientes.ToList()
            }).AsNoTracking().ToListAsync();
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

        public async Task<bool> UpdateInconveniente(ModifyInconvenienteDto inconvenienteDto)
        {
            try
            {
             /*Se observa que el id de la persona exista y se recupera sus otros datos que no se pueden 
             *modificar
             */
                Inconveniente inconveniente = await dbContext.Inconvenientes.FirstOrDefaultAsync(i => i.IdInconveniente == inconvenienteDto.IdInconveniente);
                inconveniente.NombreInconveniente = inconvenienteDto.NombreNuevoInconveniente;
                if (inconveniente != null)
                {
                    dbContext.Inconvenientes.Update(inconveniente);
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

        public async Task<CreateResDto> AddRes(CreateResDto nuevaResDTO)
        {
            if (checkRazas(nuevaResDTO.listRazas)) 
                try
                {

                    Rese newRes = new Rese()
                    {
                        IdFinca = nuevaResDTO.idFinca,
                        NombreRes = nuevaResDTO.NombreRes,
                        Color = nuevaResDTO.Color,
                        FechaNacimiento = nuevaResDTO.FechaNacimiento
                    };
                    dbContext.Reses.Add(newRes);

                    await dbContext.SaveChangesAsync();

                    int idRes = dbContext.Entry(newRes).Entity.IdRes;
                    //Una vez se crea la res en la base de datos se usa el id para las entidades debiles
                    CreateListRazas(nuevaResDTO.listRazas, idRes);
                    await CreateListOwners(new AdquisicionDTO()
                    {
                        owners = nuevaResDTO.listOwner,
                        idRes = idRes,
                        CostoCompraRes = nuevaResDTO.CostoCompraRes,
                        descripcionAdquisicion = nuevaResDTO.DescripcionAdquisicion,
                        ComisionesPagada = nuevaResDTO.ComisionesPagada,
                        PrecioFlete = nuevaResDTO.PrecioFlete
                    });

                    return nuevaResDTO;
                }catch
                {
                    return null;
                }
            return null;
        }
        private void CreateListRazas(List<CreateResRaza> listRazas, int idRes)
        {
            foreach (CreateResRaza resRaza in listRazas)
            {
                dbContext.ResRazas.Add(new ResRaza()
                {
                    IdRaza = resRaza.IdRaza,
                    PorcentajeRaza = resRaza.porcentaje,
                    IdRes = idRes
                });
            }

        }

        private async Task CreateListOwners(AdquisicionDTO adquisicionDTO)
        {

            foreach(CreateOwner ownerDTO in adquisicionDTO.owners)
            {
                //Revisar si el Propietario existe en la BD usando la Cedula como criterio
                int idPropietario;
                bool ownerExists = await dbContext.Personas.AnyAsync(p => p.Cedula == ownerDTO.Cedula && p.TipoPersona == "P");
                //En caso de que no exista, agregar el propietario a la 
                if (!ownerExists)
                {
                    Persona owner = new Persona()
                    {
                        NombrePersona = ownerDTO.NombrePersona,
                        ApellidoPersona = ownerDTO.ApellidoPersona,
                        Cedula = ownerDTO.Cedula,
                        TipoPersona = "P"
                    };
                    dbContext.Personas.Add(owner);
                    await dbContext.SaveChangesAsync();
                    idPropietario = dbContext.Entry(owner).Entity.IdPersona;
                }
                else
                {
                    //En caso de que exista, configurar la variable idPropietario 
                    // Observar la parte de los roles y mirar si se puede manejar varios roles, y agregarlos a la base de datos
                    // o la otra es volver a crear el mismo registro en la base de datos pero con otro rol
                    Persona owner = await dbContext.Personas.Where(p => p.Cedula == ownerDTO.Cedula).FirstAsync();
                    idPropietario = owner.IdPersona;
                    
                    //var existingAdquisicion = await dbContext.Adquisiciones.SingleAsync(ad => ad.IdRes == adquisicionDTO.idRes && ad.IdPropietario == idPropietario);
                    //existingAdquisicion.PrecioFlete = adquisicionDTO.PrecioFlete;
                    //existingAdquisicion.CostoCompraRes = adquisicionDTO.CostoCompraRes;
                    //existingAdquisicion.ComisionesPagada = adquisicionDTO.ComisionesPagada;
                    //existingAdquisicion.DescripcionAdquisicion = adquisicionDTO.descripcionAdquisicion;
                    //dbContext.Update(existingAdquisicion);
                }
                //Con el propietario agregado a la BD, se relaciona en la entidad Adquisicion
                Adquisicione adquisicion = new Adquisicione()
                {
                    IdPropietario = idPropietario,
                    IdRes = adquisicionDTO.idRes,
                    CostoCompraRes = adquisicionDTO.CostoCompraRes,
                    DescripcionAdquisicion = adquisicionDTO.descripcionAdquisicion,
                    ComisionesPagada = adquisicionDTO.ComisionesPagada,
                    PrecioFlete = adquisicionDTO.PrecioFlete,
                    FechaAdquisicion = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss"), "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                };
                await dbContext.Adquisiciones.AddAsync(adquisicion);
                await dbContext.SaveChangesAsync();
            }
        }

        private bool checkRazas(List<CreateResRaza> razas)
        {
            //Revisa que la lista de razas exista en la base de 
            foreach (CreateResRaza raza in razas)
            {
                try
                {
                    dbContext.Razas.Where(r => r.IdRaza == raza.IdRaza);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<ModifyResDTO> UpdateRes(ModifyResDTO updatedResDto)
        {
            //Lllama a la base de datos, buscando a la res por su ID
            var existingRes = await dbContext.Reses.FindAsync(updatedResDto.IdRes);

            //Revisa que la res exista
            if (existingRes == null)
            {   
                //Si no existe, retornar nulo y manejar excepción en Controlador
                return null;
            }

            existingRes.IdFinca = updatedResDto.idFinca;
            existingRes.NombreRes = updatedResDto.NombreRes;
            existingRes.Color = updatedResDto.Color;
            existingRes.FechaNacimiento = updatedResDto.FechaNacimiento;

            dbContext.ResRazas.RemoveRange(dbContext.ResRazas.Where(rr => rr.IdRes == existingRes.IdRes));
            CreateListRazas(updatedResDto.listRazas, updatedResDto.IdRes);


            //Elimina la adquisición de la base de datos
            dbContext.Adquisiciones.RemoveRange(dbContext.Adquisiciones.Where(ad => ad.IdRes == existingRes.IdRes));
            //Si la cedula ingresada ya existe en la BD no se hará nada, en caso de no existir se creara un nuevo propietario para la res
            foreach (CreateOwner owner in updatedResDto.listOwner)
            {
                var existingOwner = dbContext.Personas.Where(p => p.Cedula == owner.Cedula).FirstOrDefault();
                if (existingOwner == null)
                {
                    Persona newOwner = new Persona()
                    {
                        NombrePersona = owner.NombrePersona,
                        ApellidoPersona = owner.ApellidoPersona,
                        Cedula = owner.Cedula,
                        TipoPersona = "P"
                    };
                    await dbContext.Personas.AddAsync(newOwner);
                }
            }
            await CreateListOwners(new AdquisicionDTO()
            {
                owners = updatedResDto.listOwner,
                idRes = updatedResDto.IdRes,
                CostoCompraRes = updatedResDto.costoCompraRes,
                descripcionAdquisicion = updatedResDto.DescripcionAdquisicion,
                ComisionesPagada = updatedResDto.ComisionesPagada,
                PrecioFlete = updatedResDto.PrecioFlete
            });

            await dbContext.SaveChangesAsync();

            return updatedResDto;
        }

        public async Task<IEnumerable<Rese>> GetBulls(int stateId)
        {
            return await dbContext.Reses.Where(x=> x.IdFinca==stateId).ToListAsync();
        }

        public async Task<IEnumerable<ResInconveniente>> GetBullInconvenients(int bullId)
        => await dbContext.ResInconvenientes.Where(ri=> ri.IdRes == bullId).ToArrayAsync();
/// <inheritdoc/>

        public async Task<IEnumerable<FullBullDto>> GetFullBull(int stateId) {

            return await dbContext.Reses.Where(r => r.IdFinca == stateId).Select(p => new FullBullDto()
            {
                id = p.IdRes,
                idFinca = p.IdFinca,
                NombreRes = p.NombreRes,
                Color = p.Color,
                FechaNacimiento = p.FechaNacimiento,
                listRazas = p.ResRazas.Select(o=> new RazaResDTO() {
                    idRaza = o.IdRaza,
                    PorcentajeRaza= o.PorcentajeRaza,
                    NombreRaza = o.IdRazaNavigation.NombreRaza
                } ).ToList(),
                listOwner = p.Adquisiciones.Select(o => o.IdPropietarioNavigation).ToList(),
                ComisionesPagada = p.Adquisiciones.FirstOrDefault().ComisionesPagada,
                CostoCompraRes = p.Adquisiciones.FirstOrDefault().CostoCompraRes,
                DescripcionAdquisicion = p.Adquisiciones.FirstOrDefault().DescripcionAdquisicion,
                PrecioFlete = p.Adquisiciones.FirstOrDefault().PrecioFlete,
                DrawBacks = p.ResInconvenientes.ToList()
            }).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Inconveniente>> GetDrawBacks() => dbContext.Inconvenientes.AsEnumerable();

        public async Task<bool> AddResInconvenientes(AddResInconvenientesDto addResInconvenientesDto)
        {
            try
            {
                ResInconveniente resInconveniente = new()
                {

                    IdInconveniente = addResInconvenientesDto.IdInconveniente,
                    IdRes = addResInconvenientesDto.IdRes,
                    FechaInconveniente = addResInconvenientesDto.FechaInconveniente,
                    DineroGastado = addResInconvenientesDto.DineroGastado,
                    DescripcionInconveniente = addResInconvenientesDto.DescripcionInconveniente
                };

                dbContext.ResInconvenientes.Add(resInconveniente);
                await dbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddWeightBull(WeightHistoryBullDto weight)
        {
            try
            {

                HistorialPeso historialPeso = new()
                {

                    IdRes = weight.IdRes,
                    PesoRes = weight.PesoRes,
                    FechaActualizacion = weight.FechaActualizacion
                };

                dbContext.HistorialPesos.Add(historialPeso);
                await dbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public async Task<WeightReportDTO> GetWeightReport(int idRes)
        {
            var historial = await dbContext.HistorialPesos
                .Where(hp => hp.IdRes == idRes)
                .ToListAsync();

            WeightReportDTO result = new WeightReportDTO();

            result.fechas = new DateTime[historial.Count];
            result.valores = new int[historial.Count];

            for (int i = 0; i < historial.Count; i++)
            {
                result.fechas[i] = historial[i].FechaActualizacion;
                result.valores[i] = historial[i].PesoRes;
            }

            return result;
        }

    }
}
