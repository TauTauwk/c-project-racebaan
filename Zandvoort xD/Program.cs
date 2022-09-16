using Controller;
using Model;
using System;
using static Controller.Data;

namespace Zandvoort_xD // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            NextRace();
            Console.WriteLine("Track: " + CurrentRace.Track.Name + "\n");
            CurrentRace.RandomizeEquipment();
            Console.WriteLine("Name \t\tColor \tPerformance \tQuality \tSpeed \tBroken");
            foreach (var Participant in CurrentRace.Participants)
            {
                Console.WriteLine(Participant.Name + "\t\t" 
                                + Participant.TeamColor + "\t" 
                                + Participant.Equipment.Performance + "\t\t" 
                                + Participant.Equipment.Quality + "\t\t" 
                                + Participant.Equipment.Speed + "\t" 
                                + Participant.Equipment.IsBroken);
            }

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
