using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; } = new Queue<Track>();

        public Track NextTrack()
        {
            //if there is any track left in the queue dequeue it
            if (Tracks.Count > 0)
            {
                return Tracks.Dequeue();
            }
                return null;
            
        }
    }
}
