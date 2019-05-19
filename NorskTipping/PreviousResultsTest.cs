﻿using DevExpress.XtraCharts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Drawing.Imaging;

namespace NorskTipping
{
    [TestClass]
    public class PreviousResultsTest
    {
        private const string SavePath = @"c:\NorskTipping\";
        private readonly Games _games = new Games();
        [TestMethod]
        public void GetHistoricSave_Lotto()
        {
            _games.FetchResultsToDisk(SavePath, 0);
        }       
        
        [TestMethod]
        public void GetHistoricSave_Vikinglotto()
        {
            _games.FetchResultsToDisk(SavePath, 1);
        } 
        
        [TestMethod]
        public void GetHistoricSave_EuroJackpot()
        {
            _games.FetchResultsToDisk(SavePath, 2);
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
            var testGame = (ToJsonBase) _games.GameTypes[0];
            if (max == 0)
                max = testGame.CurrentRound;
            var allNumbers = new ArrayList();
            var lottoString = new List<string>();
            for (var i = testGame.CurrentRound; i > (testGame.CurrentRound - max); i--)
            {
                var res = ToJsonBase.GetNumbers(SavePath + testGame.Init.Name, i);
                lottoString.Add(res[0] + "," + res[1]);
                allNumbers.AddRange((res[0] + "," + res[1]).Split(','));
                //var arr = res.Split(',').ToList();
            }
            var sorted = lottoString.OrderBy(q => q).ToList();
            //var duplicateKeys = lottoString.GroupBy(x => x)
            //    .Where(group => group.Count() > 1)
            //    .Select(group => group.Key);

            CreateChart(allNumbers, "chart_last_" + max);
        }

        [DataRow(30)]
        [TestMethod]
        public void GetRoundsDifferance(int rounds)
        {
            var testGame = (ToJsonBase) _games.GameTypes[0];
            var start = testGame.CurrentRound - rounds;
            var labels = Enumerable.Range(start, testGame.CurrentRound - start + 1).Reverse().ToList();
            var ltj = new Lotto();
            ltj.GetResultsModel(SavePath, labels, true);
            var roundsDifferance = new List<int[]>();
            var ballsPrevious = new int[8];
            for(var i = 0; i < rounds; i++)
            {
                var ballsDifferance = new int[8];
                if(i > 0) {
                    for(var j = 0; j < 8; j++)
                        ballsDifferance[j] = Math.Abs(ballsPrevious[j] - (int)ltj.Model[j].Data[i]);
                    roundsDifferance.Add(ballsDifferance.OrderBy(q => q).ToArray());
                }
                
                for(var j = 0; j < 8; j++)
                    ballsPrevious[j] = (int)ltj.Model[j].Data[i];
            }
            System.IO.File.WriteAllLines(SavePath + "DifferanceUnsigned.txt", roundsDifferance.Select(a => string.Join(" ", a)));
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
            var testGame = (ToJsonBase) _games.GameTypes[0];
            var current = testGame.CurrentRound;
            var allNumbers = new ArrayList();
            var lottoString = new List<string>();
            current -= max * 5;
            for (var i = current; i > (current - 5); i--)
            {
                var res = ToJsonBase.GetNumbers(SavePath + testGame.Init.Name, i);
                lottoString.Add((res[0] + "," + res[1]));
                var arr = (res[0] + "," + res[1]).Split(',').ToList();
                allNumbers.AddRange((res[0] + "," + res[1]).Split(','));
            }
            var sorted = lottoString.OrderBy(q => q).ToList();
            //var duplicateKeys = lottoString.GroupBy(x => x)
            //    .Where(group => group.Count() > 1)
            //    .Select(group => group.Key);

            CreateChart(allNumbers, "chart_period_" + max);
        }

        [TestMethod]
        public void CalculateRoundFromInitialDate_Lotto()
        {
            var testGame = (ToJsonBase) _games.GameTypes[0];
            var testDate = new DateTime(2019, 3, 30);
            var totalWeeks = Math.Floor(testDate.Subtract(testGame.Init.InitialDate).TotalDays / 7);
            Assert.AreEqual(1194, totalWeeks);
        }

        [TestMethod]
        public void CalculateRoundFromInitialDate_VikingLotto()
        {
            var testGame = (ToJsonBase) _games.GameTypes[1];
            var testDate = new DateTime(2019, 5, 1);
            var totalWeeks = Math.Floor(testDate.Subtract(testGame.Init.InitialDate).TotalDays / 7);
            Assert.AreEqual(1200, totalWeeks);
        }

        [TestMethod]
        public void CalculateRoundFromInitialDate_Eurojackpot()
        {
            var testGame = (ToJsonBase) _games.GameTypes[2];
            var testDate = new DateTime(2019, 5, 3);
            var totalWeeks = Math.Floor(testDate.Subtract(testGame.Init.InitialDate).TotalDays / 7);
            Assert.AreEqual(327, totalWeeks);
        }

        [TestMethod]
        public void CheckRound_1145()
        {
            var testGame = (ToJsonBase) _games.GameTypes[0];
            var res = ToJsonBase.GetNumbers(SavePath + testGame.Init.Name,1145);
            var arr = (res[0] + "," + res[1]).Split(',').ToList();
            Assert.AreEqual(8, arr.Count);
            Assert.AreEqual("2", arr[7]);
        }

        [TestMethod]
        public void CheckRoundYear_2018()
        {
            var testGame = (ToJsonBase) _games.GameTypes[0];
            var start = (int) Math.Floor((new DateTime(2018, 1, 1).Subtract(testGame.Init.InitialDate).TotalDays - 1) / 7) + 2;
            var end = (int) Math.Floor((new DateTime(2018, 12, 31).Subtract(testGame.Init.InitialDate).TotalDays - 1) / 7) + 2;           
            for (var i = start; i < end; i++)
            {
                var res = new Lotto().GetNumbersWithDate(SavePath + testGame.Init.Name, i);
                Trace.WriteLine(res.Numbers[0] + "," + res.Numbers[1] + "," + res.DrawDate);
            }
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
