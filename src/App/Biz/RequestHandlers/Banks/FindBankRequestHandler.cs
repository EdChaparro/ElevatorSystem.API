using System;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class FindBankRequestHandler :
        AbstractRequestHandler<FindBankRequest, FindBankResponse>
    {
        public FindBankRequestHandler(IRepository<BuildingElevatorBank> bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override FindBankResponse DoHandle(FindBankRequest request)
        {
            var elevatorBank = _bankRepo.FindById(request.BankId);
            var details = elevatorBank?.Bank;

            var dto = elevatorBank == null
                ? null
                : ToDTO(details, elevatorBank.BuildingId, request.BankId);

            return new FindBankResponse(request)
            {
                Bank = dto
            };
        }

        private static BankDTO ToDTO(Bank? bank, Guid buildingId, Guid bankId)
        {
            if ((bank == null))
            {
                throw new InvalidOperationException
                    ($"Bank details null for Id {bankId}");
            }

            return new BankDTO
            {
                Id = bank.Id,
                BuildingId = buildingId,
                Name = bank.Name,
                NumberOfElevators = bank.NumberOfElevators,
                FloorNbrs = bank.OrderedFloorNumbers.ToList(),
                LowestFloorNbr = bank.LowestFloorNbr,
                HighestFloorNbr = bank.HighestFloorNbr
            };
        }
    }
}