using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    static public class Data
    {
        static private Race currentRace;
        static public Competition Competition { get; set; }
        static public Race CurrentRace { get; set; }

        static private int num =  0;
        static private int trackNum = 0;

        static public void Initialize()
        {
            Competition = new Competition();
            Competition.Participants = new List<IParticipant>();
            Competition.Tracks = new Queue<Track>();
            CurrentRace = currentRace;

            AddParticipant();
            AddParticipant();
            AddParticipant();
            AddParticipant();
            AddParticipant();
            AddParticipant();

            AddTrack();
            AddTrack();
        }

        static public void AddParticipant()
        {
            num++;
            int teamColor = (num % 5);

            Car car = new Car(100, 100, 100, false);
            Driver driver = new Driver("driver" + num, 0, car, (TeamColors)teamColor);
            Competition.Participants.Add(driver);
        }

        static public void AddTrack()
        {
            trackNum++;
            trackNum = trackNum % 2;

            var l = SectionTypes.LeftCorner;
            var r = SectionTypes.RightCorner;
            var S = SectionTypes.StartGrid;
            var s = SectionTypes.Straight;
            var F = SectionTypes.Finish;

            if (trackNum == 1)
            {
                SectionTypes[] track1 = new SectionTypes[8]
                {
                    S, l, l, s, s, l, l ,F
                };
                Track a = new Track("Nederland", track1);
                Competition.Tracks.Enqueue(a);
            }
            else 
            {
                SectionTypes[] track2 = new SectionTypes[16]
                {
                    S, r, l, r, r, s, s, l, r, r, s, l, r, r, s, F
                };
                Track a = new Track("België", track2);
                Competition.Tracks.Enqueue(a);
            }
        }

        static public void NextRace() 
        {
            var track = Competition.NextTrack();
            if (track != null)
            {
                CurrentRace = new Race(track, Competition.Participants);
            }
        }
    }
}
