using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class BankOperationsResponse : ResponseAbstract
    {
        public BankOperationsResponse(IRequest originalRequest, ErrorInfo? errorInfo = null)
            : base(originalRequest, errorInfo)
        { }

        public bool IsRunning { get; set; }
        public List<ElevatorDTO> ElevatorDTOs { get; set; } = new List<ElevatorDTO>();
    }
}