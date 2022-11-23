using System;
using IntrepidProducts.Common;
using IntrepidProducts.ElevatorSystem.Banks;

namespace IntrepidProducts.Shared.ElevatorSystem.Entities
{
    public class BuildingElevatorBank : IHasId
    {
        public Guid BuildingId { get; set; }
        public Bank Bank { get; set; }

        public Guid Id
        {
            get
            {
                AssertBankIsNotNull();
                return Bank.Id;
            }

            set
            {
                AssertBankIsNotNull();
                Bank.Id = value;
            }
        }

        private void AssertBankIsNotNull()
        {
            if (Bank == null)
            {
                throw new NullReferenceException("Bank object is null");
            }
        }
    }
}