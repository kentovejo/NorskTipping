using DevExpress.XtraCharts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using System.Net;

namespace NorskTipping
{
    [TestClass]
    public class PreviousResults
    {
        public static int Current = 1187;
        [TestMethod, Ignore]
        public void GetHistoricSave()
        {
            using (var webClient = new WebClient())
            {
                //for (int i = Current; i > 0; i--)
                {
                    var i = Current;
                    var json = webClient.DownloadString("https://www.norsk-tipping.no/api-lotto/getResultInfo.json?drawID=" + i);
                    File.WriteAllText(@"c:\NorskTipping\" + i + ".txt", json);
                }
            }
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
                max = Current;
            var allNumbers = new ArrayList();
            var lottoString = new List<string>();
            for (var i = Current; i > (Current - max); i--)
            {
                var res = new LottoToJson(true).GetNumbers(i);
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
            var allNumbers = new ArrayList();
            var lottoString = new List<string>();
            Current -= max * 5;
            for (int i = Current; i > (Current - 5); i--)
            {
                var lotto = File.ReadAllText(@"c:\NorskTipping\" + i + ".txt");
                var startPos = lotto.LastIndexOf("mainTable") + 12;
                var length = lotto.IndexOf("addTable") - startPos - 3;
                var res = lotto.Substring(startPos, length);
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

            chart.ExportToImage(@"c:\NorskTipping\" + name + ".png", ImageFormat.Png);
        }
    }
}
