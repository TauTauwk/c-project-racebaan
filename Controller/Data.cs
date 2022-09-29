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

            var ln = SectionTypes.LeftN;
            var le = SectionTypes.LeftE;
            var ls = SectionTypes.LeftS;
            var lw = SectionTypes.LeftW;
            var rn = SectionTypes.RightN;
            var re = SectionTypes.RightE;
            var rs = SectionTypes.RightS;
            var rw = SectionTypes.RightW;
            var Sn = SectionTypes.StartN;
            var Se = SectionTypes.StartE;
            var Ss = SectionTypes.StartS;
            var Sw = SectionTypes.StartW;
            var sn = SectionTypes.StraightN;
            var se = SectionTypes.StraightE;
            var ss = SectionTypes.StraightS;
            var sw = SectionTypes.StraightW;
            var Fn = SectionTypes.FinishN;
            var Fe = SectionTypes.FinishE;
            var Fs = SectionTypes.FinishS;
            var Fw = SectionTypes.FinishW;

            if (trackNum == 0)
            {
                SectionTypes[] track1 = new SectionTypes[]
                {
                    Se, Se, Se, Se, ln, lw, sw, sw, ls, le ,Fe
                };
                Track a = new Track("Nederland", track1);
                Competition.Tracks.Enqueue(a);
            }
            else 
            {
                SectionTypes[] track2 = new SectionTypes[]
                {
                    Se, Se, Se, Se, rs, le, rs, rw, sw, sw, ls, rw, rn, sn, lw, rn, re, se, Fe
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
