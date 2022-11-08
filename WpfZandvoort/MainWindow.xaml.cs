using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xaml.Permissions;
using Controller;
using Model;

namespace WpfZandvoort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StatisticsCompetition StatComp;
        private StatisticsRace StatRace;

        public MainWindow()
        {
            InitializeComponent();

            Data.Initialize();
            Data.NextRaceEvent += OnNextRace;
            Data.NextRace();
        }

        private void OnNextRace(object? sender, OnNextRaceEventArgs e)
        {
            DoImage.ClearImageCache();
            WPFVisualization.Initialize(e.Race);
            
            e.Race.DriverChanged += DriverChanged;
            e.Race.FinishedRace += OnFinishedRace;

            Dispatcher.Invoke(() => { e.Race.DriverChanged += ((DataContextMainWindow)DataContext).OnDriverChanged; });
        }

        private void DriverChanged(object? sender, DriverChangedEventsArgs e)
        {
            this.WeirdImage.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.WeirdImage.Source = null;
                    this.WeirdImage.Source = WPFVisualization.DrawTrack(e.Track);
                })
            );
        }

        private void OnFinishedRace(object sender, EventArgs e)
        {
            DoImage.ClearImageCache();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_CompStats_Click(object sender, RoutedEventArgs e)
        {
            StatComp = new StatisticsCompetition();
            Data.NextRaceEvent += ((DataContextCompStats)StatComp.DataContext).OnNextRace;
            ((DataContextCompStats)StatComp.DataContext).OnNextRace(null, new OnNextRaceEventArgs(Data.CurrentRace));

            StatComp.Show();
        }

        private void MenuItem_RaceStats_Click(object sender, RoutedEventArgs e)
        {
            StatRace = new StatisticsRace();
            Data.NextRaceEvent += ((DataContextRaceStats)StatRace.DataContext).OnNextRace;
            ((DataContextRaceStats)StatRace.DataContext).OnNextRace(null, new OnNextRaceEventArgs(Data.CurrentRace));
            Data.CurrentRace.DriverChanged += ((DataContextRaceStats)StatRace.DataContext).OnDriverChanged;

            StatRace.Show();
        }
    }
}
