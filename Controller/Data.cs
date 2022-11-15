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
        private const int VerschillendeTracks = 2;

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
            trackNum = trackNum % VerschillendeTracks;

            if (trackNum == 1)
            {
                Track a = new Track("Nederland", Track1());
                Competition?.Tracks?.Enqueue(a);
            }
            else 
            {
                Track a = new Track("België", Track2());
                Competition?.Tracks?.Enqueue(a);
            }
        }

        private static SectionTypes[] Track1()
        {
            SectionTypes[] track = new SectionTypes[] {
                SectionTypes.StartE,
                SectionTypes.StartE,
                SectionTypes.StartE,
                SectionTypes.StartE,
                SectionTypes.LeftN,
                SectionTypes.LeftW,
                SectionTypes.StraightW,
                SectionTypes.StraightW,
                SectionTypes.LeftS,
                SectionTypes.LeftE,
                SectionTypes.FinishE
            };
            return track;
        }

        private static SectionTypes[] Track2()
        {
            SectionTypes[] track = new SectionTypes[] {
                SectionTypes.StartE,
                SectionTypes.StartE,
                SectionTypes.StartE,
                SectionTypes.StartE,
                SectionTypes.RightS,
                SectionTypes.LeftE,
                SectionTypes.RightS,
                SectionTypes.RightW,
                SectionTypes.StraightW,
                SectionTypes.StraightW,
                SectionTypes.LeftS,
                SectionTypes.RightW,
                SectionTypes.RightN,
                SectionTypes.StraightN,
                SectionTypes.LeftW,
                SectionTypes.RightN,
                SectionTypes.RightE,
                SectionTypes.StraightE,
                SectionTypes.FinishE
            };
            return track;
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
