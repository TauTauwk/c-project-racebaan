using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DriverChangedEventsArgs : EventArgs
    {
        public Track Track { get; set; }

        public delegate void DriverChanged(object? sender, DriverChangedEventsArgs e);
    }
}
