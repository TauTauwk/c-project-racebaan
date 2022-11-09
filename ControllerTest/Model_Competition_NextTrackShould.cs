using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Assert.IsNull(_competition.NextTrack());
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {

            var ln = SectionTypes.LeftN;
            var le = SectionTypes.LeftE;
            var ls = SectionTypes.LeftS;
            var lw = SectionTypes.LeftW;
            var Se = SectionTypes.StartE;
            var sw = SectionTypes.StraightW;
            var Fe = SectionTypes.FinishE;

            SectionTypes[] track1 = new SectionTypes[8]
            {
                Se, ln, lw, sw, sw, ls, le ,Fe
            };

            Track aTrack = new Track("Nederland", track1);

            _competition.Tracks = new Queue<Track>();
            _competition.Tracks.Enqueue(aTrack);

            var result = _competition.NextTrack();
            Assert.AreEqual(aTrack, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {

            var ln = SectionTypes.LeftN;
            var le = SectionTypes.LeftE;
            var ls = SectionTypes.LeftS;
            var lw = SectionTypes.LeftW;
            var Se = SectionTypes.StartE;
            var sw = SectionTypes.StraightW;
            var Fe = SectionTypes.FinishE;

            SectionTypes[] track1 = new SectionTypes[8]
            {
                Se, ln, lw, sw, sw, ls, le ,Fe
            };

            Track aTrack = new Track("Nederland", track1);

            _competition.Tracks = new Queue<Track>();
            _competition.Tracks.Enqueue(aTrack);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {

            var ln = SectionTypes.LeftN;
            var le = SectionTypes.LeftE;
            var ls = SectionTypes.LeftS;
            var lw = SectionTypes.LeftW;
            var Se = SectionTypes.StartE;
            var sw = SectionTypes.StraightW;
            var Fe = SectionTypes.FinishE;

            SectionTypes[] track1 = new SectionTypes[8]
            {
                Se, ln, lw, sw, sw, ls, le ,Fe
            };
            var track2 = track1;

            Track aTrack = new Track("Nederland", track1);
            Track bTrack = new Track("België", track2);

            _competition.Tracks = new Queue<Track>();

            _competition.Tracks.Enqueue(aTrack);
            _competition.Tracks.Enqueue(bTrack);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.AreEqual(bTrack, result);
        }
    }
}
