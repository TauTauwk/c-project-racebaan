using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    public class Controller_Race_MakeBrokenShould
    {
        Race race;
        List<IParticipant> participants;
        Track track;

        [SetUp]
        public void SetUp()
        {
            SectionTypes[] sections = new SectionTypes[4];
            sections[0] = SectionTypes.StartE;
            sections[1] = SectionTypes.StartE;
            sections[2] = SectionTypes.RightS;
            sections[3] = SectionTypes.FinishE;
            track = new Track("test", sections);
            participants = new List<IParticipant>();
            participants.Add(new Driver("Me", 100, new Car(1, 10, 10, false), TeamColors.Red));
            participants.Add(new Driver("Me", 100, new Car(1, 10, 10, true), TeamColors.Red));

            race = new Race(track, participants);
        }

        [Test]
        public void MakeBroken_MakesRandomBroken()
        {
            while (!participants[0].Equipment.IsBroken) {
                race.MakeBroken();
            }
            Assert.IsTrue(participants[0].Equipment.IsBroken);
        }

        [Test]
        public void FracturedButWhole_Repairs()
        {
            while (participants[1].Equipment.IsBroken)
            {
                race.FracturedButWhole();
            }
            Assert.IsFalse(participants[1].Equipment.IsBroken);
        }

        [Test]
        public void GiveStartPositions_GivesStartPositions_NotEmptyLeft()
        {
            race.GiveStartPositions(track, participants);

            Assert.IsNotNull(race.GetSectionData(track.Sections.First()).Left);
        }

        [Test]
        public void GiveStartPositions_GivesStartPositions_NotEmptyRight()
        {
            race.GiveStartPositions(track, participants);

            Assert.IsNotNull(race.GetSectionData(track.Sections.First()).Right);
        }

        [Test]
        public void GiveStartPositions_GiveStartPositions_IsEmptyLeft()
        {
            race.GiveStartPositions(track, participants);
            SectionData sectionData = race.GetSectionData(track.Sections.ElementAt(1));
            Assert.IsNull(sectionData.Left);
        }

        [Test]
        public void GiveStartPositions_GiveStartPositions_IsEmptyRight()
        {
            race.GiveStartPositions(track, participants);
            SectionData sectionData = race.GetSectionData(track.Sections.ElementAt(1));
            Assert.IsNull(sectionData.Right);
        }

        [Test]
        public void ChangeDriverPosition_DriverMovedOne_IsNotEmptyLeft()
        {
            participants[1].Equipment.IsBroken = false;
            race.GiveStartPositions(track, participants);
            SectionData sectionData = race.GetSectionData(track.Sections.ElementAt(1));
            while (sectionData.Left == null)
            {
                race.ChangeDriverPosition(track);
            }
            Assert.IsNotNull(sectionData.Left);
        }

        [Test]
        public void ChangeDriverPosition_DriverMovedOne_IsNotEmptyRight()
        {
            race.GiveStartPositions(track, participants);
            SectionData sectionData = race.GetSectionData(track.Sections.ElementAt(1));
            while (sectionData.Right == null)
            {
                race.ChangeDriverPosition(track);
            }
            Assert.IsNotNull(sectionData.Right);
        }

        [Test]
        public void AmountOfLaps_DriverMakesMultipleLaps()
        {
            race.GiveStartPositions(track, participants);
            while (race.AmountOfLaps(participants[0]) != 2)
            {
                race.ChangeDriverPosition(track);
            }
            Assert.AreEqual(2, race.AmountOfLaps(participants[0]));
        }

        [Test]
        public void IsFinished_isFinished_True()
        {
            race.GiveStartPositions(track, participants);
            while (race.AmountOfLaps(participants[0]) != 2)
            {
                race.ChangeDriverPosition(track);
            }
            Assert.IsTrue(race.IsFinished(participants[0]));
        }

        [Test]
        public void IsFinished_isFinished_False()
        {
            race.GiveStartPositions(track, participants);
            race.ChangeDriverPosition(track);
            Assert.IsFalse(race.IsFinished(participants[0]));
        }
    }
}
