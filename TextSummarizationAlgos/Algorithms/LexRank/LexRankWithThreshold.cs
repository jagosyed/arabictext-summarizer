using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using TextSummarizationAlgos.Utils;
using Utils;
using System.Collections;

namespace TextSummarizationAlgos.Algorithms.LexRank
{
    public class LexRankWithThreshold : MultipleDocTextSummarizationAlgorithm
    {
        private double threshold = 0.1;
        private double dampingFactor = 0.15;

        public LexRankWithThreshold(double threshold, double dampingFactor)
        {
            this.threshold = threshold;
            this.dampingFactor = dampingFactor;
        }

        //override public string generateSummary(DocsStatistics docStats, Document newDoc)
        override public string generateSummary(ArrayList docs, double compressionRatio)
        {
            string genSummary = "";

            ArrayList allSents = new ArrayList();

            foreach (Document doc in docs)
            {
                allSents.AddRange(doc.sentences);
            }

            double[][] idfModifiedCosineMatrix = LexRankCommon.generateIdfModifiedCosineMatrix(IDF.getInstance(), allSents);

            //*
            Trace.write(" IDF Cosine Matrix : ");
            Trace.write(MatrixUtil.printMatrix(idfModifiedCosineMatrix));
            //*/

            double[] sentDegree = new double[allSents.Count];

            for (int i = 0; i < sentDegree.Length; i++)
                sentDegree[i] = 0;

            for (int i = 0; i < idfModifiedCosineMatrix.Length; i++)
            {
                for (int j = 0; j < idfModifiedCosineMatrix[i].Length; j++)
                {
                    /*
                    if (i == j)
                        continue;
                    //*/
                    if (idfModifiedCosineMatrix[i][j] > this.threshold)
                    {
                        idfModifiedCosineMatrix[i][j] = 1;
                        sentDegree[i]++;
                    }
                    else
                    {
                        idfModifiedCosineMatrix[i][j] = 0;
                    }
                }
            }

            Trace.write(MatrixUtil.printMatrix(idfModifiedCosineMatrix));

            for (int i = 0; i < idfModifiedCosineMatrix.Length; i++)
                for (int j = 0; j < idfModifiedCosineMatrix[i].Length; j++)
                {
                    idfModifiedCosineMatrix[i][j] = idfModifiedCosineMatrix[i][j] / sentDegree[i];
                    idfModifiedCosineMatrix[i][j] = (dampingFactor / idfModifiedCosineMatrix.Length) + ((1 - dampingFactor) * idfModifiedCosineMatrix[i][j]);
                }

            Trace.write(MatrixUtil.printMatrix(idfModifiedCosineMatrix));

            double[] weights = LexRankCommon.powerMethod(idfModifiedCosineMatrix, 0.1);

            for (int i = 0; i < allSents.Count; i++)
            {
                ((Sentence)allSents[i]).weight = weights[i];
            }

            Sentence[] sents = (Sentence[])allSents.ToArray(new Sentence().GetType());

            genSummary = SummaryUtil.SummarizeByCompressionRatio(sents, compressionRatio);
            /*
            Array.Sort(sents, new SentenceComparer());
            Array.Reverse(sents);

            foreach (Sentence sent in sents)
            {
                Trace.write(sent.fullText);
                Trace.write("Weight : " + sent.weight);
            }

            genSummary = getText(sents);
            //*/
            return (genSummary);
        }
    }
}
