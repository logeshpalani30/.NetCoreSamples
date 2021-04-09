namespace WebApi.Model
{
    public class FcmMessageRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Topic { get; set; }
        public string? ImageUri { get; set; }
    }
}