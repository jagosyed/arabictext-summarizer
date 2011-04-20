using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using Utils;
using System.Collections;
using TextSummarizationAlgos.Utils;

namespace TextSummarizationAlgos.Algorithms.Centroid
{
    public class CentroidAlgorithm2 : SingleDocTextSummarizationAlgorithm
    {
        //private int decayThreshold = 0;
        private double idfThreshold = 0;
        private int keepWords = 0;
        private double simThreshold = 0;

        private ArrayList trainingDocs = null;

        private double centroidWeight = 1;
        private double positionalWeight = 1;
        private double firstSentenceWeight = 1;

        private double compressionRatio = 0.3;

        public CentroidAlgorithm2(ArrayList docs, double centroidWeight, double positionalWeight, double firstSentenceWeight, double compressionRatio, double idfThreshold, int keepWords, double simThreshold)
        {
            this.trainingDocs = docs;

            this.idfThreshold = idfThreshold;
            this.keepWords = keepWords;
            this.simThreshold = simThreshold;

            this.centroidWeight = centroidWeight;
            this.positionalWeight = positionalWeight;
            this.firstSentenceWeight = firstSentenceWeight;
            this.compressionRatio = compressionRatio;
        }

        public static double sim(IDF idf, Hashtable first, Hashtable second)
        {
            double similarity = 0;

            HashSet<string> commonWords = SummaryUtil.getCommonWords(new ArrayList(first.Keys), new ArrayList(second.Keys));

            double numerator = 0;

            foreach (string aWord in commonWords)
            {
                numerator += ((double)first[aWord] * (double)second[aWord] * idf.get(aWord));
            }

            double denominator1 = 0;

            foreach (string aWord in first.Keys)
            {
                //if (docStats.wordRefs[aWord] != null)
                denominator1 += Math.Pow((double)first[aWord], 2);
            }

            denominator1 = Math.Sqrt(denominator1);

            double denominator2 = 0;

            foreach (string aWord in second.Keys)
            {
                //if (docStats.wordRefs[aWord] != null)
                denominator2 += Math.Pow((double)second[aWord], 2);
            }

            denominator2 = Math.Sqrt(denominator2);

            similarity = numerator / (denominator1 * denominator2);

            return (similarity);
        }

        public ArrayList buildCentroids(ArrayList docs, IDF idfdb)
        {
            ArrayList centroids = new ArrayList();

            foreach (Document doc in docs)
            {
                ArrayList currDoc = new ArrayList();
                currDoc.Add(doc);

                DocsStatistics currDocStats = DocsStatistics.generateStatistics(currDoc);

                Hashtable docVector = new Hashtable();

                foreach (DictionaryEntry entry in currDocStats.wordsCount)
                {
                    string word = (string)entry.Key;
                    int count = (int)entry.Value;

                    //double idf = CentroidAlgorithm2.idf(allDocStats, firstWord);
                    double idf = idfdb.get(word);

                    if (idf < this.idfThreshold)
                        continue;

                    double tfidf = ((double)count) * idf;

                    docVector[word] = tfidf;
                }

                if (centroids.Count == 0)
                {
                    Centroid centroid = new Centroid(docVector, this.keepWords);
                    centroid.noOfDocuments = 1;

                    centroids.Add(centroid);
                }
                else
                {
                    Centroid nearestCentroid = null;
                    double maxSimilarity = double.MinValue;

                    foreach (Centroid centroid in centroids)
                    {
                        double similarity = sim(IDF.getInstance(), centroid.values, docVector);

                        if (similarity > simThreshold)
                        {
                            if (similarity > maxSimilarity)
                            {
                                maxSimilarity = similarity;
                                nearestCentroid = centroid;
                            }
                        }
                    }

                    if (nearestCentroid == null)
                    {
                        nearestCentroid = new Centroid(docVector, this.keepWords);
                        centroids.Add(nearestCentroid);
                    }
                    else
                    {
                        nearestCentroid.addDocument(docVector);
                    }
                }
            }

            // Apply the KEEP_WORDS parameter for each centroid
            /*
            foreach (Centroid centroid in centroids)
            {
                Hashtable centroidValues = centroid.values;

                DictionaryEntry[] centValuesArr = new DictionaryEntry[centroids.Count];

                centroidValues.CopyTo(centValuesArr, 0);

                Array.Sort(centValuesArr, new DictionaryEntryValueComparer());
                Array.Reverse(centValuesArr);

                DictionaryEntry[] finalCentroidValuesArr = new DictionaryEntry[this.keepWords];

                Array.Copy(centValuesArr, finalCentroidValuesArr, this.keepWords);

                Hashtable finalCentroidValues = new Hashtable();

                foreach (DictionaryEntry entry in finalCentroidValuesArr)
                {
                    finalCentroidValues.Add(entry.Key, entry.Value);
                }

                centroid.values = finalCentroidValues;
            }
            //*/

            //*
            foreach (Centroid centroid in centroids)
            {
                centroid.applyKeepWords();
            }
            //*/

            // Trace
            /*
            int i = 0;
            foreach (Centroid centroid in centroids)
            {
                Trace.write("Centroid #" + (++i));
                foreach (DictionaryEntry entry in centroid.values)
                {
                    Trace.write(entry.Key + " : " + entry.Value);
                }
            }
            //*/

            return (centroids);
        }

