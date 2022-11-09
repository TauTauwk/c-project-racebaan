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
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CarCreated()
        {
            Car car = new Car(100, 100, 100, false);

            Assert.IsTrue(car is Car);
        }

    }
}
