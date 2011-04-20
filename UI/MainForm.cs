using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextSummarizationAlgos.DocumentProcessing;
using System.IO;
using System.Collections;
using TextSummarizationAlgos.Algorithms;
using TextSummarizationAlgos.Algorithms.LexRank;
using Utils;
using TextSummarizationAlgos.Algorithms.Centroid;
using TextSummarizationAlgos;
using TextSummarizationAlgos.Utils;

namespace UI
{
    public partial class MainForm : Form
    {
        private TextSummarizationAlgorithm algorithm = null;
        private DocumentProcessor docProcessor = new DocumentProcessor();
        private ArrayList trainingDocs = null;
        private DocsStatistics docsStat = null;
        private IDF idf = null;

        private enum TextSummarizationAlgorithmType
        {
            Lakhas = 1,
            CentroidBased = 2,
            DegreeCentrality = 3,
            LexRank = 4,
            ContinuousLexRank = 5
        }

        private void changeAlgorithm(TextSummarizationAlgorithmType algo)
        {
            TextSummarizationAlgorithm prevAlgorithm = this.algorithm;

            switch (algo)
            {
                case TextSummarizationAlgorithmType.Lakhas:
                    {
                        this.algorithm = new LakhasAlgorithm();
                        break;
                    }
                case TextSummarizationAlgorithmType.DegreeCentrality:
                    {
                        this.algorithm = new LexRankDegreeCentrality(0.1);
                        break;
                    }
                case TextSummarizationAlgorithmType.LexRank:
                    {
                        this.algorithm = new LexRankWithThreshold(0.2, 0.15);
                        break;
                    }
                case TextSummarizationAlgorithmType.ContinuousLexRank:
                    {
                        this.algorithm = new ContinuousLexRank(0.1, 0.15);
                        break;
                    }
                case TextSummarizationAlgorithmType.CentroidBased:
                    {
                        this.algorithm = new CentroidAlgorithm(Conf.CENTROID_CLUSTERS_PATH, 1, 1, 1, 3, 20);
                        break;
                    }
            }

            if (this.algorithm is SingleDocTextSummarizationAlgorithm)
            {
                this.SummarizationTypeSingle.Checked = true;
                this.SummarizationTypeMultiple.Checked = false;
            }
            else if (this.algorithm is MultipleDocTextSummarizationAlgorithm)
            {
                this.SummarizationTypeSingle.Checked = false;
                this.SummarizationTypeMultiple.Checked = true;
            }

            if ((prevAlgorithm is SingleDocTextSummarizationAlgorithm && this.algorithm is MultipleDocTextSummarizationAlgorithm) || (prevAlgorithm is MultipleDocTextSummarizationAlgorithm && this.algorithm is SingleDocTextSummarizationAlgorithm))
            {
                this.OriginalTextTxt.Text = "";
                this.filenames = new ArrayList();
            }


            if (this.SummarizationTypeSingle.Checked)
                this.TestFileDialog.Multiselect = false;
            else
                this.TestFileDialog.Multiselect = true;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Trace.setInstance(new TextBoxTrace(this.TraceTxt));
            //Trace.setInstance(new NullTrace());
            IDF.setInstance(IDF.fromFile(Conf.IDFFILE_PATH));
            this.AlgorithmCmbo.SelectedIndex = 0;
        }

        private void TrainingFilesDialog_FileOk(object sender, CancelEventArgs e)
        {
            string[] fileNames = this.TrainingFilesDialog.FileNames;
            ArrayList docs = new ArrayList();

            this.progressBar.Show();
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = fileNames.Length + (fileNames.Length / 4);
            this.progressBar.Value = 0;

            foreach (string fileName in fileNames)
            {
                string fileText = File.ReadAllText(fileName, Encoding.Default);
                Document doc = docProcessor.process(fileText);
                docs.Add(doc);
                this.progressBar.Increment(1);
            }

            this.trainingDocs = docs;
            this.docsStat = DocsStatistics.generateStatistics(docs);

            this.progressBar.Value = this.progressBar.Maximum;
            this.progressBar.Hide();

            this.AlgorithmCmbo.Enabled = true;
        }

        private void TrainingGrpBox_Enter(object sender, EventArgs e)
        {

        }

        private void TrainBtn_Click(object sender, EventArgs e)
        {
            this.TrainingFilesDialog.ShowDialog();
        }

        private void Algorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool invalidValue = true;
            //this.AlgorithmCmbo.Text

            if (this.AlgorithmCmbo.Text != null)
            {
                if (!this.AlgorithmCmbo.Text.ToString().Trim().Equals(""))
                {
                    changeAlgorithm((TextSummarizationAlgorithmType)Convert.ToInt32(this.AlgorithmCmbo.Text.ToString().ToCharArray()[0] - Convert.ToInt32('0')));
                    invalidValue = false;
                }
            }

            if (invalidValue)
                MessageBox.Show("Invalid Value Selected");
        }

