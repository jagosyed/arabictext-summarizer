using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using System.Collections;
using Utils;
using TextSummarizationAlgos.Utils;

namespace TextSummarizationAlgos.Algorithms.LexRank
{
    public class LexRankDegreeCentrality : MultipleDocTextSummarizationAlgorithm
    {
        private double degreeCentrality = 0;

        public LexRankDegreeCentrality(double degreeCentrality)
        {
            this.degreeCentrality = degreeCentrality;
        }

        /*
        public string generateSummary(DocsStatistics docStats, string newDocText)
        {
            Document newDoc = Conf.getDocumentProcessor().process(newDocText);

            return (generateSummary(docStats, newDoc));
        }
        //*/

        //private static double DEGREE_CENTRALITY = 0.1;

        //override public string generateSummary(DocsStatistics docStats, Document newDoc)
        override public string generateSummary(ArrayList docs, double compressionRatio)
        {
            string genSummary = null;

            ArrayList allSents = new ArrayList();

            foreach (Document doc in docs)
            {
                allSents.AddRange(doc.sentences);
            }

            double[][] idfModifiedCosine = LexRankCommon.generateIdfModifiedCosineMatrix(IDF.getInstance(), allSents);

            Trace.write(" IDF Cosine Matrix : ");
            Trace.write(MatrixUtil.printMatrix(idfModifiedCosine));

            for (int i = 0; i < idfModifiedCosine.Length; i++)
            {
                int sentDegree = 0;

                for (int j = 0; j < idfModifiedCosine[i].Length; j++)
                {
                    if (idfModifiedCosine[i][j] > this.degreeCentrality)
                    {
                        ++sentDegree;
                    }
                }

                ((Sentence)allSents[i]).weight = sentDegree;
            }

            Sentence[] sents = (Sentence[])allSents.ToArray(typeof(Sentence));

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
