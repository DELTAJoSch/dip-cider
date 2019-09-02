using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    public class PlotManager
    ///ToDo: Redesign in order to allow any List with floats
    {
        private PlotModel _plot;
        private string _title;
        private List<LineSeries> Series;
        public PlotManager()
        {
            Series = new List<LineSeries>();
        }

        public PlotModel GetPlotModel(string Title)
        {
            Create(Title);

            return _plot;
        }

        private void Create(string Title)
        {
            //Create a new PlotModel
            PlotModel CreatePlot = new PlotModel();
            CreatePlot.Title = _title;

            Parallel.ForEach(Series, x =>
            {
                CreatePlot.Series.Add(x);
            });

            _plot = CreatePlot;
        }

        public void CreatePDF(string Title, string fileName)
        {
            Create(Title);

            using (var stream = File.Create(fileName))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(_plot, stream);
            }
        }
        private async Task<List<DataPoint>> GetLineSeriesAsync(List<float> data, int interval)
        ///This Task reads all the data points and convert them to line series
        ///It´s executed asynchronously, so this doesn't block when large datasets are involved
        {
            List<DataPoint> Points = new List<DataPoint>();

            double t = 0;

            foreach (float p in data)
            {
                Points.Add(new DataPoint(t, p));
                t += interval;
            }

            return Points;
        }

        private async void CreateSeries(List<float> data, string name, OxyColor color, int interval)
        {
            var Task = GetLineSeriesAsync(data, interval);

            LineSeries ls = new LineSeries();

            ls.Title = name;
            ls.Color = color;

            await Task;

            ls.Points.AddRange(Task.Result);
            ls.TextColor = OxyColors.Black;

            Series.Add(ls);
        }

        public void AddLineSeries(List<float> data, string name)
        {
            Random random = new Random();

            CreateSeries(data, name, OxyColor.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)), 1);
        }

        public void AddLineSeries(List<float> data, string name, OxyColor color)
        {
            CreateSeries(data, name, color, 1);
        }

        public void AddLineSeries(List<float> data, string name, OxyColor color, int interval)
        {
            CreateSeries(data, name, color, interval);
        }

        public void AddLineSeries(List<float> data, string name, int interval)
        {
            Random random = new Random();

            CreateSeries(data, name, OxyColor.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)), interval);
        }
    }
}
