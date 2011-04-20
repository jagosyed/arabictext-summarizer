using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using Utils;
using System.Collections;

namespace TextSummarizationAlgos.Algorithms
{
    class SummaryUtil
    {
        private SummaryUtil()
        {
        }

        public static HashSet<string> getCommonWords(ArrayList first, ArrayList second)
        {
            HashSet<string> commonWords = new HashSet<string>();

            foreach (string aWord in first)
            {
                if (second.Contains(aWord))
                    commonWords.Add(aWord);
            }

            return (commonWords);
        }

        public static HashSet<string> getCommonWords(Sentence firstSentence, Sentence secondSentence)
        {
            HashSet<string> commonWords = new HashSet<string>();

            foreach (string aWord in firstSentence.words)
            {
                if (secondSentence.words.Contains(aWord))
                    commonWords.Add(aWord);
            }

            return (commonWords);
        }

        public static string SummarizeByCompressionRatio(Sentence[] sents, double ratio)
        {
            if (!(ratio > 0 && ratio <= 1))
                throw new ArgumentOutOfRangeException("ratio");

            int count = (int)(sents.Length * ratio);

            return (SummarizeBySentenceCount(sents, count));
        }

        public static string SummarizeBySentenceCount(Sentence[] sents, int count)
        {
            //Sentence[] originalSents = new Sentence[sents.Length];
            //Array.Copy(sents, originalSents, sents.Length);
            Array.Sort(sents, new SentenceComparer());
            Array.Reverse(sents);

            foreach (Sentence sent in sents)
            {
                Trace.write(sent.fullText);
                Trace.write("Weight : " + sent.weight);
            }

            return (getText(sents, count));
        }

        public static string getText(Sentence[] sents, int count)
        {
            string genSummary = "";
            int numSents = count;
            if (sents.Length < numSents)
                numSents = sents.Length;

            for (int i = 0; i < numSents; i++)
            {
                genSummary += sents[i].fullText + Environment.NewLine;
            }

            return (genSummary);
        }
    }
}
