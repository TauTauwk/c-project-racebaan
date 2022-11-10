using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTest
{
    public class Model_Driver_FunctionsInDriverShould
    {
        Driver driver;
        [SetUp]
        public void SetUp() 
        { 
            driver = new Driver("test1", 10, new Car(1,10,10,false), TeamColors.Blue);
        }

        [Test]
        public void GetPoints()
        {
            Assert.AreEqual(10, driver.Points);
        }

        [Test]
        public void GetTeamColor()
        {
            Assert.AreEqual(TeamColors.Blue, driver.TeamColor);
        }
    }
}
