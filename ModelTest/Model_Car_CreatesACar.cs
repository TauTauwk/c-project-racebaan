using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTest
{
    public class Model_Car_CreatesACar
    {
        Car car;
        [SetUp]
        public void Setup()
        {
            car = new Car(100, 100, 100, false);
        }

        [Test]
        public void CarCreated()
        {
            Assert.IsTrue(car is Car);
        }

    }
}
