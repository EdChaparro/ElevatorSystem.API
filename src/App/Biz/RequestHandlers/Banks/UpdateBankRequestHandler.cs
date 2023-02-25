using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks
{
    public class UpdateBankRequestHandler :
        AbstractRequestHandler<UpdateBankRequest, OperationResponse>
    {
        public UpdateBankRequestHandler(IRepository<BuildingElevatorBank> bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override OperationResponse DoHandle(UpdateBankRequest request)
        {
            var response = new OperationResponse(request);

            var bankDTO = request.Bank;
            if (bankDTO == null)
            {
                throw new ArgumentException("Bank object not provided");
            }

            IsValid(bankDTO);   //Will generation error-response when invalid

            var bank = _bankRepo.FindById(bankDTO.Id);

            if (bank == null)
            {
                response.Result = OperationResult.NotFound;
                return response;
            }

            bank.Name = bankDTO.Name;
            bank.FloorNbrs = bankDTO.FloorNbrs;
            bank.LowestFloorNbr = bankDTO.LowestFloorNbr;
            bank.HighestFloorNbr = bankDTO.HighestFloorNbr;
            bank.NumberOfElevators = bankDTO.NumberOfElevators;

            var isSuccessful = _bankRepo.Update(bank) == 1;

            if (isSuccessful)
            {
                response.Result = OperationResult.Successful;
                return response;
            }

            throw new InvalidOperationException
                ($"Update for Building Id {bank.Id} failed");
        }
    }
}