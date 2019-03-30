using DevExpress.XtraCharts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;

namespace NorskTipping
{
    [TestClass]
    public class PreviousResultsTest
    {
        public const string SavePath = @"c:\NorskTipping\";
        [TestMethod]
        public void GetHistoricSave()
        {
            ResultsRepository.FetchResultsToDisk(SavePath);
        }

        [DataRow(500)]
        [DataRow(400)]
        [DataRow(300)]
        [DataRow(200)]
        [DataRow(100)]
        [DataRow(50)]
        [DataRow(40)]
        [DataRow(30)]
        [DataRow(20)]
        [DataRow(10)]
        [DataRow(5)]
        [DataRow(0)]
        [TestMethod]
        public void GetHistoric(int max)
        {
            if (max == 0)
                max = ResultsRepository.Current;
            var allNumbers = new ArrayList();
            var lottoString = new List<string>();
            for (var i = ResultsRepository.Current; i > (ResultsRepository.Current - max); i--)
            {
                var res = new LottoToJson().GetNumbers(SavePath, i);
                lottoString.Add(res);
                allNumbers.AddRange(res.Split(','));
                //var arr = res.Split(',').ToList();
            }
            var sorted = lottoString.OrderBy(q => q).ToList();
            //var duplicateKeys = lottoString.GroupBy(x => x)
            //    .Where(group => group.Count() > 1)
            //    .Select(group => group.Key);

            CreateChart(allNumbers, "chart_last_" + max);
        }

        [DataRow(7)]
        [DataRow(6)]
        [DataRow(5)]
        [DataRow(4)]
        [DataRow(3)]
        [DataRow(2)]
        [DataRow(1)]
        [DataRow(0)]
        [TestMethod]
        public void GetHistoricWeek(int max)
        {
            var current = ResultsRepository.Current;
            var allNumbers = new ArrayList();
            var lottoString = new List<string>();
            current -= max * 5;
            for (var i = current; i > (current - 5); i--)
            {
                var res = new LottoToJson().GetNumbers(SavePath, i);
                lottoString.Add(res);
                var arr = res.Split(',').ToList();
                allNumbers.AddRange(res.Split(','));
            }
            var sorted = lottoString.OrderBy(q => q).ToList();
            //var duplicateKeys = lottoString.GroupBy(x => x)
            //    .Where(group => group.Count() > 1)
            //    .Select(group => group.Key);

            CreateChart(allNumbers, "chart_period_" + max);
        }

        [TestMethod]
        public void CalculateRoundFromInitialDate()
        {
            var testDate = new DateTime(2019, 3, 30);
            var totalWeeks = Math.Floor(testDate.Subtract(ResultsRepository.InitialDate).TotalDays / 7);
            Assert.AreEqual(1194, totalWeeks);
        }

        [TestMethod]
        public void CheckRound_1145()
        {
            var res = new LottoToJson().GetNumbers(SavePath,1145);
            var arr = res.Split(',').ToList();
            Assert.AreEqual(8, arr.Count);
            Assert.AreEqual("2", arr[7]);
        }

        private static void CreateChart(ArrayList allNumbers, string name)
        {
            var numberOccurrence = allNumbers.ToArray().GroupBy(x => x)
                .ToDictionary(g => g.Key,
                    g => g.Count());

            var dt = new DataTable("Lotto");
            dt.Columns.Add("Number", typeof(int));
            dt.Columns.Add("Occurence", typeof(int));

            var chart = new ChartControl();
            chart.Padding.All = 0;
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.Width = 900;
            chart.Height = 600;
            var series = new Series("Series", ViewType.Bar) { ValueScaleType = ScaleType.Numerical, ArgumentScaleType = ScaleType.Numerical };
            for (var i = 1; i < 35; i++)
            {
                dt.Rows.Add(i, numberOccurrence.FirstOrDefault(a => Convert.ToInt32(a.Key) == i).Value);
            }
            series.DataSource = dt;
            series.ArgumentDataMember = dt.Columns[0].ColumnName;
            series.ValueDataMembers.AddRange(dt.Columns[1].ColumnName);
            chart.Series.Add(series);

            if (true)
            {
                ((SideBySideBarSeriesView)series.View).BarWidth = 1;
                var diagram = (XYDiagram)chart.Diagram;
                diagram.Margins.All = 0;
                // Define the whole range for the X-axis.
                diagram.AxisX.Tickmarks.Visible = false;
                diagram.AxisX.Tickmarks.MinorVisible = true;
                diagram.AxisX.GridLines.Visible = false;
                diagram.AxisX.GridLines.MinorVisible = true;
                diagram.AxisX.MinorCount = 1;
                diagram.AxisX.Label.MaxLineCount = 1;
                diagram.AxisX.WholeRange.Auto = false;
                diagram.AxisX.WholeRange.SetMinMaxValues(1, 34);
                diagram.AxisX.VisualRange.Auto = false;
                diagram.AxisX.VisualRange.SetMinMaxValues(1, 34);
                diagram.AxisX.VisualRange.AutoSideMargins = false;
                diagram.AxisX.WholeRange.SideMarginsValue = 0.5;
                diagram.AxisY.WholeRange.Auto = false;
            }

            chart.ExportToImage(SavePath + name + ".png", ImageFormat.Png);
        }
    }
}
