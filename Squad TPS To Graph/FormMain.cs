using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Squad_TPS_To_Graph
{
    public partial class FormMain : Form
    {
        private class ProgressData
        {
            public bool HasPoint { get; set; } = false;
            public DateTime Timestamp { get; set; }
            public float Value { get; set; }
        }

        private int FZoomLevel = 0;
        private const float CZoomScale = 4f;
        private const float CWarnColorThreshold = 10f; // If greater then this, orange
        private const float CGoodColorThreshold = 15f; // If greater then this, green

        public FormMain()
        {
            InitializeComponent();
            chartTPS.MouseWheel += ChartTPS_MouseWheel;
        }

        private void BtnOpenLog_Click(object aSender, EventArgs aArg)
        {
            ofdLogFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (ofdLogFile.ShowDialog() == DialogResult.OK) {
                // Reset zoom
                chartTPS.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                FZoomLevel = 0;
                // Clear old data
                chartTPS.Series[0].Points.Clear();
                edtTPSLog.Text = $"Timestamp,TPS{Environment.NewLine}";
                // Show overlay and start processing
                overlayPanel.Visible = true;
                bgwLogProcessing.RunWorkerAsync(ofdLogFile.FileName);
            }
        }

        private void ChartTPS_MouseEnter(object aSender, EventArgs aArg)
        {
            if (!chartTPS.Focused)
                chartTPS.Focus();
        }

        private void ChartTPS_MouseLeave(object aSender, EventArgs aArg)
        {
            if (chartTPS.Focused)
                chartTPS.Parent.Focus();
        }

        private void ChartTPS_MouseWheel(object aSender, MouseEventArgs aArg)
        {
            try {
                Axis xAxis = chartTPS.ChartAreas[0].AxisX;
                double xMin = xAxis.ScaleView.ViewMinimum;
                double xMax = xAxis.ScaleView.ViewMaximum;
                double xPixelPos = xAxis.PixelPositionToValue(aArg.Location.X);

                if (aArg.Delta < 0 && FZoomLevel > 0) {
                    // Scrolled down, meaning zoom out
                    if (--FZoomLevel <= 0) {
                        FZoomLevel = 0;
                        xAxis.ScaleView.ZoomReset();
                    } else {
                        double xStartPos = Math.Max(xPixelPos - (xPixelPos - xMin) * CZoomScale, 0);
                        double xEndPos = Math.Min(xStartPos + (xMax - xMin) * CZoomScale, xAxis.Maximum);
                        xAxis.ScaleView.Zoom(xStartPos, xEndPos);
                    }
                } else if (aArg.Delta > 0) {
                    // Scrolled up, meaning zoom in
                    double xStartPos = Math.Max(xPixelPos - (xPixelPos - xMin) / CZoomScale, 0);
                    double xEndPos = Math.Min(xStartPos + (xMax - xMin) / CZoomScale, xAxis.Maximum);
                    xAxis.ScaleView.Zoom(xStartPos, xEndPos);
                    FZoomLevel++;
                }
            } catch { }
        }

        private void BackgroundWorker1_DoWork(object aSender, DoWorkEventArgs aArg)
        {
            IEnumerable<string> logLines = File.ReadLines((string)aArg.Argument);
            int count = 0;
            int itemCount = logLines.Count();

            foreach (string line in logLines) {
                if (bgwLogProcessing.CancellationPending) // Always check if the background worker is being canceled
                    break;

                Match match = Regex.Match(line, @"\[(.*)\]\[.*Server\sTick\sRate:\s(\d+\.?\d*)", RegexOptions.IgnoreCase);
                ProgressData data = new ProgressData();

                if (match.Success) {
                    string timestampStrVal = match.Groups[1].Value;
                    string tpsStrVal = match.Groups[2].Value;

                    data.HasPoint = true;
                    data.Timestamp = DateTime.ParseExact(
                        timestampStrVal,
                        "yyyy.MM.dd-HH.mm.ss:fff",
                        System.Globalization.CultureInfo.InvariantCulture
                    );
                    data.Value = float.Parse(tpsStrVal, System.Globalization.CultureInfo.InvariantCulture);
                }

                int percentage = (++count * 100) / itemCount;
                bgwLogProcessing.ReportProgress(percentage, data);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object aSender, ProgressChangedEventArgs aArg)
        {
            if (!bgwLogProcessing.CancellationPending) { // Always check if the background worker is being canceled
                ProgressData data = (ProgressData)aArg.UserState;
                pbLogProgress.Value = aArg.ProgressPercentage;

                if (data.HasPoint) {
                    int itemIndex = chartTPS.Series[0].Points.AddXY(data.Timestamp.ToOADate(), data.Value);
                    edtTPSLog.AppendText(
                        data.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture) +
                        $",{data.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)}{Environment.NewLine}"
                    );

                    if (data.Value > CGoodColorThreshold) {
                        chartTPS.Series[0].Points[itemIndex].Color = System.Drawing.Color.Green;
                    } else if (data.Value > CWarnColorThreshold) {
                        chartTPS.Series[0].Points[itemIndex].Color = System.Drawing.Color.Orange;
                    } else {
                        chartTPS.Series[0].Points[itemIndex].Color = System.Drawing.Color.Red;
                    }
                }
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object aSender, RunWorkerCompletedEventArgs aArg)
        {
            if (!bgwLogProcessing.CancellationPending) // Always check if the background worker is being canceled
                overlayPanel.Visible = false;
        }

        private void Form1_FormClosing(object aSender, FormClosingEventArgs aArg)
        {
            if (bgwLogProcessing.IsBusy) // No need to cancel if the background worker is not working
                bgwLogProcessing.CancelAsync();
        }
    }
}