        override public string generateSummary(Document newDoc, double compressionRatio)
        {
            double[] cTotal = new double[newDoc.sentences.Count];
            double[] pTotal = new double[newDoc.sentences.Count];
            double[] fTotal = new double[newDoc.sentences.Count];
            double cMax = double.MinValue;

            ArrayList centroids = buildCentroids(this.trainingDocs, IDF.getInstance());

            for (int i = 0; i < newDoc.sentences.Count; i++)
            {
                Sentence currSent = (Sentence)newDoc.sentences[i];

                // Calculate C
                cTotal[i] = 0;
                foreach (string word in currSent.words)
                {
                    /*
                    double tf = termFrequency(docStats, firstWord);
                    double idf = CentroidAlgorithm.idf(docStats, firstWord);
                    cTotal[i] += tf * idf;
                    //*/

                    cTotal[i] += getCentroidValue(centroids, word);
                }

                if (cTotal[i] > cMax)
                    cMax = cTotal[i];

                // Calculate F
                fTotal[i] = 0;

                foreach (string word in currSent.words)
                {
                    int wordOccurence = 0;

                    if (newDoc.title.wordsCount[word] != null)
                    {
                        wordOccurence += ((int)newDoc.title.wordsCount[word]);
                    }

                    if (newDoc.sentences.Count > 1)
                    {
                        if (((Sentence)newDoc.sentences[0]).wordsCount[word] != null)
                        {
                            wordOccurence += ((int)((Sentence)newDoc.sentences[0]).wordsCount[word]);
                        }
                    }

                    fTotal[i] += (wordOccurence * ((int)currSent.wordsCount[word]));
                }
            }

            // Calculate P
            for (int i = 0; i < newDoc.sentences.Count; i++)
            {
                // Remove + 1 as arrays are zero based.
                pTotal[i] = ((newDoc.sentences.Count - i) * cMax) / newDoc.sentences.Count;
            }

            double maxScore = double.MinValue;

            for (int i = 0; i < newDoc.sentences.Count; i++)
            {
                double currWeight = (this.centroidWeight * cTotal[i]) + (this.positionalWeight * pTotal[i]) + (this.firstSentenceWeight * fTotal[i]);

                ((Sentence)newDoc.sentences[i]).weight = currWeight;

                if (currWeight > maxScore)
                    maxScore = currWeight;
            }

            string genSummary = null;
            string prevgenSummary = null;

            do
            {
                for (int i = 0; i < newDoc.sentences.Count; i++)
                {
                    for (int j = 0; j < newDoc.sentences.Count; j++)
                    {
                        if (i >= j)
                            continue;

                        double redundancy = redundancyPenalty((Sentence)newDoc.sentences[i], (Sentence)newDoc.sentences[j]);

                        ((Sentence)newDoc.sentences[j]).weight -= (maxScore * redundancy);
                    }
                }

                maxScore = double.MinValue;

                for (int i = 0; i < newDoc.sentences.Count; i++)
                {
                    if (((Sentence)newDoc.sentences[i]).weight > maxScore)
                        maxScore = ((Sentence)newDoc.sentences[i]).weight;
                }

                Sentence[] sents = (Sentence[])newDoc.sentences.ToArray(new Sentence().GetType());

                prevgenSummary = genSummary;

                genSummary = SummaryUtil.SummarizeByCompressionRatio(sents, this.compressionRatio);
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

        public static double getCentroidValue(ArrayList centroids, string word)
        {
            double result = 0;

            foreach (Centroid centroid in centroids)
            {
                if (centroid.values[word] != null)
                {
                    result += (double)centroid.values[word];
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

        /*
        public static double idf(DocsStatistics docStats, string firstWord)
        {
            double wordRefCount = docStats.wordRefs[firstWord] == null ? 0 : ((HashSet<Document>)docStats.wordRefs[firstWord]).Count;
            return (Math.Log(docStats.docCount / (wordRefCount + 1)));
        }
        //*/
    }
}
