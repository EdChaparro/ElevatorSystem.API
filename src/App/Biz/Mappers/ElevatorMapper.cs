﻿using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Biz.Mappers
{
    public static class ElevatorMapper
    {
        public static List<ElevatorDTO> Map(IEnumerable<Elevator> elevators)
        {
            var elevatorDTOs = new List<ElevatorDTO>();

            foreach (var elevator in elevators)
            {
                var dto = Map(elevator);

                if (dto == null)
                {
                    continue;
                }

                elevatorDTOs.Add(dto);
            }

            return elevatorDTOs;
        }

        public static ElevatorDTO? Map(Elevator? elevator)
        {
            if (elevator == null)
            {
                return null;
            }

            return new ElevatorDTO
            {
                BankId = elevator.BankId,
                Id = elevator.Id,
                Name = elevator.Name,
                FloorNbrs = elevator.FloorNbrs,
                CurrentFloorNumber = elevator.CurrentFloorNumber,
                IsEnabled = elevator.IsEnabled,
                IsIdle = elevator.IsIdle
            };
        }
    }
}