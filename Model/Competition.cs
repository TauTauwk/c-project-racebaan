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
        public Queue<Track> Tracks { get; set; }

        public Track? NextTrack()
        {
            //if there is any track left in the queue dequeue it
            if (Tracks.Any())
            {
                return Tracks.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}
