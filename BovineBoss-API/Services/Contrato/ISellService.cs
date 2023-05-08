using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface ISellService
    {
        Task<CreateSellDto> registerSell(CreateSellDto createSell);
        Task<List<SellDTO>> getListVentas();
        Task<bool> ModifySell(SellChangeDTO sellDTO);
        Task<List<VentasDto>> GetListVentasFincas(int idFinca);
    }
}
