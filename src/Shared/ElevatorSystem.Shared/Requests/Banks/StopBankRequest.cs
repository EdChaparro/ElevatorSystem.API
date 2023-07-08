﻿using System;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Banks
{
    public class StopBankRequest : RequestAbstract
    {
        public Guid BusinessId { get; set; }
        public Guid BankId { get; set; }
    }
}