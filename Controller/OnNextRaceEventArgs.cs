namespace Controller
{
    public class OnNextRaceEventArgs
    {
        public Race Race { get; set; }

        public OnNextRaceEventArgs(Race race)
        {
            Race = race;
        }
    }
}
