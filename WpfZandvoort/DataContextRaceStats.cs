using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfZandvoort
{
    public class DataContextRaceStats : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<IParticipant> Participants { get; set; }
        public Dictionary<IParticipant, int> _lap { get; set; }
        public Dictionary<IParticipant, string> _finished { get; set; }

        public void OnDriverChanged(object sender, DriverChangedEventsArgs e)
        {
            Race _race = (Race)sender;

            Participants = _race.Participants;

            Dictionary<IParticipant, int> Finished = _race._FinishedProp;
            Dictionary<IParticipant, int> lap = new Dictionary<IParticipant, int>();
            Dictionary<IParticipant, string> finished = new Dictionary<IParticipant, string>();
            foreach (var participant in Participants)
            {
                lap.Add(participant, 1);
                finished.Add(participant, "nee");
            }

            foreach (var KeyValue in Finished)
            {
                if (KeyValue.Value == 2)
                {
                    finished[KeyValue.Key] = "ja";
                    lap[KeyValue.Key] = 2;
                }
                else
                {
                    lap[KeyValue.Key] = 2;
                }
            }

            _lap = lap;
            _finished = finished;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void OnNextRace(object sender, OnNextRaceEventArgs e)
        {
            Participants = new List<IParticipant>();
            _lap = new Dictionary<IParticipant, int>();
            _finished = new Dictionary<IParticipant, string>();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
