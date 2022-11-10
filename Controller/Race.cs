using Model;

namespace Controller
{
    public class Race
    {
        public Track track { get; set; }
        public DateTime StartTime { get; set; }

        public List<IParticipant>? Participants { get; set; } = new List<IParticipant> { };

        public event EventHandler<DriverChangedEventsArgs> DriverChanged;
        public event EventHandler<EventArgs> FinishedRace;

        private System.Timers.Timer timer;

        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        public Dictionary<IParticipant, int> _Finished = new Dictionary<IParticipant, int>();

        public Dictionary<IParticipant, int> _FinishedProp { get { return _Finished; } }

        public Race(Track track, List<IParticipant>? participants)
        {
            this.track = track;
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
            MakeBroken();
            FracturedButWhole();
            ChangeDriverPosition(track);
            DriverChanged?.Invoke(this, new DriverChangedEventsArgs(track));
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
        
        //randomly makes a car broken based on their quality
        public void MakeBroken()
        {
            foreach (var participant in Participants)
            {
                int quality = participant.Equipment.Quality;
                int formula = 100 - (quality * 10); //quality 10 never breaks
                if (formula != 0)
                {
                    int chance = _random.Next(1, (formula + 1)); //the number from the formula included

                    if (!participant.Equipment.IsBroken && chance == 1)
                    {
                        participant.Equipment.IsBroken = true;
                    }
                }
            }
        }

        public void FracturedButWhole()
        {
            foreach (var participant in Participants)
            {
                if (participant.Equipment.IsBroken)
                {
                    int quality = participant.Equipment.Quality;
                    int formula = 10 - quality; //doesn't have to be 11 because quality 10 will never be broken
                    int chance = _random.Next(1, (formula + 1)); //the number from the formula included

                    if (chance == 1) //quality is max 10 11-10 = 1 
                    {
                        participant.Equipment.IsBroken = false;
                        if (participant.Equipment.Speed > 5) //only if the speed is greater than 5 otherwise they will be too slow
                        {
                            participant.Equipment.Speed -= 1; //after it is broken down speed will decrease by 1 point
                        }
                    }
                }
            }
        }

        //all drivers need a start position on the grid
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

        //starts the timer and will call to tell that drivers need a start position
        public void Start()
        {
            timer.Start();
            GiveStartPositions(track, Participants);
        }

        //every time someone moves this is triggerd
        public void ChangeDriverPosition(Track track)
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
                        if (sdP.Right != null && sdP.DistanceRight >= 100 && !sdP.Right.Equipment.IsBroken)
                        {
                            sdC.Right = sdP.Right;
                            sdC.DistanceRight = sdP.DistanceRight - 100;
                            sdP.Right = null;
                            sdP.DistanceRight = 0;
                        }
                        //he can also come from the previous section's left
                        else if (sdP.Left != null && sdP.DistanceLeft >= 100 && !sdP.Left.Equipment.IsBroken)
                        {
                            sdC.Right = sdP.Left;
                            sdC.DistanceRight = sdP.DistanceLeft - 100;
                            sdP.Left = null;
                            sdP.DistanceLeft = 0;
                        }
                    }
                    //is the section not free, keep driving on this section
                    else if (sdC.Right != null && !sdC.Right.Equipment.IsBroken)
                    {
                        int performanceR = sdC.Right.Equipment.Performance;
                        int speedR = sdC.Right.Equipment.Speed;
                        int actualSpeedR = speedR * performanceR;

                        sdC.DistanceRight += actualSpeedR;
                    }
                    //same goes for the left
                    if (sdC.Left == null)
                    {
                        if (sdP.Left != null && sdP.DistanceLeft >= 100 && !sdP.Left.Equipment.IsBroken)
                        {
                            sdC.Left = sdP.Left;
                            sdP.Left = null;
                            sdP.DistanceLeft = 0;
                        }
                        else if (sdP.Right != null && sdP.DistanceRight >= 100 && !sdP.Right.Equipment.IsBroken)
                        {
                            sdC.Left = sdP.Right;
                            sdP.Right = null;
                            sdP.DistanceRight = 0;
                        }
                    }
                    else if (sdC.Left != null && !sdC.Left.Equipment.IsBroken)
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
                    for (int j = 0; j < _Finished.Count(); j++)
                    {
                        _Finished.ElementAt(j).Key.Points += _Finished.Count() - j;
                    }
                    FinishedRace?.Invoke(this, EventArgs.Empty);
                    CleanUp();
                    break;
                }
                i++;
            }
        }

        //function counts the amount of laps a driver has ridden in a dictionary
        public int AmountOfLaps(IParticipant participant)
        {
            if (!_Finished.ContainsKey(participant))
            {
                _Finished.Add(participant, 1);
            }
            else if(_Finished.ContainsKey(participant))
            {
                _Finished.Remove(participant);
                _Finished.Add(participant, 2);
            }
            return _Finished[participant];
        }

        //chack if a driver has finished
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

        //cleans up the console after a race
        private void CleanUp()
        {
            DriverChanged = null;
            FinishedRace = null;
            timer.Stop();
        }

        //needed to check if a new race wants to start
        private void OnNextRace(object? sender, EventArgs e)
        {
            Data.NextRace();
        }
    }
}
