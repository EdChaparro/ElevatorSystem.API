using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class AddBankRequestHandler :
        AbstractRequestHandler<AddBankRequest, EntityOperationResponse>
    {
        public AddBankRequestHandler(
            IRepository<Building> buildingRepo, IRepository<BuildingElevatorBank> bankRepo)
        {
            _buildingRepo = buildingRepo;
            _bankRepo = bankRepo;
        }

        private readonly IRepository<Building> _buildingRepo;
        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override EntityOperationResponse DoHandle(AddBankRequest request)
        {
            var bankDTO = request.Bank;
            if (bankDTO == null)
            {
                throw new ArgumentException("Bank object not provided");
            }

            IsValid(bankDTO);   //Will generation error-response when invalid

            var building = _buildingRepo.FindById(bankDTO.BuildingId);

            if (building == null)
            {
                throw new ArgumentException("Building Id not found");
            }

            var buildingElevatorBank = Transform(bankDTO);

            _bankRepo.Create(buildingElevatorBank);

            return new EntityOperationResponse(request)
            {
                EntityId = buildingElevatorBank.Id
            };
        }

        private static BuildingElevatorBank Transform(BankDTO dto)
        {
            if (dto.FloorNbrs.Any())
            {
                return new BuildingElevatorBank(dto.BuildingId,
                    new Bank(dto.NumberOfElevators, dto.FloorNbrs.ToArray())
                    {
                        Name = dto.Name
                    });
            }

            return new BuildingElevatorBank(dto.BuildingId,
                new Bank(dto.NumberOfElevators,
                    new Range(dto.LowestFloorNbr, dto.HighestFloorNbr))
                {
                    Name = dto.Name
                });
        }
    }
}