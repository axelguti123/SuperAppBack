namespace SuperApp.Services.DTOs
{
    public class ResponseDTO
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class ResponseDTO<TEntity> : ResponseDTO
    {
        public TEntity Data { get; set; }
    }
}
