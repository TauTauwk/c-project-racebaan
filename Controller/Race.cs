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
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions;

        public SectionData GetSectionData(Section section) 
        {            
            if (!(_positions.ContainsKey(section)))
            {
                _positions[section] = new SectionData();
            }
            return _positions[section];
        }

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
        }

        public void RandomizeEquipment() 
        {
            foreach (var participant in Participants) 
            {
                participant.Equipment.Quality = _random.Next(100);
                participant.Equipment.Performance = _random.Next(100);
            }
        }
    }
}
