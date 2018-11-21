namespace Squad_TPS_To_Graph
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.ofdLogFile = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.chartTPS = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.edtTPSLog = new System.Windows.Forms.TextBox();
            this.bgwLogProcessing = new System.ComponentModel.BackgroundWorker();
            this.overlayPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pbLogProgress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTPS)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.overlayPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenLog.Location = new System.Drawing.Point(1252, 673);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(85, 27);
            this.btnOpenLog.TabIndex = 1;
            this.btnOpenLog.Text = "Open Log";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.BtnOpenLog_Click);
            // 
            // ofdLogFile
            // 
            this.ofdLogFile.Filter = "Squad Logs|*.log";
            this.ofdLogFile.ShowReadOnly = true;
            this.ofdLogFile.Title = "Open a Squad server log";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1325, 655);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.chartTPS);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1317, 629);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graph";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1169, 610);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Scroll to zoom the chart";
            // 
            // chartTPS
            // 
            chartArea1.AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            chartArea1.AxisY.Interval = 5D;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IntervalOffset = 5D;
            chartArea1.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chartTPS.ChartAreas.Add(chartArea1);
            this.chartTPS.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartTPS.Legends.Add(legend1);
            this.chartTPS.Location = new System.Drawing.Point(3, 3);
            this.chartTPS.Name = "chartTPS";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Green;
            series1.Legend = "Legend1";
            series1.MarkerSize = 8;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series1.Name = "TPS";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartTPS.Series.Add(series1);
            this.chartTPS.Size = new System.Drawing.Size(1311, 623);
            this.chartTPS.TabIndex = 0;
            this.chartTPS.Text = "chart1";
            this.chartTPS.MouseEnter += new System.EventHandler(this.ChartTPS_MouseEnter);
            this.chartTPS.MouseLeave += new System.EventHandler(this.ChartTPS_MouseLeave);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.edtTPSLog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1317, 629);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log As CSV";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // edtTPSLog
            // 
            this.edtTPSLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtTPSLog.Location = new System.Drawing.Point(3, 3);
            this.edtTPSLog.Multiline = true;
            this.edtTPSLog.Name = "edtTPSLog";
            this.edtTPSLog.ReadOnly = true;
            this.edtTPSLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.edtTPSLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edtTPSLog.Size = new System.Drawing.Size(1311, 623);
            this.edtTPSLog.TabIndex = 0;
            // 
            // bgwLogProcessing
            // 
            this.bgwLogProcessing.WorkerReportsProgress = true;
            this.bgwLogProcessing.WorkerSupportsCancellation = true;
            this.bgwLogProcessing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.bgwLogProcessing.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.bgwLogProcessing.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // overlayPanel
            // 
            this.overlayPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.overlayPanel.ColumnCount = 3;
            this.overlayPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.overlayPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.overlayPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.overlayPanel.Controls.Add(this.pbLogProgress, 1, 2);
            this.overlayPanel.Controls.Add(this.label1, 1, 1);
            this.overlayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overlayPanel.Location = new System.Drawing.Point(0, 0);
            this.overlayPanel.Margin = new System.Windows.Forms.Padding(150);
            this.overlayPanel.Name = "overlayPanel";
            this.overlayPanel.RowCount = 4;
            this.overlayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.overlayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.overlayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.overlayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.overlayPanel.Size = new System.Drawing.Size(1349, 712);
            this.overlayPanel.TabIndex = 5;
            this.overlayPanel.Visible = false;
            // 
            // pbLogProgress
            // 
            this.pbLogProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogProgress.Location = new System.Drawing.Point(502, 364);
            this.pbLogProgress.Name = "pbLogProgress";
            this.pbLogProgress.Size = new System.Drawing.Size(344, 24);
            this.pbLogProgress.Step = 1;
            this.pbLogProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbLogProgress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(502, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "Processing log file";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 712);
            this.Controls.Add(this.overlayPanel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOpenLog);
            this.Name = "FormMain";
            this.Text = "Squad TPS To Graph";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTPS)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.overlayPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.OpenFileDialog ofdLogFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox edtTPSLog;
        private System.ComponentModel.BackgroundWorker bgwLogProcessing;
        private System.Windows.Forms.TableLayoutPanel overlayPanel;
        private System.Windows.Forms.ProgressBar pbLogProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTPS;
        private System.Windows.Forms.Label label2;
    }
}

