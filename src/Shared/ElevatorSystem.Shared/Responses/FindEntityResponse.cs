using System.Collections.Generic;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class FindEntityResponse<TEntity> : ResponseAbstract
        where TEntity : class, new()
    {
        public FindEntityResponse(IRequest originalRequest, ErrorInfo? errorInfo = null)
            : base(originalRequest, errorInfo)
        {
            Entities = new List<TEntity>();
        }

        public List<TEntity> Entities { get; set; }
    }
}