using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TextSummarizationAlgos.DocumentProcessing;
using System.Collections;
using TextSummarizationAlgos.Utils;

namespace TextSummarizationAlgos.Algorithms.Centroid
{
    public class CentroidCluster
    {
        private string folderPath = null;
        private double idfThreshold = 0;
        private int keepWords = 0;

        public Hashtable centroidWords = null;

        public static CentroidCluster[] fromFolder(string folderPath, double idfThreshold, int keepWords)
        {
            string[] clusterDirs = Directory.GetDirectories(folderPath, "*", SearchOption.TopDirectoryOnly);
            ArrayList clusters = new ArrayList();

            foreach (string clusterDir in clusterDirs)
            {
                CentroidCluster cluster = new CentroidCluster(clusterDir, idfThreshold, keepWords);

                clusters.Add(cluster);
            }
            return ((CentroidCluster[])clusters.ToArray(typeof(CentroidCluster)));
        }

        private CentroidCluster(string folderPath, double idfThreshold, int keepWords)
        {
            this.folderPath = folderPath;
            this.idfThreshold = idfThreshold;
            this.keepWords = keepWords;

            this.load(folderPath);
        }

        private void load(string clusterDir)
        {
            DocumentProcessor docProcessor = new DocumentProcessor();
            ArrayList docs = new ArrayList();

            string[] clusterFiles = Directory.GetFiles(clusterDir, "*.txt", SearchOption.TopDirectoryOnly);

            foreach (string filename in clusterFiles)
            {
                string fileText = File.ReadAllText(filename, Encoding.Default);

                Document doc = docProcessor.process(fileText);

                docs.Add(doc);
            }

            DocsStatistics docStats = DocsStatistics.generateStatistics(docs);
            Hashtable centroid = new Hashtable();

            foreach (string word in docStats.wordsCount.Keys)
            {
                //centroid[firstWord] = (((int)docStats.wordsCount[firstWord]) * idf(docStats, firstWord)) / docs.Count;
                centroid[word] = (((int)docStats.wordsCount[word]) * IDF.getInstance().get(word) ) / docs.Count;
            }

            this.centroidWords = applyKeepWords(centroid, this.keepWords);
        }

        private static Hashtable applyKeepWords(Hashtable centroidValues, int keepWords)
        {
            DictionaryEntry[] centValuesArr = new DictionaryEntry[centroidValues.Count];

            centroidValues.CopyTo(centValuesArr, 0);

            Array.Sort(centValuesArr, new DictionaryEntryValueComparer());
            Array.Reverse(centValuesArr);

            Hashtable finalCentroidValues = new Hashtable();

            for (int i = 0; i < keepWords && i < centValuesArr.Length; i++)
            {
                DictionaryEntry entry = centValuesArr[i];
                finalCentroidValues.Add(entry.Key, entry.Value);
            }

            return (finalCentroidValues);
        }

        /*
        public static double idf(DocsStatistics docStats, string firstWord)
        {
            double wordRefCount = docStats.wordRefs[firstWord] == null ? 0 : ((HashSet<Document>)docStats.wordRefs[firstWord]).Count;
            return (Math.Log(docStats.docCount / (wordRefCount + 1)));
        }
        //*/
    }
}
