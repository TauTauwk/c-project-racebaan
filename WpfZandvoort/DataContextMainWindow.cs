using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfZandvoort
{
    public class DataContextMainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TrackName { get; set; }

        public void OnDriverChanged(object? sender, DriverChangedEventsArgs e)
        {
            TrackName = e.Track.Name.ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
