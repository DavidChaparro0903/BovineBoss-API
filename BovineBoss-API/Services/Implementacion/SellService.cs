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
            if (!await resesAreValid(dto.ResesVendidas) || !buyerIsValid(dto.IdComprador))
            {
                return null;
            }
            //Una vez revisado, se crea la entidad Venta y se agrega a la BD
            Venta sale = new Venta()
            {
                FechaVenta = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy"), "MM-dd-yyyy", CultureInfo.InvariantCulture),
                IdComprador = dto.IdComprador
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

        private bool buyerIsValid(int IdComprador)
        {
            //Metodo que revisa la existencia del comprador en la BD, si no existe lo agrega
            //si existe, pero no es rol comprador "C", retorna false
            try
            {
                Persona p = _context.Personas.Where(p => p.IdPersona == IdComprador).FirstOrDefault();
                if(p.TipoPersona != "C")
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private async Task<bool> resesAreValid(List<SellResDto> resesVendidas)
        {
            //Metodo para revisar si la res existe en la BD y si no ha sido vendida

            foreach(SellResDto resVendida in resesVendidas)
            {
                Console.WriteLine($"\n\n\n------Accediendo a contexto {_context} ------\n\n\n");

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
                    List<object> listReses = new List<object>();
                    foreach (Rese res in reses)
                    {
                        listReses.Add(new
                        {
                            res.IdRes,
                            res.NombreRes,
                            res.ValorVenta
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

        public async Task<bool> ModifySell(SellDTO sellDTO)
        {
            try
            {
                Venta venta = await _context.Ventas.Where(a => a.IdVenta == sellDTO.IdVenta).FirstOrDefaultAsync();

                if (venta != null)
                {
                    venta.FechaVenta= sellDTO.FechaVenta;
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