        private void LoadTestFileBtn_Click(object sender, EventArgs e)
        {
            this.TestFileDialog.ShowDialog();
        }

        ArrayList filenames = null;

        private void TestFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (this.SummarizationTypeSingle.Checked)
            {
                string testFilename = this.TestFileDialog.FileName;
                string testFileText = File.ReadAllText(testFilename, Encoding.Default);

                this.OriginalTextTxt.Text = testFileText;
            }
            else if (this.SummarizationTypeMultiple.Checked)
            {
                /*
                string[] testfilenames = this.TestFileDialog.FileNames;

                string result = "";
                foreach (string filename in testfilenames)
                {
                    result += filename + Environment.NewLine;
                }

                this.OriginalTextTxt.Text = result;
                //*/

                string[] testfilenames = this.TestFileDialog.FileNames;

                string result = "";
                int i = 0;
                this.filenames = new ArrayList();

                foreach (string filename in testfilenames)
                {
                    filenames.Add(filename);
                    string fileContent = File.ReadAllText(filename, Encoding.Default);

                    result += ("##########################################################################" + Environment.NewLine);
                    result += ("Document #" + (++i) + Environment.NewLine + fileContent + Environment.NewLine);
                }

                this.OriginalTextTxt.Text = result;
            }
        }

        private void SummarizeBtn_Click(object sender, EventArgs e)
        {
            string generatedSummary = "";

            if (this.algorithm == null)
            {
                MessageBox.Show("Algorithm not selected");
                return;
            }

            double compressionRatio = (double)this.compressionRatioTrckBar.Value / 10;

            if (this.SummarizationTypeSingle.Checked)
            {
                Document doc = this.docProcessor.process(this.OriginalTextTxt.Text);

                generatedSummary = ((SingleDocTextSummarizationAlgorithm)this.algorithm).generateSummary(doc, compressionRatio);
            }
            else if (this.SummarizationTypeMultiple.Checked)
            {
                //string[] testfilenames = this.OriginalTextTxt.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                ArrayList docs = new ArrayList();

                //foreach (string filename in testfilenames)
                foreach (string filename in this.filenames)
                {
                    string content = File.ReadAllText(filename, Encoding.Default);
                    Document doc = this.docProcessor.process(content);

                    docs.Add(doc);
                }

                generatedSummary = ((MultipleDocTextSummarizationAlgorithm)this.algorithm).generateSummary(docs, compressionRatio);
            }

            this.summarizationTxt.Text = generatedSummary;
        }

        private void clearTraceBtn_Click(object sender, EventArgs e)
        {
            this.TraceTxt.Text = "";
        }

        private void generateIDFBtn_Click(object sender, EventArgs e)
        {
            this.IDFPreprocessDocsDialog.ShowDialog();
        }

        private void IDFPreprocessDocsDialog_FileOk(object sender, CancelEventArgs e)
        {
            string[] fileNames = this.IDFPreprocessDocsDialog.FileNames;

            /*
            ArrayList docs = new ArrayList();
            
            this.progressBar.Show();
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = fileNames.Length + (fileNames.Length / 4);
            this.progressBar.Value = 0;

            foreach (string fileName in fileNames)
            {
                string fileText = File.ReadAllText(fileName, Encoding.Default);
                Document doc = docProcessor.process(fileText);
                docs.Add(doc);
                this.progressBar.Increment(1);
            }

            this.idf = IDF.fromDocuments(docs);

            this.progressBar.Value = this.progressBar.Maximum;
            this.progressBar.Hide();
            //*/

            this.idf = IDF.IDFGenerator.fromFiles(fileNames);

            this.IDFSaveDialog.ShowDialog();
        }

        private void IDFSaveDialog_FileOk(object sender, CancelEventArgs e)
        {
            string filename = this.IDFSaveDialog.FileName;

            this.idf.toFile(filename);
        }

        private void SummarizationTypeMultiple_CheckedChanged(object sender, EventArgs e)
        {
            this.TestFileDialog.Multiselect = true;
        }

        private void SummarizationTypeSingle_CheckedChanged(object sender, EventArgs e)
        {
            this.TestFileDialog.Multiselect = false;
        }

        private void compressionRatioTrckBar_Scroll(object sender, EventArgs e)
        {
            double compressionRatio = (double)this.compressionRatioTrckBar.Value / 10;

            this.compressionValuelbl.Text = Convert.ToString(compressionRatio);
        }

        private void removeStopWordsChkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.removeStopWordsChkbox.Checked)
                StopWordsHandler.setInstance(new DefaultStopWordsHandler(Conf.STOP_WORDS_PATH));
            else
                StopWordsHandler.setInstance(new NullStopWordsHandler());
        }

        private void lemmatizeWordsChkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.lemmatizeWordsChkbox.Checked)
                Lemmatizer.setInstance(new DefaultLemmatizer(Conf.LEMMATIZATION_WORDS_PATH));
            else
                Lemmatizer.setInstance(new NullLemmatizer());
        }
    }
}
