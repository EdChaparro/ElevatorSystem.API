using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class BankControlPanelResponse : ResponseAbstract
    {
        public BankControlPanelResponse(IRequest originalRequest, ErrorInfo? errorInfo = null)
            : base(originalRequest, errorInfo)
        { }

        public bool IsRunning { get; set; }
        public List<ElevatorDTO> ElevatorDTOs { get; set; } = new List<ElevatorDTO>();
    }
}