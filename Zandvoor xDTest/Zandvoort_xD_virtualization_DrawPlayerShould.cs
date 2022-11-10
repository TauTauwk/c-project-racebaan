using Model;
using Zandvoort_xD;
namespace Zandvoor_xDTest
{
    public class Zandvoort_xD_virtualization_DrawPlayerShould
    {
        public IParticipant LeftPlayer { get; set; }
        public IParticipant RightPlayer { get; set; }
        [SetUp]
        public void Setup()
        {
            LeftPlayer = new Driver("1", 10, new Car(1, 10, 10, false), TeamColors.Red);
            RightPlayer = new Driver("2", 10, new Car(1, 10, 10, false), TeamColors.Blue);
        }

        [Test]
        public void DrawPlayers_LR_AreBroken()
        {
            LeftPlayer.Equipment.IsBroken = true;
            RightPlayer.Equipment.IsBroken = true;
            string expected = "##-##";
            string str = "#L-R#";
            string strNew = Virtualization.DrawPlayers(str, LeftPlayer, RightPlayer);
            Assert.AreEqual(expected, strNew);
        }

        [Test]
        public void DrawPlayers_LR_HasChanged()
        {
            string expected = "#1-2#";
            string str = "#L-R#";
            string strNew = Virtualization.DrawPlayers(str, LeftPlayer, RightPlayer);
            Assert.AreEqual(expected, strNew);
        }
    }
}