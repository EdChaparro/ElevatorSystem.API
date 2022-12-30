using System;
using System.Collections.Generic;

namespace IntrepidProducts.WebAPI.Results
{
    //Based on https://code-maze.com/hateoas-aspnet-core-web-api/
    public class Links : List<Link>
    { }

    public class Link
    {
        //Empty constructor needed by some serializers
        public Link()
        { }
        public Link(string href, string rel, string method, string id = "")
        {
            Href = href;
            Rel = rel;
            Method = method;
            Id = id;
        }

        public string? Href { get; set; }
        public string? Rel { get; set; }
        public string? Method { get; set; }
        public string? Id { get; set; }
    }
}