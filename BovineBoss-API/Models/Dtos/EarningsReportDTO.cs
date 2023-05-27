namespace BovineBoss_API.Models.Dtos
{
    public class EarningsReportDTO
    {
        public DateOnly[] fechas { get; set; }

        public int[] valores { get; set; }

    }
}