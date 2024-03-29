﻿using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Banks.Floors
{
    public class FindAllFloorsRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid BuildingId { get; set; }
        public Guid BankId { get; set; }
    }
}