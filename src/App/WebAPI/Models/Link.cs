namespace IntrepidProducts.WebAPI.Models
{
    //Based on https://code-maze.com/hateoas-aspnet-core-web-api/
    public class Link
    {
        //Empty constructor needed by some serializers
        public Link()
        { }
        public Link(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }

        public string? Href { get; set; }
        public string? Rel { get; set; }
        public string? Method { get; set; }
    }
}