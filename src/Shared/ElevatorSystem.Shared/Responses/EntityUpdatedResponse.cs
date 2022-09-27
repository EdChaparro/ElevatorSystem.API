using System;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class EntityUpdatedResponse : ResponseAbstract
    {
        public EntityUpdatedResponse(IEntityAddRequest request) : base(request)
        { }

        public Guid EntityId { get; set; }
    }
}