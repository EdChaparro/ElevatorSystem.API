using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class AddBankRequestHandler :
        AbstractRequestHandler<AddBankRequest, EntityOperationResponse>
    {
        public AddBankRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override EntityOperationResponse DoHandle(AddBankRequest request)
        {
            var bankDTO = request.Bank;
            if (bankDTO == null)
            {
                throw new ArgumentException("Bank object not provided");
            }

            IsValid(bankDTO);   //Will generation error-response when invalid

            var building = _buildings.FirstOrDefault(x => x.Id == bankDTO.BuildingId);

            if (building == null)
            {
                throw new ArgumentException("Building Id not found");
            }

            var bank = Instantiate(bankDTO);
            building.Add(bank);

            return new EntityOperationResponse(request)
            {
                EntityId = bank.Id
            };
        }

        private static Bank Instantiate(BankDTO dto)
        {
            if (dto.FloorNbrs.Any())
            {
                return new Bank(dto.NumberOfElevators, dto.FloorNbrs.ToArray())
                {
                    Name = dto.Name
                };
            }

            return new Bank(dto.NumberOfElevators,
                new Range(dto.LowestFloorNbr, dto.HighestFloorNbr))
            {
                Name = dto.Name
            };
        }
    }
}