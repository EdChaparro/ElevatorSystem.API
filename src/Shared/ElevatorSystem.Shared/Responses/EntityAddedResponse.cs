using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class EntityAddedResponse : ResponseAbstract
    {
        public EntityAddedResponse(IEntityAddRequest request) : base(request)
        { }

        public Guid EntityId { get; set; }
    }
}