using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.Common.Requests
{
    public interface IEntityAddRequest : IRequest
    { }

    public class AddBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public string? Name { get; set; }
        public int NumberOfFloors { get; set; }
    }
}
