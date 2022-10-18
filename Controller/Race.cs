﻿using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Timers;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public DateTime StartTime { get; set; }
        public List<IParticipant>? Participants { get; set; } = new List<IParticipant> { };
        public event EventHandler<DriverChangedEventsArgs> DriverChanged;
        public event EventHandler<EventArgs> FinishedRace;

        private System.Timers.Timer timer;

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Dictionary<IParticipant, int> _Finished = new Dictionary<IParticipant, int>();

        public Race(Track track, List<IParticipant>? participants)
        {
            Track = track;
            Participants = participants;
            RandomizeEquipment();
            //if a race is finished you want the event to be triggerd so the next track will appear
            FinishedRace += OnNextRace;
            
            //lets something happen every half a second
            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += OnTimedEvent;
        }

        //here is the something that happens
        private void OnTimedEvent(object? sender, EventArgs e)
        {
            ChangeDriverPosition(Track);
            DriverChanged?.Invoke(this, new DriverChangedEventsArgs(Track));
        }
        
        //get the sectionData for a section or adds data if the key was not already known
        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }

        //create some kind of randomness in the race
        private void RandomizeEquipment()
        {
            foreach (var participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(1,11);
                participant.Equipment.Performance = _random.Next(1,6);
            }
        }

        //all drivers need a start position on the grid
        private void GiveStartPositions(Track track, List<IParticipant>? participants)
        {
            int nummer = 0;
            foreach (Section s in track.Sections)
            {
                if (s.SectionType.ToString() == "StartE")
                {
                    if (nummer < (participants.Count - 1))
                    {
                        GetSectionData(s).Left = participants[nummer];
                        nummer++;
                        GetSectionData(s).Right = participants[nummer];
                        nummer++;
                    }
                }
            }
        }

        //starts the timer and will call to tell that drivers need a start position
        public void start()
        {
            timer.Start();
            GiveStartPositions(Track, Participants);
        }

        //every time someone moves this is triggerd
        private void ChangeDriverPosition(Track track)
        {
            int i = 0;
            while (i < track.Sections.Count()+1)
            {
                if (i < track.Sections.Count())
                {
                    SectionData sdP;
                    SectionData sdC;
                    //check if a driver is at the firt section than the previous will be the last section
                    if (track.Sections.ElementAt(i) == track.Sections.First())
                    {
                        sdP = GetSectionData(track.Sections.Last());
                        sdC = GetSectionData(track.Sections.ElementAt(i));
                    }
                    else
                    {
                        //chack if the sdP (previous section) is equal to the last than the sdC will be the First
                        if (track.Sections.ElementAt(i-1) == track.Sections.Last())
                        {
                            sdC = GetSectionData(track.Sections.First());
                            sdP = GetSectionData(track.Sections.Last());
                            i = 0;
                        }
                        //default
                        else
                        {
                            sdP = GetSectionData(track.Sections.ElementAt(i - 1));
                            sdC = GetSectionData(track.Sections.ElementAt(i));
                        }
                    }
                    //if sdC.Right is empty you want a driver to be able to go there
                    if (sdC.Right == null)
                    {
                        //but only if he has driven more than 100 meters
                        //comming from the previous section's right
                        if (sdP.DistanceRight >= 100)
                        {
                            sdC.Right = sdP.Right;
                            sdC.DistanceRight = sdP.DistanceRight - 100;
                            sdP.Right = null;
                            sdP.DistanceRight = 0;
                        }
                        //he can also come from the previous section's left
                        else if (sdP.DistanceLeft >= 100)
                        {
                            sdC.Right = sdP.Left;
                            sdC.DistanceRight = sdP.DistanceLeft - 100;
                            sdP.Left = null;
                            sdP.DistanceLeft = 0;
                        }
                    }
                    //is the section not free, keep driving on this section
                    else if (sdC.Right != null)
                    {
                        int performanceR = sdC.Right.Equipment.Performance;
                        int speedR = sdC.Right.Equipment.Speed;
                        int actualSpeedR = speedR * performanceR;
                        sdC.DistanceRight += actualSpeedR;
                    }
                    //same goes for the left
                    if (sdC.Left == null)
                    {
                        if (sdP.DistanceLeft >= 100)
                        {
                            sdC.Left = sdP.Left;
                            sdC.DistanceLeft = sdP.DistanceLeft - 100;
                            sdP.Left = null;
                            sdP.DistanceLeft = 0;
                        }
                        else if (sdP.DistanceRight >= 100)
                        {
                            sdC.Left = sdP.Right;
                            sdC.DistanceLeft = sdP.DistanceRight - 100;
                            sdP.Right = null;
                            sdP.DistanceRight = 0;
                        }
                    }
                    else if (sdC.Left != null)
                    {
                        int performanceL = sdC.Left.Equipment.Performance;
                        int speedL = sdC.Left.Equipment.Speed;
                        int actualSpeedL = speedL * performanceL;
                        sdC.DistanceLeft += actualSpeedL;
                    }
                    //check if a driver can pass the finish
                    if (track.Sections.Last() == track.Sections.ElementAt(i) && sdC.Left != null && sdC.DistanceLeft >= 100)
                    {
                        if (IsFinished(sdC.Left))
                        {
                            sdC.Left = null;
                            sdC.DistanceLeft = 0;
                        }
                    }
                    if (track.Sections.Last() == track.Sections.ElementAt(i) && sdC.Right != null && sdC.DistanceRight >= 100)
                    {
                        if (IsFinished(sdC.Right))
                        {
                            sdC.Right = null;
                            sdC.DistanceRight = 0;
                        }
                    }
                }
                //end of race
                //check if all the drivers have done their laps
                if (_Finished.Where(x => x.Value >= 2).Count() == Participants.Count())
                {
                    FinishedRace?.Invoke(this, EventArgs.Empty);
                    CleanUp();
                    break;
                }
                i++;
            }
        }

        //function counts the amount of laps a driver has ridden in a dictionary
        private int AmountOfLaps(IParticipant participant)
        {
            if (!_Finished.ContainsKey(participant))
            {
                _Finished.Add(participant, 1);
            }
            else if(_Finished.ContainsKey(participant))
            {
                _Finished[participant] += 1;
            }
            Console.SetCursorPosition(0, 0);
            foreach (var pair in _Finished)
            {
                Console.WriteLine(pair.ToString());
            }
            return _Finished[participant];
        }

        //chack if a driver has finished
        private bool IsFinished(IParticipant participant)
        {
            if (AmountOfLaps(participant) == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //cleans up the console after a race
        private void CleanUp()
        {
            DriverChanged = null;
            FinishedRace = null;
            timer.Stop();

            Console.Clear();
        }

        //needed to check if a new race wants to start
        private void OnNextRace(object? sender, EventArgs e)
        {
            Data.NextRace();
        }
    }
}
