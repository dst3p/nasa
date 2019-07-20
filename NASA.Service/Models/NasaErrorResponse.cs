namespace NASA.API.Models
{
    public class NasaErrorResponse
    {
        public NasaErrorBody error { get; set; }
    }

    public class NasaErrorBody
    {
        public string code { get; set; }

        public string message { get; set; }
    }
}