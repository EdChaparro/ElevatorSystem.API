using System;
using IntrepidProducts.Common.Requests;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.Common.Responses
{
    public class EntityAddedResponse : ResponseAbstract
    {
        public EntityAddedResponse(IEntityAddRequest request) : base(request)
        { }

        public Guid Id { get; set; }
    }
}