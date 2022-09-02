namespace TinyLink.Core.Domain.Links.Models
{
    public class CreateLinkRequest
    {
        public string Url { get; set; }
    }

    public class CreateLinkResponse 
    {
        public string Code { get; set; }
    }
}
