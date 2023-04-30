using BovineBoss_API.Models.DB;

namespace BovineBoss_API.Models.Dtos
{
    public class CreateSellDto
    {
        public int IdComprador { get; set; }
        public List<SellResDto> ResesVendidas { get; set; }
    }
}
