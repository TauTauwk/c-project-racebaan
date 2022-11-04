using System;
using System.Collections.Generic;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition? Competition { get; set; }
        public static Race? CurrentRace { get; set; }

        public static event EventHandler<OnNextRaceEventArgs> NextRaceEvent;

        private static int num =  0;
        private static int trackNum = 0;

        public static void Initialize()
        {
            Competition = new Competition();
            Competition.Participants = new List<IParticipant>();
            Competition.Tracks = new Queue<Track>();

            AddParticipant();
            AddParticipant();
            AddParticipant();
            AddParticipant();
            AddParticipant();
            AddParticipant();

            AddTrack();
            AddTrack();
        }

        //add a participant
        //call per participant
        private static void AddParticipant()
        {
            num++;
            int teamColor = (num % 5);

            Car car = new Car(10, 10, 10, false);
            Driver driver = new Driver("driver" + num, 0, car, (TeamColors)teamColor);
            Competition?.Participants?.Add(driver);
        }

        //adds a track to the queue
        //call per track
        private static void AddTrack()
        {
            trackNum++;
            trackNum = trackNum % 2;

            var ln = SectionTypes.LeftN;
            var le = SectionTypes.LeftE;
            var ls = SectionTypes.LeftS;
            var lw = SectionTypes.LeftW;
            var rn = SectionTypes.RightN;
            var re = SectionTypes.RightE;
            var rs = SectionTypes.RightS;
            var rw = SectionTypes.RightW;
            var Se = SectionTypes.StartE;
            var sn = SectionTypes.StraightN;
            var se = SectionTypes.StraightE;
            var sw = SectionTypes.StraightW;
            var Fe = SectionTypes.FinishE;

            if (trackNum == 0)
            {
                SectionTypes[] track1 = new SectionTypes[]
                {
                    Se, Se, Se, Se, ln, lw, sw, sw, ls, le ,Fe
                };
                Track a = new Track("Nederland", track1);
                Competition?.Tracks?.Enqueue(a);
            }
            else 
            {
                SectionTypes[] track2 = new SectionTypes[]
                {
                    Se, Se, Se, Se, rs, le, rs, rw, sw, sw, ls, rw, rn, sn, lw, rn, re, se, Fe
                };
                Track a = new Track("België", track2);
                Competition?.Tracks?.Enqueue(a);
            }
        }

        //gets the next race from the competition
        public static void NextRace() 
        {
            Track track = null;
            if (Competition.NextTrack != null)
            {
                track = Competition.NextTrack();
            }
            if (track != null)
            {
                CurrentRace = new Race(track, Competition?.Participants);
                NextRaceEvent?.Invoke(null, new OnNextRaceEventArgs(CurrentRace));
                CurrentRace.start();
            }
        }
    }
}
