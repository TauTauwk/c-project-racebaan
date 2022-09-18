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
            Competition _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Competition _competition = new Competition();
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Competition _competition = new Competition();

            var l = SectionTypes.LeftCorner;
            var r = SectionTypes.RightCorner;
            var S = SectionTypes.StartGrid;
            var s = SectionTypes.Straight;
            var F = SectionTypes.Finish;

            SectionTypes[] track1 = new SectionTypes[8]
                {
                    S, l, l, s, s, l, l ,F
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
            Competition _competition = new Competition();

            var l = SectionTypes.LeftCorner;
            var r = SectionTypes.RightCorner;
            var S = SectionTypes.StartGrid;
            var s = SectionTypes.Straight;
            var F = SectionTypes.Finish;

            SectionTypes[] track1 = new SectionTypes[8]
            {
                S, l, l, s, s, l, l ,F
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
            Competition _competition = new Competition();

            var l = SectionTypes.LeftCorner;
            var r = SectionTypes.RightCorner;
            var S = SectionTypes.StartGrid;
            var s = SectionTypes.Straight;
            var F = SectionTypes.Finish;

            SectionTypes[] track1 = new SectionTypes[8]
            {
                S, l, l, s, s, l, l ,F
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
