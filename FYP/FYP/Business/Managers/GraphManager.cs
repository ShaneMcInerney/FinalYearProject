using FYP.Business.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FYP.Business.Managers
{
    /// <summary>
    /// enum for filtering the displayed graph data
    /// </summary>
    public enum GraphType
    {
        Month,
        Week,
        Year
    }

    public class GraphManager : BaseManager
    {
        #region Fields

        private List<double> m_accelerometerVectorMagnitudes;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GraphManager()
        {
            this.m_accelerometerVectorMagnitudes = new List<double>();
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_accelerometerVectorMagnitudes
        /// </summary>
        public List<double> AccelerometerVectorMagnitudes
        {
            get
            {
                return m_accelerometerVectorMagnitudes;
            }

            set
            {
                m_accelerometerVectorMagnitudes = value;
            }
        }


        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Creates an oxyplot graph of the user's weight
        /// </summary>
        /// <param name="title">title to be displayed on the graph</param>
        /// <param name="pointsToPlot">list of weight entries to display</param>
        /// <param name="graphType">the graph type (week,month,year)</param>
        /// <returns>Oxyplot graph of weight</returns>
        public PlotModel CreateWeightPlotModel(string title, List<WeightEntry> pointsToPlot, GraphType graphType)
        {
            //creating new plot model
            var plotModel = new PlotModel { Title = title };
            //if there are points to plot
            if (pointsToPlot.Count > 0)
            {
                //Creating new line series
                var series = CreateLineSeries();
                //if graph type is week
                if (graphType == GraphType.Week)
                {
                    //get start of week
                    DateTime startOfWeek = GetStartOfWeek();
                    //sets length of x axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.Day, Minimum = startOfWeek.Day, IntervalLength = 50 });
                    //sets length of y axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = pointsToPlot.Max(x => x.Weight) + 40, Minimum = pointsToPlot.Min(x => x.Weight) - 40, IntervalLength = 20 });
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {
                        series.Points.Add(new DataPoint(p.Date.Day, p.Weight));
                    }
                }
                //if graph type is month
                if (graphType == GraphType.Month)
                {
                    //get start of month
                    var startOfTheMonth = GetStartOfMonth();
                    //sets length of x axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.Day, Minimum = startOfTheMonth.Day, IntervalLength = 20 });
                    //sets length of y axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = pointsToPlot.Max(x => x.Weight) + 40, Minimum = pointsToPlot.Min(x => x.Weight) - 40, IntervalLength = 20 });
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {
                        series.Points.Add(new DataPoint(p.Date.Day, p.Weight));
                    }
                }
                //if graph type is year
                if (graphType == GraphType.Year)
                {
                    //get start of month
                    var startOfTheYear = GetStartOfYear();
                    //sets length of x axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.DayOfYear, Minimum = startOfTheYear.DayOfYear, IntervalLength = 15 });
                    //sets length of y axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = pointsToPlot.Max(x => x.Weight) + 40, Minimum = pointsToPlot.Min(x => x.Weight) - 40, IntervalLength = 20 });
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {
                        series.Points.Add(new DataPoint(p.Date.DayOfYear, p.Weight));
                    }
                }
                //add series to plot model
                plotModel.Series.Add(series);
            }
            //return oxyplot model
            return plotModel;
        }

        /// <summary>
        /// Creates an oxyplot graph of the user's hourly step entries
        /// </summary>
        /// <param name="title">title to be displayed on the graph</param>
        /// <param name="pointsToPlot">list of step entries to display</param>
        /// <param name="graphType">the graph type (week,month,year)</param>
        /// <returns>Oxyplot graph of step entries</returns>
        public PlotModel CreateStepPlotModel(string title, List<StepEntry> pointsToPlot, GraphType graphType)
        {
            //creating new plot model
            var plotModel = new PlotModel { Title = title };
            //Creating new line series
            var series = CreateLineSeries();
            //if there are points to plot
            if (pointsToPlot.Count > 0)
            {
                //if graph type is week
                if (graphType == GraphType.Week)
                {
                    //get start of year
                    DateTime startOfWeek = GetStartOfYear();
                    //set up x axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.Day, Minimum = startOfWeek.Day });
                    //set up y axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = pointsToPlot.Max(x => x.Count) + 1000, Minimum = pointsToPlot.Min(x => x.Count) - 1000 });
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {
                        double x = double.Parse(p.Date.Day.ToString() + "." + p.Date.Hour.ToString() + p.Date.Minute.ToString());
                        series.Points.Add(new DataPoint(x, p.Count));
                    }
                }
                //if graph type is month
                if (graphType == GraphType.Month)
                {
                    //get start of month
                    var startOfTheMonth = GetStartOfMonth();
                    //set up x axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.Day, Minimum = startOfTheMonth.Day });
                    //setup y axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = pointsToPlot.Max(x => x.Count) + 1000, Minimum = pointsToPlot.Min(x => x.Count) - 1000 });
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {

                        double x = double.Parse(p.Date.Day.ToString() + "." + p.Date.Hour.ToString() + p.Date.Minute.ToString());
                        series.Points.Add(new DataPoint(x, p.Count));
                    }
                }
                //if graph type is year
                if (graphType == GraphType.Year)
                {
                    //get start of year
                    var startOfTheYear = GetStartOfYear();
                    //set up x axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.DayOfYear, Minimum = startOfTheYear.DayOfYear });
                    //set up y axis
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = pointsToPlot.Max(x => x.Count) + 1000, Minimum = pointsToPlot.Min(x => x.Count) - 1000 });
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {
                        series.Points.Add(new DataPoint(p.Date.DayOfYear, p.Count));
                    }
                }
                //add series to plot model
                plotModel.Series.Add(series);
            }
            //return oxyplot model
            return plotModel;
        }

        /// <summary>
        /// Creates an oxyplot graph of the user's sleep entries
        /// </summary>
        /// <param name="title">title to be displayed on the graph</param>
        /// <param name="pointsToPlot">list of sleep entries to display</param>
        /// <param name="graphType">the graph type (week,month,year)</param>
        /// <returns>Oxyplot graph of sleep entries</returns>
        public PlotModel CreateSleepPlotModel(string title, List<SleepEntry> pointsToPlot, GraphType graphType)
        {
            //creating new plot model
            var plotModel = new PlotModel { Title = title };
            //if there are points to plot
            if (pointsToPlot.Count > 0)
            {
                //set up y axis
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = pointsToPlot.Max(x => x.SleepLength.Hours) + 2, Minimum = pointsToPlot.Min(x => x.SleepLength.Hours) - 2 });
                //setting up x axis
                GraphTypeSwitcher(plotModel, graphType);
                //creating new line series
                var series = CreateLineSeries();
                //if graph is year
                if (graphType == GraphType.Year)
                {
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {
                        //add day of year points to graph
                        series.Points.Add(new DataPoint(p.Date.DayOfYear, p.SleepLength.Hours));
                    }
                }
                //if graph is month or week
                else
                {
                    //adding points to data series
                    foreach (var p in pointsToPlot)
                    {
                        //add day of month to graph
                        series.Points.Add(new DataPoint(p.Date.Day, p.SleepLength.Hours));
                    }
                }
                //add series to plot model
                plotModel.Series.Add(series);
            }
            //return oxyplot model
            return plotModel;
        }

        /// <summary>
        /// Creates a realtime graph of accelerometer readings vector magnitudes
        /// </summary>
        /// <param name="title">title to be displayed on graph</param>
        /// <param name="vectorMagniitude">value of the vector's magnitude, to be graphed</param>
        /// <returns>Oxyplot graph of accelerometer's vector magnitude</returns>
        public PlotModel CreateRealtimeAccelerometerGraph(string title, double vectorMagniitude)
        {
            //creating new plot model
            var plotModel = new PlotModel { Title = title };
            //addin vector magntude to list of vector magnitudes
            AccelerometerVectorMagnitudes.Add(vectorMagniitude);
            //if there are lots to point
            if (AccelerometerVectorMagnitudes.Count > 0)
            {
                //set up x axis
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = 20, Minimum = 0 });
                //set up y axis
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 20, Minimum = -2 });
                //creating new line series
                var series = CreateLineSeries();
                //adding points to data series
                foreach (var p in AccelerometerVectorMagnitudes)
                {
                    series.Points.Add(new DataPoint(AccelerometerVectorMagnitudes.IndexOf(p), p));
                }
                //add series to plot model
                plotModel.Series.Add(series);
            }
            //if there are 30 or more vecotr magnitudes added
            if (AccelerometerVectorMagnitudes.Count > 30)
            {
                //clear list
                AccelerometerVectorMagnitudes.Clear();
            }
            //return oxyplot model
            return plotModel;
        }

        /// <summary>
        /// Switches the axiis of the graph
        /// </summary>
        /// <param name="plotModel">Oxyplot model to adjust</param>
        /// <param name="graphType">Graph type to adjust axis for</param>
        public void GraphTypeSwitcher(PlotModel plotModel, GraphType graphType)
        {
            //if graph type is week
            if (graphType == GraphType.Week)
            {
                //get start of week
                DateTime startOfWeek = GetStartOfWeek();
                //set x axis for week
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.Day, Minimum = startOfWeek.Day });
            }
            //if graph type is month
            if (graphType == GraphType.Month)
            {
                //get stat of month
                var startOfTheMonth = GetStartOfMonth();
                //set x axis for month
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.Day, Minimum = startOfTheMonth.Day });
            }
            //if graph type is year
            if (graphType == GraphType.Year)
            {
                //get start of year
                var startOfTheYear = GetStartOfYear();
                //set x axis for year
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = DateTime.Now.DayOfYear, Minimum = startOfTheYear.DayOfYear });
            }
        }

        /// <summary>
        /// Create a new line series
        /// </summary>
        /// <returns>new line series</returns>
        private LineSeries CreateLineSeries()
        {
            var series = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.White
            };
            return series;
        }

        #endregion //Methods

    }
}
