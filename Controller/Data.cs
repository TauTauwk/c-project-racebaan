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

            AddParticipants();

            AddTrack();
            AddTrack();
        }

        //add a participant
        private static void AddParticipants()
        {
            int teamColor = (num % 5);

            Competition?.Participants?.Add(new Driver("Chadwick Moore", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
            num++;
            Competition?.Participants?.Add(new Driver("Andrew Sowards", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
            num++;
            Competition?.Participants?.Add(new Driver("Ronnie Peter", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
            num++;
            Competition?.Participants?.Add(new Driver("Franklin Smith", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
            num++;
            Competition?.Participants?.Add(new Driver("Jozua Putnam", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
            num++;
            Competition?.Participants?.Add(new Driver("Jerold Samson", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
            num++;
            Competition?.Participants?.Add(new Driver("Raynard Blakeley", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
            num++;
            Competition?.Participants?.Add(new Driver("Gerard Meeuwis", 0, new Car(10, 10, 10, false), (TeamColors)(num % 5)));
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
                CurrentRace.Start();
            }
            NextRaceEvent?.Invoke(null, new OnNextRaceEventArgs(CurrentRace));
        }
    }
}
