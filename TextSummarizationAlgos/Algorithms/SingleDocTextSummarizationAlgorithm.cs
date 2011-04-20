using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TextSummarizationAlgos.DocumentProcessing;

namespace TextSummarizationAlgos.Algorithms
{
    public abstract class SingleDocTextSummarizationAlgorithm : TextSummarizationAlgorithm
    {
        abstract public string generateSummary(Document newDoc, double compressionRatio);

        /*
        public string getText(Sentence[] sents)
        {
            string genSummary = "";
            int numSents = NUM_SENTENCES;
            if (sents.Length < numSents)
                numSents = sents.Length;

            for (int i = 0; i < numSents; i++)
            {
                genSummary += ((Sentence)sents[i]).fullText + "\r\n";
            }

            return (genSummary);
        }
        //*/
    }
}
