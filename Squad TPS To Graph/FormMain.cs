using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Squad_TPS_To_Graph
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Used to share data between the worker thread and the ProgressChanged event
        /// </summary>
        private class ProgressData
        {
            /// <summary>
            /// The log entry timestamp
            /// </summary>
            public DateTime Timestamp { get; set; }
            /// <summary>
            /// The TPS value
            /// </summary>
            public float Value { get; set; }
        }

        // Fields
        private int FZoomLevel = 0;
        private Regex FRegex;
        // Constants
        private const float CZoomScale = 4f;
        private const float CWarnColorThreshold = 10f; // If greater then this, orange
        private const float CGoodColorThreshold = 15f; // If greater then this, green
        private const int CLineCounterBufferSize = 1024 * 1024; // 1 MB
        private const char CCharLF = '\n';
        private const char CCharNULL = (char)0;

        /// <summary>
        /// Form constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            chartTPS.MouseWheel += ChartTPS_MouseWheel;
            FRegex = new Regex(
                @"\[(.*)\]\[.*Server\sTick\sRate:\s(\d+\.?\d*)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled
            );
        }

        /// <summary>
        /// Called when the "Open Log" 
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
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

        /// <summary>
        /// Called when the mouse cursor enters the chart's component area
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
        private void ChartTPS_MouseEnter(object aSender, EventArgs aArg)
        {
            if (!chartTPS.Focused)
                chartTPS.Focus();
        }

        /// <summary>
        /// Called when the mouse cursor leaves the chart's component area
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
        private void ChartTPS_MouseLeave(object aSender, EventArgs aArg)
        {
            if (chartTPS.Focused)
                chartTPS.Parent.Focus();
        }

        /// <summary>
        /// Called when the mouse is scrolled and the chart is the focused component
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
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

        /// <summary>
        /// Called when the background worker's RunWorkerAsync method is called
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
        private void BackgroundWorker1_DoWork(object aSender, DoWorkEventArgs aArg)
        {
            long count = 0;
            Match match;
            string line;
            ProgressData progress;

            using (FileStream fs = new FileStream((string)aArg.Argument, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs)) {
                long lineCount = CountLines(bs);
                bs.Seek(0, SeekOrigin.Begin);

                while ((line = sr.ReadLine()) != null) {
                    if (bgwLogProcessing.CancellationPending) // Always check if the background worker is being canceled
                        break;

                    match = FRegex.Match(line);

                    if (match.Success) {
                        progress =  new ProgressData {
                            Timestamp = DateTime.ParseExact(
                                match.Groups[1].Value,
                                "yyyy.MM.dd-HH.mm.ss:fff",
                                System.Globalization.CultureInfo.InvariantCulture
                            ),
                            Value = float.Parse(match.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture)
                        };
                    } else {
                        progress = null;
                    }

                    bgwLogProcessing.ReportProgress((int)((count++ * 100) / lineCount), progress);
                }
            }
        }

        /// <summary>
        /// Called when the background worker's ReportProgress method is called (thread-safe with the main thread)
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
        private void BackgroundWorker1_ProgressChanged(object aSender, ProgressChangedEventArgs aArg)
        {
            if (!bgwLogProcessing.CancellationPending) { // Always check if the background worker is being canceled
                if (pbLogProgress.Value != aArg.ProgressPercentage)
                    pbLogProgress.Value = aArg.ProgressPercentage;

                if (aArg.UserState != null) {
                    ProgressData data = (ProgressData)aArg.UserState;
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

        /// <summary>
        /// Called right after the background worker completed it's asynch task
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
        private void BackgroundWorker1_RunWorkerCompleted(object aSender, RunWorkerCompletedEventArgs aArg)
        {
            if (!bgwLogProcessing.CancellationPending) // Always check if the background worker is being canceled
                overlayPanel.Visible = false;

            pbLogProgress.Value = 0;
        }

        /// <summary>
        /// Called right before the form closes
        /// </summary>
        /// <param name="aSender"></param>
        /// <param name="aArg"></param>
        private void Form1_FormClosing(object aSender, FormClosingEventArgs aArg)
        {
            if (bgwLogProcessing.IsBusy) // No need to cancel if the background worker is not working
                bgwLogProcessing.CancelAsync();
        }

        /// <summary>
        /// Returns the number of lines in the given <paramref name="stream"/>.
        /// We only check LineFeed (#10) to also count the RCON/Query response lines.
        /// </summary>
        /// <param name="stream">The stream to count</param>
        /// <returns></returns>
        public long CountLines(Stream stream)
        {
            long result = 0L;
            byte[] byteBuffer = new byte[CLineCounterBufferSize];
            char currentChar = CCharNULL;
            int bytesRead;

            while ((bytesRead = stream.Read(byteBuffer, 0, CLineCounterBufferSize)) > 0) {
                for (int i = 0; i < bytesRead; i++) {
                    if ((char)byteBuffer[i] == CCharLF)
                        result++;
                }
            }

            if (currentChar != CCharLF && currentChar != CCharNULL)
                result++;

            return result > 0L ? ++result : result;
        }
    }
}
