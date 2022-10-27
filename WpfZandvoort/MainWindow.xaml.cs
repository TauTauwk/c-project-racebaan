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
using Controller;
using Model;

namespace WpfZandvoort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            Dispatcher.Invoke(() => { e.Race.DriverChanged += DriverChanged; });
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
    }
}
