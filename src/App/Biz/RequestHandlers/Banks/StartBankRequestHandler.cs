using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System;
using IntrepidProducts.ElevatorService.Banks;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks
{
    public class StartBankRequestHandler :
        AbstractRequestHandler<StartBankRequest, BankOperationsResponse>
    {
        public StartBankRequestHandler
            (IRepository<BuildingElevatorBank> bankRepo,
                IBankServiceRegistry bankServiceRegistry)
        {
            _bankRepo = bankRepo;
            _bankServiceRegistry = bankServiceRegistry;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;
        private readonly IBankServiceRegistry _bankServiceRegistry;

        protected override BankOperationsResponse DoHandle(StartBankRequest request)
        {
            var bankEntity = _bankRepo.FindById(request.BankId);

            if (bankEntity == null)
            {
                throw new ArgumentException
                    ($"Bank not found, Id: {request.BankId}");
            }

            var elevatorDTOs = ElevatorMapper.Map(bankEntity.Elevators);

            var isStarted = StartBank(bankEntity);

            if (!isStarted)
            {
                throw new InvalidOperationException("Bank Start Operation Failed");
            }

            return new BankOperationsResponse(request) { ElevatorDTOs = elevatorDTOs };
        }

        //TODO: Finish Me
        private bool StartBank(BuildingElevatorBank bank)
        {
            return true;
        }
    }
}