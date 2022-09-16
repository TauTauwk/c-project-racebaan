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
            Console.WriteLine(CurrentRace.Track.Name);

            for (; ; )
            {
                Thread.Sleep(100);
            }



        }
    }
}
