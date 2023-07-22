using System.Linq;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.ElevatorSystemBizTest.Mappers
{
    [TestClass]
    public class FloorMapperTest
    {
        [TestMethod]
        public void ShouldMapAllBankFloors()
        {
            var bank = new Bank(2, 1..10);

            var dtos = FloorMapper.Map(bank);

            Assert.IsNotNull(dtos);

            Assert.AreEqual(bank.NumberOfFloors, dtos.Count);

            foreach (var dto in dtos)
            {
                Assert.IsFalse(dto.IsDownCallBackRequested);
                Assert.IsFalse(dto.IsUpCallBackRequested);
            }
        }

        [TestMethod]
        public void ShouldMapFloorCallBackStatus()
        {
            var bank = new Bank(2, 1..5);

            var floor1Panel = bank.GetFloorFor(1)?.Panel;
            var floor3Panel = bank.GetFloorFor(3)?.Panel;
            var floor5Panel = bank.GetFloorFor(5)?.Panel;

            Assert.IsNotNull(floor1Panel);
            Assert.IsNotNull(floor3Panel);
            Assert.IsNotNull(floor5Panel);

            floor1Panel.UpButton?.SetPressedTo(true);

            floor3Panel.UpButton?.SetPressedTo(true);
            floor3Panel.DownButton?.SetPressedTo(true);

            floor5Panel.DownButton?.SetPressedTo(true);

            var dtos = FloorMapper.Map(bank);
            Assert.IsNotNull(dtos);

            var floor1DTO = dtos.FirstOrDefault(x => x.Number == 1);
            var floor2DTO = dtos.FirstOrDefault(x => x.Number == 2);
            var floor3DTO = dtos.FirstOrDefault(x => x.Number == 3);
            var floor4DTO = dtos.FirstOrDefault(x => x.Number == 4);
            var floor5DTO = dtos.FirstOrDefault(x => x.Number == 5);

            Assert.IsNotNull(floor1DTO);
            Assert.IsNotNull(floor2DTO);
            Assert.IsNotNull(floor3DTO);
            Assert.IsNotNull(floor4DTO);
            Assert.IsNotNull(floor5DTO);

            Assert.IsTrue(floor1DTO.IsUpCallBackRequested);

            Assert.IsFalse(floor2DTO.IsUpCallBackRequested);
            Assert.IsFalse(floor2DTO.IsDownCallBackRequested);

            Assert.IsTrue(floor3DTO.IsUpCallBackRequested);
            Assert.IsTrue(floor3DTO.IsDownCallBackRequested);

            Assert.IsFalse(floor4DTO.IsUpCallBackRequested);
            Assert.IsFalse(floor4DTO.IsDownCallBackRequested);

            Assert.IsTrue(floor5DTO.IsDownCallBackRequested);
        }

    }
}