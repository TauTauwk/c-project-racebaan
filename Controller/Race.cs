using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; } = new List<IParticipant> { };
        public DateTime StartTime { get; set; }

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            GiveStartPositions(track, participants);
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
                participant.Equipment.Quality = _random.Next(100);
                participant.Equipment.Performance = _random.Next(100);
            }
        }

        public void GiveStartPositions(Track track, List<IParticipant> participants)
        {
            int nummer = 0;
            foreach (Section s in track.Sections) {
                if (s.SectionType.ToString() == "StartE")
                {
                    if (nummer % 2 == 0)
                    {
                        GetSectionData(s).Left = participants[nummer];
                        nummer++;
                    }
                    else
                    {
                        GetSectionData(s).Right = participants[nummer];
                        nummer++;
                    }
                    Console.WriteLine(GetSectionData(s).Left.Name);
                }
            }
            foreach (var iets in _positions)
            {
                Console.WriteLine(iets);
            }
        }
    }
}
