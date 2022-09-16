using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    static class Data
    {
        static public Competition competition { get; set; }

        static public void Initialize()
        {
            competition = new Competition();
            Car a = new Car(100, 100, 100, false);
            Driver b = new Driver("driver1", 0, a, TeamColors.Blue);
            Car c = new Car(100, 100, 100, false);
            Driver d = new Driver("driver1", 0, a, TeamColors.Blue);
            Car e = new Car(100, 100, 100, false);
            Driver f = new Driver("driver1", 0, a, TeamColors.Blue);
            AddParticipant(b);
            AddParticipant(d);
            AddParticipant(f);

            SectionTypes[] sections = new SectionTypes[7];
            sections[0] = SectionTypes.StartGrid;
            sections[1] = SectionTypes.LeftCorner;
            sections[2] = SectionTypes.LeftCorner;
            sections[3] = SectionTypes.Straight;
            sections[4] = SectionTypes.Straight;
            sections[5] = SectionTypes.LeftCorner;
            sections[6] = SectionTypes.LeftCorner;
            sections[7] = SectionTypes.Finish;
            
            Track z = new Track("nederland", sections);
        }

        static public void AddParticipant(IParticipant participant) 
        {
            competition.Participants.Add(participant);
        }

        static public void AddTrack(Track track)
        {
            competition.Tracks.Enqueue(track);
        }
    }
}
