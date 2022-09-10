using System;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class EntityAddedResponse : ResponseAbstract
    {
        public EntityAddedResponse(IEntityAddRequest request) : base(request)
        { }

        public Guid Id { get; set; }
    }
}