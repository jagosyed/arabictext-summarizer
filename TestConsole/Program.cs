using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.Algorithms.LexRank;
using TextSummarizationAlgos.DocumentProcessing;
using System.IO;
using TextSummarizationAlgos;
using TextSummarizationAlgos.Utils;
using System.Collections;
using Utils;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IDF idf = IDF.IDFGenerator.fromFiles(Directory.GetFiles(@"D:\Files\College\Advanced AI\Data Sets\CNNArabic2\Dest2\", "*.txt", SearchOption.TopDirectoryOnly));

            idf.toFile(@"IDF.txt");
            /*
            Lemmatizer lemm = Lemmatizer.getInstance(Conf.LEMMATIZATION_WORDS_PATH);

            //new DocumentProcessor ().process ( File.ReadAllText ( "" , Encoding.Default ) ) ;
            //new LexRankDegreeCentrality( 0.1 ).generateSummary ( ;

            // Training
            string searchPath = Conf.TRAINING_PATH;

            string[] files = Directory.GetFiles(searchPath, "*", SearchOption.AllDirectories);
            //string[] files = Directory.GetFiles(searchPath, "*", SearchOption.AllDirectories);

            ArrayList docs = new ArrayList();

            foreach (string file in files)
            {
                Console.WriteLine("Processing file : " + file);

                string currContent = File.ReadAllText(file, Encoding.Default);

                //Document doc = Document.process(currContent);
                Document doc = Conf.getDocumentProcessor().process(currContent);

                docs.Add(doc);

                //break ;
            }

            DocsStatistics stats = DocsStatistics.generateStatistics(docs);

            foreach (DictionaryEntry entry in stats.wordsCount)
            {
                Trace.write(entry.Key + " : " + entry.Value + " Times in " + ((HashSet<Document>)stats.wordRefs[entry.Key]).Count + " Documents.");
            }

            // Testing
            searchPath = Conf.TESTING_PATH;

            files = Directory.GetFiles(searchPath, "*_AR.txt", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                /*
                //string file = @"D:\Files\College\Advanced AI\Data Sets\DataSet_Economics_4\Testing\06042008_7\1897283_1897234_AR.txt";
                string testFiletext = File.ReadAllText(file, Encoding.Default);

                //string genSummary = DocsStatistics.generateSummary(stats, testFiletext);
                //string genSummary = new LakhasAlgorithm().generateSummary(stats, testFiletext);
                //string genSummary = new LexRankDegreeCentrality(0.1).generateSummary(stats, testFiletext);
                string genSummary = new LexRankWithThreshold(0.1, 0.15).generateSummary(stats, new DocumentProcessor().process(testFiletext));

                string currDirectory = Directory.GetParent(file).FullName;
                //string filename = file.Remove(0, currDirectory.Length + 1);
                string filename = file.Remove(0, Conf.TESTING_PATH.Length + 1);

                File.WriteAllText(currDirectory + "\\" + filename + "_SUMMARY.txt", genSummary, Encoding.Default);
                //* /
            }
        //*/
        }
    }
}
