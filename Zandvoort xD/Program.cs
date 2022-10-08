﻿using Controller;
using Model;
using System;
using static Controller.Data;
using static Zandvoort_xD.Virtualization;

namespace Zandvoort_xD // Note: actual namespace depends on the project name.
{
    static class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            #region trackNameTest
            //Console.WriteLine("Track: " + CurrentRace.Track.Name + "\n");
            //CurrentRace.RandomizeEquipment();
            //Console.WriteLine("Name \t\tColor \tPerformance \tQuality \tSpeed \tBroken");
            //foreach (var Participant in CurrentRace.Participants)
            //{
            //    Console.WriteLine(Participant.Name + "\t\t" 
            //                    + Participant.TeamColor + "\t" 
            //                    + Participant.Equipment.Performance + "\t\t" 
            //                    + Participant.Equipment.Quality + "\t\t" 
            //                    + Participant.Equipment.Speed + "\t" 
            //                    + Participant.Equipment.IsBroken);
            //}
            #endregion
            Virtualization.DrawTrack(CurrentRace.Track);
            Data.CurrentRace.GiveStartPositions(CurrentRace.Track, CurrentRace.Participants);
            Virtualization.DrawTrack(CurrentRace.Track);
            CurrentRace.driverChanged += Virtualization.DriverChanged;

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
