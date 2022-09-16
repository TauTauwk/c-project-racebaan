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

        static public void Initialize()
        {
            Competition = new Competition();
            Competition.Participants = new List<IParticipant>();
            Competition.Tracks = new Queue<Track>();
            CurrentRace = currentRace;

            Car a = new Car(100, 100, 100, false);
            Driver b = new Driver("driver1", 0, a, TeamColors.Blue);
            Car c = new Car(100, 100, 100, false);
            Driver d = new Driver("driver2", 0, c, TeamColors.Red);
            Car e = new Car(100, 100, 100, false);
            Driver f = new Driver("driver3", 0, e, TeamColors.Green);
            AddParticipant(b);
            AddParticipant(d);
            AddParticipant(f);

            var l = SectionTypes.LeftCorner;
            var r = SectionTypes.RightCorner;
            var S = SectionTypes.StartGrid;
            var s = SectionTypes.Straight;
            var F = SectionTypes.Finish;

            SectionTypes[] sectionsTrack1 = new SectionTypes[8]
            {
                S, l, l, s, s, l, l ,F
            };

            SectionTypes[] sectionsTrack2 = new SectionTypes[16]
            {
                S, r, l, r, r, s, s, l, r, r, s, l, r, r, s, F
            };

            Track z = new Track("nederland", sectionsTrack1);
            Track y = new Track("België", sectionsTrack2);

            AddTrack(z);
            AddTrack(y);
        }

        static public void AddParticipant(IParticipant participant)
        {
            Competition.Participants.Add(participant);
        }

        static public void AddTrack(Track track)
        {
            Competition.Tracks.Enqueue(track);
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
