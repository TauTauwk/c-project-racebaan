using Model;
using System;
using System.Collections.Generic;
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
        public event EventHandler<DriverChangedEventsArgs> driverChanged;

        private System.Timers.Timer timer;

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Dictionary<IParticipant, int> _Finished = new Dictionary<IParticipant, int>();

        public Race(Track track, List<IParticipant>? participants)
        {
            Track = track;
            Participants = participants;
            RandomizeEquipment();
            
            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object? sender, EventArgs e)
        {
            ChangeDriverPosition(Track);
            driverChanged?.Invoke(this, new DriverChangedEventsArgs(Track));
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }

        public void RandomizeEquipment()
        {
            foreach (var participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(1,11);
                participant.Equipment.Performance = _random.Next(1,6);
            }
        }

        public void GiveStartPositions(Track track, List<IParticipant>? participants)
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

        public void start()
        {
            timer.Start();
            GiveStartPositions(Track, Participants);
        }

        public void ChangeDriverPosition(Track track)
        {
            int i = 0;
            while (i < track.Sections.Count()+1)
            {
                if (i < track.Sections.Count())
                {
                    SectionData sdP;
                    SectionData sdC;
                    if (track.Sections.ElementAt(i) == track.Sections.First())
                    {
                        sdP = GetSectionData(track.Sections.Last());
                        sdC = GetSectionData(track.Sections.ElementAt(i));
                    }
                    else
                    {
                        if (track.Sections.ElementAt(i-1) == track.Sections.Last())
                        {
                            sdC = GetSectionData(track.Sections.First());
                            sdP = GetSectionData(track.Sections.Last());
                            i = 0;
                        }
                        else
                        {
                            sdP = GetSectionData(track.Sections.ElementAt(i - 1));
                            sdC = GetSectionData(track.Sections.ElementAt(i));
                        }
                    }

                    if (sdC.Right == null)
                    {
                        if (sdP.DistanceRight >= 100)
                        {
                            sdC.Right = sdP.Right;
                            sdP.Right = null;
                            sdP.DistanceRight = 0;
                        }
                        else if (sdP.DistanceLeft >= 100)
                        {
                            sdC.Right = sdP.Left;
                            sdP.Left = null;
                            sdP.DistanceLeft = 0;
                        }
                    }
                    else if (sdC.Right != null)
                    {
                        int performanceR = sdC.Right.Equipment.Performance;
                        int speedR = sdC.Right.Equipment.Speed;
                        int actualSpeedR = speedR * performanceR;
                        sdC.DistanceRight += actualSpeedR;
                    }
                    
                    if (sdC.Left == null)
                    {
                        if (sdP.DistanceLeft >= 100)
                        {
                            sdC.Left = sdP.Left;
                            sdP.Left = null;
                            sdP.DistanceLeft = 0;
                        }
                        else if (sdP.DistanceRight >= 100)
                        {
                            sdC.Left = sdP.Right;
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
                if (_Finished.Where(x => x.Value >= 2).Count() == Participants.Count())
                {
                    timer.Stop();
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("The Race Has Ended");
                    break;
                }
                i++;
            }
        }

        public int AmountOfLaps(IParticipant participant)
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

        public bool IsFinished(IParticipant participant)
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
    }
}
