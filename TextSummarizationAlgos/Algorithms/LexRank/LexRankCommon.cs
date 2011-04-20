using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using System.Collections;
using TextSummarizationAlgos.Utils;
using Utils;

namespace TextSummarizationAlgos.Algorithms.LexRank
{
    class LexRankCommon
    {
        private LexRankCommon()
        {
        }

        public static double[] powerMethod(double[][] matrix, double errorTolerance)
        {
            double[] result = null;

            // Initialize P
            double[] p = new double[matrix.Length];
            double uniformProb = 1 / (double)p.Length;

            for (int i = 0; i < p.Length; i++)
                p[i] = uniformProb;

            double diff = 0;
            int t = 0;

            Trace.write(" P Initial values : ");
            Trace.write(MatrixUtil.printMatrix(p));

            do
            {
                t++;
                double[] pNew = MatrixUtil.multiply(matrix, p);

                Trace.write("P : ");
                Trace.write(MatrixUtil.printMatrix(pNew));

                diff = MatrixUtil.vectorNorm(MatrixUtil.subtract(pNew, p));

                p = pNew;
            } while (diff > errorTolerance);

            Console.WriteLine("After " + t + " iterations");

            result = p;

            return (result);
        }

        public static double[][] generateIdfModifiedCosineMatrix(IDF idf, ArrayList sentences)
        {
            double[][] idfModifiedCosine = new double[sentences.Count][];

            for (int i = 0; i < sentences.Count; i++)
            {
                idfModifiedCosine[i] = new double[sentences.Count];
            }

            for (int i = 0; i < sentences.Count; i++)
            {
                Sentence firstSent = (Sentence)sentences[i];

                for (int j = 0; j < sentences.Count; j++)
                {
                    // same sentence then 1
                    //*
                    if (i == j)
                    {
                        idfModifiedCosine[i][j] = 1;
                        continue;
                    }
                    //*/

                    // has been processed before
                    if (idfModifiedCosine[i][j] != 0)
                        continue;

                    Sentence secondSent = (Sentence)sentences[j];

                    idfModifiedCosine[i][j] = idfModifiedCos(idf, firstSent, secondSent);
                    idfModifiedCosine[j][i] = idfModifiedCosine[i][j];
                }
            }

            return (idfModifiedCosine);
        }

        public static double idfModifiedCos(IDF idf, Sentence firstSentence, Sentence secondSentence)
        {
            double idfModifiedCosine = 0;

            HashSet<string> commonWords = new HashSet<string>();

            foreach (string aWord in firstSentence.words)
            {
                if (secondSentence.words.Contains(aWord))
                    commonWords.Add(aWord);
            }

            double numerator = 0;

            foreach (string aWord in commonWords)
            {
                numerator += (termFrequency(firstSentence, aWord) * termFrequency(secondSentence, aWord) * Math.Pow(idf.get(aWord), 2));
            }

            double denominator1 = 0;

            foreach (string aWord in firstSentence.words)
            {
                //if (docStats.wordRefs[aWord] != null)
                denominator1 += Math.Pow(termFrequency(firstSentence, aWord) * idf.get(aWord), 2);
            }

            denominator1 = Math.Sqrt(denominator1);

            double denominator2 = 0;

            foreach (string aWord in secondSentence.words)
            {
                //if (docStats.wordRefs[aWord] != null)
                denominator2 += Math.Pow(termFrequency(secondSentence, aWord) * idf.get(aWord), 2);
            }

            denominator2 = Math.Sqrt(denominator2);

            idfModifiedCosine = numerator / (denominator1 * denominator2);

            return (idfModifiedCosine);
        }

        /*
        public static double[][] generateIdfModifiedCosineMatrix(DocsStatistics docStats, ArrayList sentences)
        {
            double[][] idfModifiedCosine = new double[sentences.Count][];

            for (int i = 0; i < sentences.Count; i++)
            {
                idfModifiedCosine[i] = new double[sentences.Count];
            }

            for (int i = 0; i < sentences.Count; i++)
            {
                Sentence firstSent = (Sentence)sentences[i];

                for (int j = 0; j < sentences.Count; j++)
                {
                    // same sentence then 1
                    //*
                    if (i == j)
                    {
                        idfModifiedCosine[i][j] = 1;
                        continue;
                    }
                    //* /

                    // has been processed before
                    if (idfModifiedCosine[i][j] != 0)
                        continue;

                    Sentence secondSent = (Sentence)sentences[j];

                    idfModifiedCosine[i][j] = idfModifiedCos(docStats, firstSent, secondSent);
                    idfModifiedCosine[j][i] = idfModifiedCosine[i][j];
                }
            }

            return (idfModifiedCosine);
        }

        public static double idfModifiedCos(DocsStatistics docStats, Sentence firstSentence, Sentence secondSentence)
        {
            double idfModifiedCosine = 0;

            HashSet<string> commonWords = new HashSet<string>();

            foreach (string aWord in firstSentence.words)
            {
                if (secondSentence.words.Contains(aWord))
                    commonWords.Add(aWord);
            }

            double numerator = 0;

            foreach (string aWord in commonWords)
            {
                numerator += (termFrequency(firstSentence, aWord) * termFrequency(secondSentence, aWord) * Math.Pow(idf(docStats, aWord), 2));
            }

            double denominator1 = 0;

            foreach (string aWord in firstSentence.words)
            {
                //if (docStats.wordRefs[aWord] != null)
                denominator1 += Math.Pow(termFrequency(firstSentence, aWord) * idf(docStats, aWord), 2);
            }

            denominator1 = Math.Sqrt(denominator1);

            double denominator2 = 0;

            foreach (string aWord in secondSentence.words)
            {
                //if (docStats.wordRefs[aWord] != null)
                denominator2 += Math.Pow(termFrequency(secondSentence, aWord) * idf(docStats, aWord), 2);
            }

            denominator2 = Math.Sqrt(denominator2);

            idfModifiedCosine = numerator / (denominator1 * denominator2);

            return (idfModifiedCosine);
        }
        //*/

        public static double termFrequency(Sentence sent, string word)
        {
            //double tf = sent.wordsCount[firstWord] == null ? 0 : ((int)sent.wordsCount[firstWord] / sent.words.Length);
            double tf = sent.wordsCount[word] == null ? 0 : (int)sent.wordsCount[word];

            return (tf);
        }

        public static double idf(DocsStatistics docStats, string word)
        {
            double wordRefCount = docStats.wordRefs[word] == null ? 0 : ((HashSet<Document>)docStats.wordRefs[word]).Count;
            return (Math.Log(docStats.docCount / (wordRefCount + 1)));
        }
    }
}
