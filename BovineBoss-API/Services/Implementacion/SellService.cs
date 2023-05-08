using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BovineBoss_API.Services.Implementacion
{
    public class SellService:ISellService
    {
        private BovineBossContext _context;
        public SellService(BovineBossContext context)
        {
            _context = context;
        }

        public async Task<CreateSellDto> registerSell(CreateSellDto dto)
        {
            //Revisa si la res existe en la BD y que no haya sido vendida
            //Revisa que el comprador exista en la BD y que sea una persona de tipo comprador
            
            if (!await resesAreValid(dto.ResesVendidas))
            {
                return null;
            }
            int IdComprador = validateBuyer(dto.Comprador);
            //Una vez revisado, se crea la entidad Venta y se agrega a la BD
            Venta sale = new Venta()
            {
                FechaVenta = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy"), "MM-dd-yyyy", CultureInfo.InvariantCulture),
                IdComprador = IdComprador
            };
            _context.Ventas.Add(sale);
            _context.SaveChanges();

            //Se recupera el Id generado para esta venta
            int IdVenta = _context.Entry(sale).Entity.IdVenta;

            //Se vinculan el id y valor de venta en las respectivas reses involucradas
            foreach(SellResDto sellRes in dto.ResesVendidas)
            {
                var res = _context.Reses.Find(sellRes.IdRes);
                res.IdVenta = IdVenta;
                res.ValorVenta = sellRes.Precio;
            }
            _context.SaveChanges();
            return dto;
        }

        private int validateBuyer(BuyerDTO compradorDTO)
        {
            //Metodo que revisa la existencia del comprador en la BD, si no existe lo agrega
            //si existe, pero no es rol comprador "C", retorna false
            try
            {
                Persona p = _context.Personas.Where(p => p.Cedula == compradorDTO.Cedula).FirstOrDefault();
                return p.IdPersona;
            }
            catch
            {
                Persona comprador = new Persona()
                {
                    NombrePersona = compradorDTO.NombreComprador,
                    ApellidoPersona = compradorDTO.ApellidoComprador,
                    Cedula = compradorDTO.Cedula,
                    TipoPersona = "C"
                };
                _context.Personas.Add(comprador);
                return _context.Entry(comprador).Entity.IdPersona;
            }
        }

        private async Task<bool> resesAreValid(List<SellResDto> resesVendidas)
        {
            //Metodo para revisar si la res existe en la BD y si no ha sido vendida

            foreach(SellResDto resVendida in resesVendidas)
            {
                try
                {
                    var res = await _context.Reses.Where(r => r.IdRes == resVendida.IdRes).FirstOrDefaultAsync();
                    if (res.IdVenta != null) 
                        return false;
                }
                catch { return false; } 
            }
            return true;
        }

        public async Task<List<SellDTO>> getListVentas()
        {
            List<SellDTO> list = new List<SellDTO>();
            var ventas = await _context.Ventas.ToListAsync();
            foreach (Venta v in ventas)
            {
                try
                {
                    var reses = await _context.Reses.Where(r => r.IdVenta == v.IdVenta).ToListAsync();
                    List<SellResDto> listReses = new List<SellResDto>();
                    foreach (Rese res in reses)
                    {
                        listReses.Add(new SellResDto()
                        {
                            IdRes = res.IdRes,
                            Nombre = res.NombreRes,
                            Precio = res.ValorVenta
                        });
                    }
                    SellDTO dto = new SellDTO()
                    {
                        IdVenta = v.IdVenta,
                        Reses = listReses,
                        FechaVenta = v.FechaVenta
                    };
                    list.Add(dto);
                }
                catch { return null; }
            }
            return list;
        }

        public async Task<bool> ModifySell(SellChangeDTO sellDTO)
        {
            try
            {
                Venta venta = await _context.Ventas.Where(a => a.IdVenta == sellDTO.IdVenta).FirstOrDefaultAsync();
                List<Rese> toModify = await _context.Reses.Where(r => r.IdVenta == sellDTO.IdVenta).ToListAsync();
                foreach(Rese res in toModify)
                {
                    res.ValorVenta = sellDTO.ValorVenta;
                }
                if (venta != null)
                {
                    venta.FechaVenta = sellDTO.FechaVenta;
                    _context.Ventas.Update(venta);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
