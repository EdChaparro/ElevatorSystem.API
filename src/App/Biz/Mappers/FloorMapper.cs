using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks.Floors;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.ElevatorSystemBiz.Mappers;

public static class FloorMapper
{
    public static List<FloorDTO> Map(Bank bank)
    {
        var floorDTOs = new List<FloorDTO>();

        foreach (var floorNbr in bank.OrderedFloorNumbers)
        {
            var floor = bank.GetFloorFor(floorNbr);

            if (floor == null)
            {
                continue;
            }

            var dto = new FloorDTO
            {
                Number = floor.Number,
                IsLocked = floor.IsLocked,
                Name = floor.Name
            };


            var floorPanel = floor.Panel;

            if (floorPanel == null)
            {
                floorDTOs.Add(dto);
                continue;
            }

            var downButton = floorPanel.DownButton;
            var upButton = floorPanel.UpButton;

            dto.IsDownCallBackRequested = downButton?.IsPressed ?? false;
            dto.IsUpCallBackRequested = upButton?.IsPressed ?? false;

            floorDTOs.Add(dto);
        }

        return floorDTOs;
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