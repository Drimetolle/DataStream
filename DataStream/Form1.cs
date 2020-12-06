using System;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace DataStream
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

            Charting.For<MeasureModel>(mapper);

            ChartValues = new ChartValues<MeasureModel>();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = ChartValues,

                    StrokeThickness = 4,
                    Fill = System.Windows.Media.Brushes.Transparent
                }
            };
            cartesianChart1.AxisX.Add(new Axis
            {
                DisableAnimations = true,
                LabelFormatter = value => new System.DateTime((long)value).ToString("mm:ss"),
                Separator = new Separator
                {
                    Step = TimeSpan.FromSeconds(1).Ticks
                }
            });

            SetAxisLimits(System.DateTime.Now);

            Timer = new Timer
            {
                Interval = 1000
            };
            Timer.Tick += TimerOnTick;
            R = new Random();
            Timer.Start();
        }

        public ChartValues<MeasureModel> ChartValues { get; set; }
        public Timer Timer { get; set; }
        public Random R { get; set; }

        private void SetAxisLimits(DateTime now)
        {
            cartesianChart1.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(1).Ticks;
            cartesianChart1.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(8).Ticks;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            var now = DateTime.Now;

            ChartValues.Add(new MeasureModel
            {
                DateTime = now,
                Value = R.Next(0, 10)
            });

            SetAxisLimits(now);

            if (ChartValues.Count > 30) ChartValues.RemoveAt(0);
        }
    }
}
