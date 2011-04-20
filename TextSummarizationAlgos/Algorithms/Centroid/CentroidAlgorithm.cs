using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using Utils;
using System.Collections;

namespace TextSummarizationAlgos.Algorithms.Centroid
{
    public class CentroidAlgorithm : MultipleDocTextSummarizationAlgorithm
    {
        private string clustersDir = null;

        private double idfThreshold = 0;
        private int keepWords = 0;

        private double centroidWeight = 1;
        private double positionalWeight = 1;
        private double firstSentenceWeight = 1;

        //private double compressionRatio = 0.3;

        CentroidCluster[] centroidClusters = null;

        public CentroidAlgorithm(string clustersDir, double centroidWeight, double positionalWeight, double firstSentenceWeight, double idfThreshold, int keepWords)
        {
            this.clustersDir = clustersDir;

            this.idfThreshold = idfThreshold;
            this.keepWords = keepWords;

            this.centroidWeight = centroidWeight;
            this.positionalWeight = positionalWeight;
            this.firstSentenceWeight = firstSentenceWeight;
            //this.compressionRatio = compressionRatio;
        }

        //override public string generateSummary(DocsStatistics docStats, Document newDoc)
        override public string generateSummary(ArrayList docs, double compressionRatio)
        {
            ArrayList allTitles = new ArrayList();
            ArrayList allFirstSents = new ArrayList();
            ArrayList allSents = new ArrayList();

            foreach (Document doc in docs)
            {
                allTitles.Add(doc.title);
                if (doc.sentences.Count >= 1)
                    allFirstSents.Add(doc.sentences[0]);
                allSents.AddRange(doc.sentences);
            }

            double[] cTotal = new double[allSents.Count];
            double[] pTotal = new double[allSents.Count];
            double[] fTotal = new double[allSents.Count];
            double cMax = double.MinValue;

            if (this.centroidClusters == null)
                this.centroidClusters = CentroidCluster.fromFolder(this.clustersDir, this.idfThreshold, this.keepWords);

            for (int i = 0; i < allSents.Count; i++)
            {
                Sentence currSent = (Sentence)allSents[i];

                // Calculate C
                cTotal[i] = 0;
                foreach (string word in currSent.words)
                {
                    cTotal[i] += getCentroidValue(this.centroidClusters, word);
                }

                if (cTotal[i] > cMax)
                    cMax = cTotal[i];

                // Calculate F
                fTotal[i] = 0;

                foreach (string word in currSent.words)
                {
                    int wordOccurence = 0;

                    foreach (Sentence title in allTitles)
                    {
                        if (title.wordsCount[word] != null)
                        {
                            wordOccurence += ((int)title.wordsCount[word]);
                        }
                    }

                    foreach (Sentence firstSent in allFirstSents)
                    {
                        if (firstSent.wordsCount[word] != null)
                        {
                            wordOccurence += ((int)firstSent.wordsCount[word]);
                        }
                    }

                    fTotal[i] += (wordOccurence * ((int)currSent.wordsCount[word]));
                }
            }

            // Calculate P
            int pIndex = 0;
            foreach (Document doc in docs)
            {
                for (int i = 0; i < doc.sentences.Count; i++)
                {
                    // Remove + 1 as arrays are zero based.
                    pTotal[pIndex++] = ((doc.sentences.Count - i) * cMax) / doc.sentences.Count;
                }
            }

            double maxScore = double.MinValue;

            for (int i = 0; i < allSents.Count; i++)
            {
                double currWeight = (this.centroidWeight * cTotal[i]) + (this.positionalWeight * pTotal[i]) + (this.firstSentenceWeight * fTotal[i]);

                ((Sentence)allSents[i]).weight = currWeight;

                if (currWeight > maxScore)
                    maxScore = currWeight;
            }

            string genSummary = null;
            string prevgenSummary = null;

            do
            {
                for (int i = 0; i < allSents.Count; i++)
                {
                    for (int j = 0; j < allSents.Count; j++)
                    {
                        if (i >= j)
                            continue;

                        double redundancy = redundancyPenalty((Sentence)allSents[i], (Sentence)allSents[j]);

                        ((Sentence)allSents[j]).weight -= (maxScore * redundancy);
                    }
                }

                maxScore = double.MinValue;

                for (int i = 0; i < allSents.Count; i++)
                {
                    if (((Sentence)allSents[i]).weight > maxScore)
                        maxScore = ((Sentence)allSents[i]).weight;
                }

                Sentence[] sents = (Sentence[])allSents.ToArray(typeof(Sentence));

                prevgenSummary = genSummary;

                genSummary = SummaryUtil.SummarizeByCompressionRatio(sents, compressionRatio);
            } while (!genSummary.Equals(prevgenSummary));

            return (genSummary);
        }

        private static double redundancyPenalty(Sentence firstSentence, Sentence secondSentence)
        {
            double redundancy = 0;
            HashSet<string> commonWords = SummaryUtil.getCommonWords(firstSentence, secondSentence);

            redundancy = (double)(2 * commonWords.Count) / (double)(firstSentence.words.Count + secondSentence.words.Count);

            return (redundancy);
        }

        public static double getCentroidValue(CentroidCluster[] centroids, string word)
        {
            double result = 0;

            foreach (CentroidCluster centroid in centroids)
            {
                if (centroid.centroidWords[word] != null)
                {
                    result += (double)centroid.centroidWords[word];
                }
            }

            return (result);
        }

        public static double termFrequency(DocsStatistics docStats, string word)
        {
            //double tf = sent.wordsCount[firstWord] == null ? 0 : ((int)sent.wordsCount[firstWord] / sent.words.Length);
            double tf = docStats.wordsCount[word] == null ? 0 : (int)docStats.wordsCount[word];

            if (tf != 0)
                tf = tf / ((HashSet<Document>)docStats.wordRefs[word]).Count;

            return (tf);
        }

        public static double idf(DocsStatistics docStats, string word)
        {
            double wordRefCount = docStats.wordRefs[word] == null ? 0 : ((HashSet<Document>)docStats.wordRefs[word]).Count;
            return (Math.Log(docStats.docCount / (wordRefCount + 1)));
        }
    }
}
