using System;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace DataStream
{
    public partial class Form1 : Form
    {
        public ChartValues<ObservableValue> Values { get; set; }

        public Form1()
        {
            InitializeComponent();

            cartesianChart1.DataTooltip = null;
            cartesianChart1.Hoverable = false;

            Values = new ChartValues<ObservableValue>
            {
                new ObservableValue(3),
                new ObservableValue(6),
                new ObservableValue(7),
                new ObservableValue(4),
                new ObservableValue(2)
            };

            cartesianChart1.LegendLocation = LegendLocation.Right;

            cartesianChart1.Series.Add(new LineSeries
            {
                Values = Values,
                StrokeThickness = 4,
                PointGeometrySize = 0
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var r = new Random();
            Values.Add(new ObservableValue(r.Next(-20, 20)));
        }
    }
}
