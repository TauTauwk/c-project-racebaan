using Model;
using System.ComponentModel.Design;

namespace ModelTest
{
    public class TrackShould
    {
        private Competition _competition;
        private SectionTypes[] track1;
        private Track aTrack;

        [SetUp]
        public void Setup()
        {
            var ln = SectionTypes.LeftN;
            var le = SectionTypes.LeftE;
            var ls = SectionTypes.LeftS;
            var lw = SectionTypes.LeftW;
            var Se = SectionTypes.StartE;
            var sw = SectionTypes.StraightW;
            var Fe = SectionTypes.FinishE;

            track1 = new SectionTypes[8]
            {
                Se, ln, lw, sw, sw, ls, le ,Fe
            };

            aTrack = new Track("Nederland", track1);
        }

        [Test]
        public void ConvArrayToLinkedList_track1Count_aTrackCount()
        {
            aTrack.Sections = aTrack.ConvArrayToLinkedList(track1);
            int actual = aTrack.Sections.Count();
            int expected = track1.Count();
            Assert.AreEqual(expected, actual);
        }
    }
}