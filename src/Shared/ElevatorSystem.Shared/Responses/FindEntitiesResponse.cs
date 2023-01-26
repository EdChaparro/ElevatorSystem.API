using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class FindEntitiesResponse<TEntity> : ResponseAbstract
        where TEntity : class, new()
    {
        public FindEntitiesResponse(IRequest originalRequest, ErrorInfo? errorInfo = null)
            : base(originalRequest, errorInfo)
        {
            Entities = new List<TEntity>();
        }

        public List<TEntity> Entities { get; set; }
    }
}