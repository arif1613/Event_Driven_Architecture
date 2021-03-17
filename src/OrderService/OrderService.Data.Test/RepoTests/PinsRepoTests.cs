using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderService.Data.Context;
using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Data.Test.RepoTests
{
    [TestClass]

    public class PinsRepoTests
    {
        private Mock<IOrderContext> pinsContextMock;
        
        [TestInitialize]
        public void Setup()
        {
            pinsContextMock = TestHelpers.TestHelpers.MockContext<IOrderContext>();
            var pins = Enumerable.Range(0, 100).Select(r => new Pin
            {
                ID = r,
                IsUsed = false,
                Value = 1000 + 1
            });

            var pinsMockSet = TestHelpers.TestHelpers.CreateMockDbSet(pins.AsQueryable());
            pinsContextMock.Setup(r => r.Pins).Returns(pinsMockSet.Object);
        }



        [TestMethod]
        public void Prepopulated_pins_should_be_unique()
        {
            var pinsrepo=new PinsRepo(pinsContextMock.Object);
            var pins= pinsrepo.CreatePins(100);
            var selectedpin=new Pin();
            foreach (var pin in pins)
            {
                Assert.AreNotEqual(selectedpin,pin);
                selectedpin = pin;
            }
            
            Assert.AreEqual(100,pins.Count);

        }

        [TestMethod]
        public void Get_pin_should_return_an_unique_unused_pin()
        {
            var pinsrepo = new PinsRepo(pinsContextMock.Object);
            var pin = pinsrepo.GetPin();
            Assert.IsNotNull(pin);
            Assert.AreEqual(pin.IsUsed,true);
        }

        [TestMethod]
        public void Unuse_pin_should_make_pin_unused()
        {
            var pinsrepo = new PinsRepo(pinsContextMock.Object);
            var pin=pinsrepo.GetPin();
            pinsContextMock.Object.UpdateEntry(pin);
            Assert.IsNotNull(pin);
            Assert.AreEqual(true, pin.IsUsed);
            pinsrepo.UnUsePin(pin.Value);
            var pin1 = pinsrepo.GetByPinValue(pin.Value);
            Assert.IsNotNull(pin1);
            Assert.AreEqual(pin.Value, pin1.Value);
            Assert.AreEqual(false,pin1.IsUsed);
        }

        [TestCleanup]
        public void CleanUp()
        {
            pinsContextMock.Reset();
        }
    }
}
