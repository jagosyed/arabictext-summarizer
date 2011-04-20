namespace UI
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
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
            this.TrainingFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.TrainingGrp = new System.Windows.Forms.GroupBox();
            this.lemmatizeWordsChkbox = new System.Windows.Forms.CheckBox();
            this.removeStopWordsChkbox = new System.Windows.Forms.CheckBox();
            this.compressionRatioGrp = new System.Windows.Forms.GroupBox();
            this.compressionValuelbl = new System.Windows.Forms.Label();
            this.compressionRatioMaxLbl = new System.Windows.Forms.Label();
            this.compressionRatioMinLbl = new System.Windows.Forms.Label();
            this.compressionRatioTrckBar = new System.Windows.Forms.TrackBar();
            this.SummarizationTypeMultiple = new System.Windows.Forms.RadioButton();
            this.SummarizationTypeSingle = new System.Windows.Forms.RadioButton();
            this.clearTraceBtn = new System.Windows.Forms.Button();
            this.LoadTestFileBtn = new System.Windows.Forms.Button();
            this.algoGrp = new System.Windows.Forms.GroupBox();
            this.AlgorithmCmbo = new System.Windows.Forms.ComboBox();
            this.SummarizeBtn = new System.Windows.Forms.Button();
            this.generateIDFBtn = new System.Windows.Forms.Button();
            this.TrainBtn = new System.Windows.Forms.Button();
            this.SummarizationGrp = new System.Windows.Forms.GroupBox();
            this.summarizationTxt = new System.Windows.Forms.TextBox();
            this.TraceGrp = new System.Windows.Forms.GroupBox();
            this.TraceTxt = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.OriginalTextGrp = new System.Windows.Forms.GroupBox();
            this.OriginalTextTxt = new System.Windows.Forms.TextBox();
            this.TestFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.IDFPreprocessDocsDialog = new System.Windows.Forms.OpenFileDialog();
            this.PreprocessingGrp = new System.Windows.Forms.GroupBox();
            this.IDFSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.TrainingGrp.SuspendLayout();
            this.compressionRatioGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressionRatioTrckBar)).BeginInit();
            this.algoGrp.SuspendLayout();
            this.SummarizationGrp.SuspendLayout();
            this.TraceGrp.SuspendLayout();
            this.OriginalTextGrp.SuspendLayout();
            this.PreprocessingGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrainingFilesDialog
            // 
            this.TrainingFilesDialog.FileName = "TrainingFilesDialog";
            this.TrainingFilesDialog.Multiselect = true;
            this.TrainingFilesDialog.Title = "Training Files";
            this.TrainingFilesDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.TrainingFilesDialog_FileOk);
            // 
            // TrainingGrp
            // 
            this.TrainingGrp.Controls.Add(this.lemmatizeWordsChkbox);
            this.TrainingGrp.Controls.Add(this.removeStopWordsChkbox);
            this.TrainingGrp.Controls.Add(this.compressionRatioGrp);
            this.TrainingGrp.Controls.Add(this.SummarizationTypeMultiple);
            this.TrainingGrp.Controls.Add(this.SummarizationTypeSingle);
            this.TrainingGrp.Controls.Add(this.clearTraceBtn);
            this.TrainingGrp.Controls.Add(this.LoadTestFileBtn);
            this.TrainingGrp.Controls.Add(this.algoGrp);
            this.TrainingGrp.Controls.Add(this.SummarizeBtn);
            this.TrainingGrp.Location = new System.Drawing.Point(1322, 12);
            this.TrainingGrp.Name = "TrainingGrp";
            this.TrainingGrp.Size = new System.Drawing.Size(295, 437);
            this.TrainingGrp.TabIndex = 0;
            this.TrainingGrp.TabStop = false;
            this.TrainingGrp.Text = "Summarization";
            this.TrainingGrp.Enter += new System.EventHandler(this.TrainingGrpBox_Enter);
            // 
            // lemmatizeWordsChkbox
            // 
            this.lemmatizeWordsChkbox.AutoSize = true;
            this.lemmatizeWordsChkbox.Checked = true;
            this.lemmatizeWordsChkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lemmatizeWordsChkbox.Location = new System.Drawing.Point(12, 293);
            this.lemmatizeWordsChkbox.Name = "lemmatizeWordsChkbox";
            this.lemmatizeWordsChkbox.Size = new System.Drawing.Size(140, 21);
            this.lemmatizeWordsChkbox.TabIndex = 12;
            this.lemmatizeWordsChkbox.Text = "Lemmatize Words";
            this.lemmatizeWordsChkbox.UseVisualStyleBackColor = true;
            this.lemmatizeWordsChkbox.CheckedChanged += new System.EventHandler(this.lemmatizeWordsChkbox_CheckedChanged);
            // 
            // removeStopWordsChkbox
            // 
            this.removeStopWordsChkbox.AutoSize = true;
            this.removeStopWordsChkbox.Checked = true;
            this.removeStopWordsChkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removeStopWordsChkbox.Location = new System.Drawing.Point(12, 265);
            this.removeStopWordsChkbox.Name = "removeStopWordsChkbox";
            this.removeStopWordsChkbox.Size = new System.Drawing.Size(159, 21);
            this.removeStopWordsChkbox.TabIndex = 11;
            this.removeStopWordsChkbox.Text = "Remove Stop Words";
            this.removeStopWordsChkbox.UseVisualStyleBackColor = true;
            this.removeStopWordsChkbox.CheckedChanged += new System.EventHandler(this.removeStopWordsChkbox_CheckedChanged);
            // 
            // compressionRatioGrp
            // 
            this.compressionRatioGrp.Controls.Add(this.compressionValuelbl);
            this.compressionRatioGrp.Controls.Add(this.compressionRatioMaxLbl);
            this.compressionRatioGrp.Controls.Add(this.compressionRatioMinLbl);
            this.compressionRatioGrp.Controls.Add(this.compressionRatioTrckBar);
            this.compressionRatioGrp.Location = new System.Drawing.Point(12, 172);
            this.compressionRatioGrp.Name = "compressionRatioGrp";
            this.compressionRatioGrp.Size = new System.Drawing.Size(260, 86);
            this.compressionRatioGrp.TabIndex = 10;
            this.compressionRatioGrp.TabStop = false;
            this.compressionRatioGrp.Text = "Compression Ratio";
            // 
            // compressionValuelbl
            // 
            this.compressionValuelbl.AutoSize = true;
            this.compressionValuelbl.Location = new System.Drawing.Point(112, 62);
            this.compressionValuelbl.Name = "compressionValuelbl";
            this.compressionValuelbl.Size = new System.Drawing.Size(28, 17);
            this.compressionValuelbl.TabIndex = 12;
            this.compressionValuelbl.Text = "0.3";
            // 
            // compressionRatioMaxLbl
            // 
            this.compressionRatioMaxLbl.AutoSize = true;
            this.compressionRatioMaxLbl.Location = new System.Drawing.Point(219, 32);
            this.compressionRatioMaxLbl.Name = "compressionRatioMaxLbl";
            this.compressionRatioMaxLbl.Size = new System.Drawing.Size(28, 17);
            this.compressionRatioMaxLbl.TabIndex = 11;
            this.compressionRatioMaxLbl.Text = "0.9";
            // 
            // compressionRatioMinLbl
            // 
            this.compressionRatioMinLbl.AutoSize = true;
            this.compressionRatioMinLbl.Location = new System.Drawing.Point(15, 32);
            this.compressionRatioMinLbl.Name = "compressionRatioMinLbl";
            this.compressionRatioMinLbl.Size = new System.Drawing.Size(28, 17);
            this.compressionRatioMinLbl.TabIndex = 10;
            this.compressionRatioMinLbl.Text = "0.1";
            // 
            // compressionRatioTrckBar
            // 
            this.compressionRatioTrckBar.Location = new System.Drawing.Point(49, 23);
            this.compressionRatioTrckBar.Maximum = 9;
            this.compressionRatioTrckBar.Minimum = 1;
            this.compressionRatioTrckBar.Name = "compressionRatioTrckBar";
            this.compressionRatioTrckBar.Size = new System.Drawing.Size(164, 56);
            this.compressionRatioTrckBar.TabIndex = 9;
            this.compressionRatioTrckBar.Value = 3;
            this.compressionRatioTrckBar.Scroll += new System.EventHandler(this.compressionRatioTrckBar_Scroll);
            // 
            // SummarizationTypeMultiple
            // 
            this.SummarizationTypeMultiple.AutoSize = true;
            this.SummarizationTypeMultiple.Checked = true;
            this.SummarizationTypeMultiple.Enabled = false;
            this.SummarizationTypeMultiple.Location = new System.Drawing.Point(108, 145);
            this.SummarizationTypeMultiple.Name = "SummarizationTypeMultiple";
            this.SummarizationTypeMultiple.Size = new System.Drawing.Size(73, 21);
            this.SummarizationTypeMultiple.TabIndex = 8;
            this.SummarizationTypeMultiple.TabStop = true;
            this.SummarizationTypeMultiple.Text = "Multiple";
            this.SummarizationTypeMultiple.UseVisualStyleBackColor = true;
            this.SummarizationTypeMultiple.CheckedChanged += new System.EventHandler(this.SummarizationTypeMultiple_CheckedChanged);
            // 
            // SummarizationTypeSingle
            // 
            this.SummarizationTypeSingle.AutoSize = true;
            this.SummarizationTypeSingle.Enabled = false;
            this.SummarizationTypeSingle.Location = new System.Drawing.Point(18, 145);
            this.SummarizationTypeSingle.Name = "SummarizationTypeSingle";
            this.SummarizationTypeSingle.Size = new System.Drawing.Size(64, 21);
            this.SummarizationTypeSingle.TabIndex = 7;
            this.SummarizationTypeSingle.Text = "Single";
            this.SummarizationTypeSingle.UseVisualStyleBackColor = true;
            this.SummarizationTypeSingle.CheckedChanged += new System.EventHandler(this.SummarizationTypeSingle_CheckedChanged);
            // 
            // clearTraceBtn
            // 
            this.clearTraceBtn.Location = new System.Drawing.Point(9, 408);
            this.clearTraceBtn.Name = "clearTraceBtn";
            this.clearTraceBtn.Size = new System.Drawing.Size(182, 23);
            this.clearTraceBtn.TabIndex = 4;
            this.clearTraceBtn.Text = "Clear Trace";
            this.clearTraceBtn.UseVisualStyleBackColor = true;
            this.clearTraceBtn.Click += new System.EventHandler(this.clearTraceBtn_Click);
            // 
            // LoadTestFileBtn
            // 
            this.LoadTestFileBtn.Location = new System.Drawing.Point(18, 87);
            this.LoadTestFileBtn.Name = "LoadTestFileBtn";
            this.LoadTestFileBtn.Size = new System.Drawing.Size(182, 23);
            this.LoadTestFileBtn.TabIndex = 3;
            this.LoadTestFileBtn.Text = "Load File(s)";
            this.LoadTestFileBtn.UseVisualStyleBackColor = true;
            this.LoadTestFileBtn.Click += new System.EventHandler(this.LoadTestFileBtn_Click);
            // 
            // algoGrp
            // 
            this.algoGrp.Controls.Add(this.AlgorithmCmbo);
            this.algoGrp.Location = new System.Drawing.Point(12, 20);
            this.algoGrp.Name = "algoGrp";
            this.algoGrp.Size = new System.Drawing.Size(188, 61);
            this.algoGrp.TabIndex = 2;
            this.algoGrp.TabStop = false;
            this.algoGrp.Text = "Algorithm";
            // 
            // AlgorithmCmbo
            // 
            this.AlgorithmCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlgorithmCmbo.FormattingEnabled = true;
            this.AlgorithmCmbo.Items.AddRange(new object[] {
            "1- Lakhas",
            "2- Centroid Based",
            "3- Degree Centrality",
            "4- LexRank",
            "5- Continuous LexRank"});
            this.AlgorithmCmbo.Location = new System.Drawing.Point(6, 23);
            this.AlgorithmCmbo.Name = "AlgorithmCmbo";
            this.AlgorithmCmbo.Size = new System.Drawing.Size(170, 24);
            this.AlgorithmCmbo.TabIndex = 0;
            this.AlgorithmCmbo.SelectedIndexChanged += new System.EventHandler(this.Algorithm_SelectedIndexChanged);
            // 
            // SummarizeBtn
            // 
            this.SummarizeBtn.Location = new System.Drawing.Point(18, 116);
            this.SummarizeBtn.Name = "SummarizeBtn";
            this.SummarizeBtn.Size = new System.Drawing.Size(182, 23);
            this.SummarizeBtn.TabIndex = 1;
            this.SummarizeBtn.Text = "Summarize";
            this.SummarizeBtn.UseVisualStyleBackColor = true;
            this.SummarizeBtn.Click += new System.EventHandler(this.SummarizeBtn_Click);
            // 
            // generateIDFBtn
            // 
            this.generateIDFBtn.Location = new System.Drawing.Point(12, 23);
            this.generateIDFBtn.Name = "generateIDFBtn";
            this.generateIDFBtn.Size = new System.Drawing.Size(182, 23);
            this.generateIDFBtn.TabIndex = 5;
            this.generateIDFBtn.Text = "Generate IDF File";
            this.generateIDFBtn.UseVisualStyleBackColor = true;
            this.generateIDFBtn.Click += new System.EventHandler(this.generateIDFBtn_Click);
            // 
            // TrainBtn
            // 
            this.TrainBtn.Enabled = false;
            this.TrainBtn.Location = new System.Drawing.Point(12, 52);
            this.TrainBtn.Name = "TrainBtn";
            this.TrainBtn.Size = new System.Drawing.Size(182, 23);
            this.TrainBtn.TabIndex = 0;
            this.TrainBtn.Text = "Load Training Files";
            this.TrainBtn.UseVisualStyleBackColor = true;
            this.TrainBtn.Visible = false;
            this.TrainBtn.Click += new System.EventHandler(this.TrainBtn_Click);
            // 
            // SummarizationGrp
            // 
            this.SummarizationGrp.Controls.Add(this.summarizationTxt);
            this.SummarizationGrp.Location = new System.Drawing.Point(12, 276);
            this.SummarizationGrp.Name = "SummarizationGrp";
            this.SummarizationGrp.Size = new System.Drawing.Size(1304, 273);
            this.SummarizationGrp.TabIndex = 1;
            this.SummarizationGrp.TabStop = false;
            this.SummarizationGrp.Text = "Summarization";
            // 
            // summarizationTxt
            // 
            this.summarizationTxt.Location = new System.Drawing.Point(17, 23);
            this.summarizationTxt.Multiline = true;
            this.summarizationTxt.Name = "summarizationTxt";
            this.summarizationTxt.ReadOnly = true;
            this.summarizationTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.summarizationTxt.Size = new System.Drawing.Size(1281, 230);
            this.summarizationTxt.TabIndex = 0;
            this.summarizationTxt.WordWrap = false;
            // 
            // TraceGrp
            // 
            this.TraceGrp.Controls.Add(this.TraceTxt);
            this.TraceGrp.Location = new System.Drawing.Point(12, 555);
            this.TraceGrp.Name = "TraceGrp";
            this.TraceGrp.Size = new System.Drawing.Size(1605, 227);
            this.TraceGrp.TabIndex = 2;
            this.TraceGrp.TabStop = false;
            this.TraceGrp.Text = "Trace";
            // 
            // TraceTxt
            // 
            this.TraceTxt.Location = new System.Drawing.Point(17, 23);
            this.TraceTxt.Multiline = true;
            this.TraceTxt.Name = "TraceTxt";
            this.TraceTxt.ReadOnly = true;
            this.TraceTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TraceTxt.Size = new System.Drawing.Size(1582, 198);
            this.TraceTxt.TabIndex = 0;
            this.TraceTxt.WordWrap = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(9, 788);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1608, 23);
            this.progressBar.TabIndex = 3;
            this.progressBar.UseWaitCursor = true;
            this.progressBar.Visible = false;
            // 
            // OriginalTextGrp
            // 
            this.OriginalTextGrp.Controls.Add(this.OriginalTextTxt);
            this.OriginalTextGrp.Location = new System.Drawing.Point(12, 12);
            this.OriginalTextGrp.Name = "OriginalTextGrp";
            this.OriginalTextGrp.Size = new System.Drawing.Size(1304, 258);
            this.OriginalTextGrp.TabIndex = 4;
            this.OriginalTextGrp.TabStop = false;
            this.OriginalTextGrp.Text = "Original Text";
            // 
            // OriginalTextTxt
            // 
            this.OriginalTextTxt.Location = new System.Drawing.Point(17, 23);
            this.OriginalTextTxt.Multiline = true;
            this.OriginalTextTxt.Name = "OriginalTextTxt";
            this.OriginalTextTxt.ReadOnly = true;
            this.OriginalTextTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OriginalTextTxt.Size = new System.Drawing.Size(1281, 229);
            this.OriginalTextTxt.TabIndex = 0;
            this.OriginalTextTxt.WordWrap = false;
            // 
            // TestFileDialog
            // 
            this.TestFileDialog.Multiselect = true;
            this.TestFileDialog.Title = "Load Test File";
            this.TestFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.TestFileDialog_FileOk);
            // 
            // IDFPreprocessDocsDialog
            // 
            this.IDFPreprocessDocsDialog.Multiselect = true;
            this.IDFPreprocessDocsDialog.Title = "Select ";
            this.IDFPreprocessDocsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.IDFPreprocessDocsDialog_FileOk);
            // 
            // PreprocessingGrp
            // 
            this.PreprocessingGrp.Controls.Add(this.TrainBtn);
            this.PreprocessingGrp.Controls.Add(this.generateIDFBtn);
            this.PreprocessingGrp.Location = new System.Drawing.Point(1322, 455);
            this.PreprocessingGrp.Name = "PreprocessingGrp";
            this.PreprocessingGrp.Size = new System.Drawing.Size(197, 94);
            this.PreprocessingGrp.TabIndex = 5;
            this.PreprocessingGrp.TabStop = false;
            this.PreprocessingGrp.Text = "Preprocessing";
            this.PreprocessingGrp.Visible = false;
            // 
            // IDFSaveDialog
            // 
            this.IDFSaveDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.IDFSaveDialog_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1533, 745);
            this.Controls.Add(this.PreprocessingGrp);
            this.Controls.Add(this.OriginalTextGrp);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.TraceGrp);
            this.Controls.Add(this.SummarizationGrp);
            this.Controls.Add(this.TrainingGrp);
            this.Name = "MainForm";
            this.Text = "Text Summarization UI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TrainingGrp.ResumeLayout(false);
            this.TrainingGrp.PerformLayout();
            this.compressionRatioGrp.ResumeLayout(false);
            this.compressionRatioGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressionRatioTrckBar)).EndInit();
            this.algoGrp.ResumeLayout(false);
            this.SummarizationGrp.ResumeLayout(false);
            this.SummarizationGrp.PerformLayout();
            this.TraceGrp.ResumeLayout(false);
            this.TraceGrp.PerformLayout();
            this.OriginalTextGrp.ResumeLayout(false);
            this.OriginalTextGrp.PerformLayout();
            this.PreprocessingGrp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog TrainingFilesDialog;
        private System.Windows.Forms.GroupBox TrainingGrp;
        private System.Windows.Forms.Button TrainBtn;
        private System.Windows.Forms.GroupBox SummarizationGrp;
        private System.Windows.Forms.GroupBox TraceGrp;
        private System.Windows.Forms.TextBox summarizationTxt;
        private System.Windows.Forms.TextBox TraceTxt;
        private System.Windows.Forms.Button SummarizeBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox algoGrp;
        private System.Windows.Forms.ComboBox AlgorithmCmbo;
        private System.Windows.Forms.GroupBox OriginalTextGrp;
        private System.Windows.Forms.TextBox OriginalTextTxt;
        private System.Windows.Forms.Button LoadTestFileBtn;
        private System.Windows.Forms.OpenFileDialog TestFileDialog;
        private System.Windows.Forms.Button clearTraceBtn;
        private System.Windows.Forms.Button generateIDFBtn;
        private System.Windows.Forms.OpenFileDialog IDFPreprocessDocsDialog;
        private System.Windows.Forms.GroupBox PreprocessingGrp;
        private System.Windows.Forms.SaveFileDialog IDFSaveDialog;
        private System.Windows.Forms.RadioButton SummarizationTypeMultiple;
        private System.Windows.Forms.RadioButton SummarizationTypeSingle;
        private System.Windows.Forms.TrackBar compressionRatioTrckBar;
        private System.Windows.Forms.GroupBox compressionRatioGrp;
        private System.Windows.Forms.CheckBox removeStopWordsChkbox;
        private System.Windows.Forms.CheckBox lemmatizeWordsChkbox;
        private System.Windows.Forms.Label compressionRatioMaxLbl;
        private System.Windows.Forms.Label compressionRatioMinLbl;
        private System.Windows.Forms.Label compressionValuelbl;
    }
}

