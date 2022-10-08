using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using static Model.DriverChangedEventsArgs;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public DateTime StartTime { get; set; }

        public List<IParticipant> Participants { get; set; } = new List<IParticipant> { };
        public event DriverChanged driverChanged;

        private System.Timers.Timer timer;

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            GiveStartPositions(track, participants);
            timer = new System.Timers.Timer(500);
            timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            else
            {
                _positions[section] = new SectionData();
            }
            return _positions[section];
        }

        public void RandomizeEquipment()
        {
            foreach (var participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(10);
                participant.Equipment.Performance = _random.Next(5);
            }
        }

        public void GiveStartPositions(Track track, List<IParticipant> participants)
        {
            int nummer = 0;
            foreach (Section s in track.Sections)
            {
                var SDL = GetSectionData(s).Left;
                var SDR = GetSectionData(s).Right;
                var deelnemer = participants[nummer];

                if (s.SectionType.ToString() == "StartE" && participants.Count < 8)
                {
                    if (nummer % 2 == 1)
                    {
                        SDL = participants[nummer];
                        nummer++;
                    }
                    else if (nummer % 2 == 0)
                    {
                        SDR = participants[nummer];
                        nummer++;
                    }
                }
                else if (!(participants.Count <= 8))
                {
                    Console.WriteLine("Er zijn te veel deelnemers");
                }
            }
        }

        public void start()
        {
            timer.Start();
        }

        public void ChangeDriverPosition()
        {
            LinkedList<Section> Sections = Track.Sections;

            foreach (Section s in Sections)
            {
                SectionData sd = GetSectionData(s);
            }
            for (int i = 0; i < Sections.Count; i++)
            {
                Sections[i];
                
                if (Se.Left == null)
                {

                }
            }
            DriverChanged driverChanged;
        }
    }
}
