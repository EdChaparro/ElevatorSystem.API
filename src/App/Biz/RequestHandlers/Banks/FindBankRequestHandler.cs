﻿using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
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

            var dto = elevatorBank == null
                ? null
                : ToDTO(elevatorBank);

            return new FindBankResponse(request)
            {
                Bank = dto
            };
        }

        private static BankDTO ToDTO(BuildingElevatorBank bank)
        {
            return new BankDTO
            {
                Id = bank.Id,
                BuildingId = bank.BuildingId,
                Name = bank.Name,
                NumberOfElevators = bank.NumberOfElevators,
                FloorNbrs = bank.FloorNbrs,
                LowestFloorNbr = bank.LowestFloorNbr,
                HighestFloorNbr = bank.HighestFloorNbr
            };
        }
    }
}