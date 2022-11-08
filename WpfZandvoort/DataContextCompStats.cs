using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;

namespace WpfZandvoort
{
    public class DataContextCompStats : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<IParticipant> Participants { get; set; }

        public void OnNextRace(object? sender, OnNextRaceEventArgs e)
        {
            var _participants =
                from participant in e.Race.Participants
                orderby participant.Points descending
                select participant;

            Participants = _participants.ToList();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }

}

