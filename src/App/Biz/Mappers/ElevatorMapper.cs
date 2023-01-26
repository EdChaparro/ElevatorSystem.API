using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Biz.Mappers
{
    public static class ElevatorMapper
    {
        internal static List<ElevatorDTO> Map(IEnumerable<Elevator> elevators)
        {
            var elevatorDTOs = new List<ElevatorDTO>();

            foreach (var elevator in elevators)
            {
                var dto = new ElevatorDTO
                {
                    BankId = elevator.BankId,
                    Id = elevator.Id,
                    Name = elevator.Name,
                    FloorNbrs = elevator.FloorNbrs,
                    CurrentFloorNumber = elevator.CurrentFloorNumber,
                    IsEnabled = elevator.IsEnabled,
                    IsIdle = elevator.IsIdle
                };

                elevatorDTOs.Add(dto);
            }

            return elevatorDTOs;
        }
    }
}