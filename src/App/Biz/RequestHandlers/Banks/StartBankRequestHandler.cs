using IntrepidProducts.ElevatorService;
using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System;
using System.Linq;
using System.Threading;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks
{
    public class StartBankRequestHandler :
        AbstractRequestHandler<StartBankRequest, BankOperationsResponse>
    {
        public StartBankRequestHandler
            (IBuildingElevatorBankRepository bankRepo,
                IBankServiceRegistry bankServiceRegistry)
        {
            _bankRepo = bankRepo;
            _bankServiceRegistry = bankServiceRegistry;
        }

        private readonly IBuildingElevatorBankRepository _bankRepo;
        private readonly IBankServiceRegistry _bankServiceRegistry;

        protected override BankOperationsResponse DoHandle(StartBankRequest request)
        {
            var banks = _bankRepo.FindByBuildingId(request.BusinessId);

            var bankEntity = banks.FirstOrDefault(x => x.Id == request.BankId);

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

        private bool StartBank(BuildingElevatorBank bank)
        {
            var bankBizObject = bank.ToBusinessObject();

            var bankService = GetBankService(bankBizObject);

            if (bankService == null)
            {
                throw new InvalidOperationException
                    ($"Unable to locate Bank Service for Id {bank.Id}");
            }

            if (bankService.IsRunning)
            {
                return true;
            }

            bankService.StartAsync(new CancellationToken());
            return true;
        }

        private IBackgroundService? GetBankService(ElevatorSystem.Banks.Bank bankBizObject)
        {
            if (!_bankServiceRegistry.IsRegistered(bankBizObject))
            {
                _bankServiceRegistry.Register(bankBizObject);
            }

            return _bankServiceRegistry.Get(bankBizObject);
        }
    }
}