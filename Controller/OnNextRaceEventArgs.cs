using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class OnNextRaceEventArgs
    {
        public Race Race { get; set; }

        public OnNextRaceEventArgs(Race race)
        {
            Race = race;
        }
    }
}
