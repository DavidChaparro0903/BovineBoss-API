namespace BovineBoss_API.Models.Dtos
{
    public class Response
    {
        public Object data { get; set; } = null!;
        public string? message { get; set; } = null!;
        public string? errors { get; set; } = null!;

        public Object? aditionals { get; set; } = null!;
    }
}
