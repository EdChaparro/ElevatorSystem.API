using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Shared.EntitiesTest;

[TestClass]
public class ElevatorTest
{
    [TestMethod]
    public void ShouldConvertToEntityObject()
    {
        var originalElevatorBizObj = new IntrepidProducts.ElevatorSystem.Elevators.Elevator
            (1..10)
            {
                Name = "Elevator A"
            };

        var entityElevatorObj = new Elevator(Guid.NewGuid(), originalElevatorBizObj);

        CollectionAssert.AreEqual(originalElevatorBizObj.OrderedFloorNumbers.ToList(),
            entityElevatorObj.FloorNbrs);

        Assert.AreEqual(originalElevatorBizObj.Id, entityElevatorObj.Id);
        Assert.AreEqual(originalElevatorBizObj.Name, entityElevatorObj.Name);
    }
}