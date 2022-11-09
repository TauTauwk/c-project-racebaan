using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;

namespace ControllerTest
{
    public class Controller_Data_FunctionsInDataShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();

            SectionTypes[] sections = new SectionTypes[4];
            sections[0] = SectionTypes.StartE;
            sections[1] = SectionTypes.StartE;
            sections[2] = SectionTypes.RightS;
            sections[3] = SectionTypes.FinishE;
            Track track1 = new Track("testTrack1", sections);
            Track track2 = new Track("testTrack2", sections);
            List<IParticipant> Participants = new List<IParticipant>();
            Participants.Add(new Driver("test1", 10, new Car(1, 10, 10, false), TeamColors.Red));
            Participants.Add(new Driver("test2", 10, new Car(1, 10, 10, false), TeamColors.Blue));
            Race race1 = new Race(track1, Participants);
            Race race2 = new Race(track2, Participants);
        }

        [Test]
        public void AddParticipants_AddsEightParticipants()
        {
            Assert.AreEqual(8, Data.Competition.Participants.Count());
        }

        [Test]
        public void Addtrack_AddsTracks()
        {
            Assert.AreEqual(2, Data.Competition.Tracks.Count());
        }

        [Test]
        public void NextRace_GoesToFirstRace()
        {
            string expected = Data.Competition.Tracks.ElementAt(0).Name;
            Data.NextRace(); // no race to first race
            Assert.AreEqual(expected, Data.CurrentRace.track.Name );
        }

        [Test]
        public void NextRace_GoesToSecondRace()
        {
            string expected = Data.Competition.Tracks.ElementAt(1).Name;
            Data.NextRace(); // no race to first race
            Data.NextRace(); // first race to second race
            Assert.AreEqual(expected, Data.CurrentRace.track.Name);
        }

        [Test]
        public void NextRace_NoThirdRace()
        {
            string expected = Data.Competition.Tracks.ElementAt(1).Name;
            Data.NextRace(); // no race to first race
            Data.NextRace(); // first race to second race
            Data.NextRace(); // no third race so second race stays
            Assert.AreEqual(expected, Data.CurrentRace.track.Name);
        }
    }
}
