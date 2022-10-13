using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using static Model.DriverChangedEventsArgs;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public DateTime StartTime { get; set; }
        public List<IParticipant>? Participants { get; set; } = new List<IParticipant> { };
        public event EventHandler<DriverChangedEventsArgs> driverChanged;

        private System.Timers.Timer timer;

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();

        public Race(Track track, List<IParticipant>? participants)
        {
            Track = track;
            Participants = participants;
            RandomizeEquipment();
            
            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object? sender, EventArgs e)
        {
            ChangeDriverPosition(Track);
            driverChanged?.Invoke(this, new DriverChangedEventsArgs(Track));
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }

        public void RandomizeEquipment()
        {
            foreach (var participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(1,10);
                participant.Equipment.Performance = _random.Next(1,5);
            }
        }

        public void GiveStartPositions(Track track, List<IParticipant>? participants)
        {
            int nummer = 0;
            foreach (Section s in Data.CurrentRace.Track.Sections)
            {
                var SDL = GetSectionData(s).Left;
                var SDR = GetSectionData(s).Right;
                var deelnemer = participants?[nummer];

                if (s.SectionType.ToString() == "StartE" && participants?.Count < 8)
                {
                    if (nummer % 2 == 1)
                    {
                        SDL = participants[nummer];
                        nummer++;
                    }
                    else if (nummer % 2 == 0)
                    {
                        SDR = participants?[nummer];
                        nummer++;
                    }
                }
                else if (!(participants?.Count <= 8))
                {
                    Console.WriteLine("Er zijn te veel deelnemers");
                }
            }
        }

        public void start()
        {
            timer.Start();
            GiveStartPositions(Track, Participants);
        }

        public void ChangeDriverPosition(Track track)
        {
            LinkedList<Section> Sections = Data.CurrentRace.Track.Sections;

            for (int i = 0; i < Sections.Count; i++)
            {
                //making it easy to reference to regular used attributes
                Section section = Sections.ElementAt(i);

                SectionData sd = GetSectionData(section);
                SectionData sdN = GetSectionData(section);
                int j = i + 1;
                if (j < Sections.Count)
                {
                    Section sectionN = Sections.ElementAt(i + 1);
                    sdN = GetSectionData(sectionN);
                }
                else { }

                if (sd.Right != null && sdN.Right == null)
                {
                    sdN.Right = sd.Right;
                    sd.Right = null;
                }

                //if (sd.Left != null && sdN.Left == null)
                //{
                //    sdN.Left = sd.Left;
                //}

                //if (sd.Left != null && sdN.Left == null)
                //{
                //    //the formula to calculate the actual speed of a driver
                //    int performance = sd.Left.Equipment.Performance;
                //    int speed = sd.Left.Equipment.Speed;
                //    int actualSpeed = speed * performance;

                //    if ((sd.DistanceLeft + actualSpeed) >= 100)
                //    {
                //        //adding the driver to his next position
                //        sdN.Left = sd.Left;
                //        sdN.DistanceLeft = sd.DistanceLeft - 100;

                //        //removing the drivers from last position
                //        sd.Left = null;
                //        sd.DistanceLeft = 0;
                //    }
                //}

                //if (sd.Right != null)
                //{
                //    int performance = sd.Right.Equipment.Performance;
                //    int speed = sd.Right.Equipment.Speed;
                //    int actualSpeed = speed * performance;

                //    if ((sd.DistanceRight += actualSpeed) >= 100)
                //    {
                //        sdN.Right = sd.Left;
                //        sdN.DistanceRight = sd.DistanceRight - 100;

                //        sd.Right = null;
                //        sd.DistanceRight = 0;
                //    }
                //}
            }
        }
    }
}
