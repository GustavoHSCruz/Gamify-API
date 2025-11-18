namespace Domain.Shared.Responses
{
    public class GetResponse : Response
    {
        public IEnumerable<object>? Data { get; set; }
        public int Items { get; set; }
        public int Pages { get; set; }
    }
}
