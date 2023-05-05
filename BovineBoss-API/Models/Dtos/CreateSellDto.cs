using BovineBoss_API.Models.DB;

namespace BovineBoss_API.Models.Dtos
{
    public class CreateSellDto
    {
        public BuyerDTO Comprador { get; set; }
        public List<SellResDto> ResesVendidas { get; set; }
    }
}
