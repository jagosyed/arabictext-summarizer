using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using Utils;
using TextSummarizationAlgos.Utils;

namespace TextSummarizationAlgos.Algorithms
{
    public class LakhasAlgorithm : SingleDocTextSummarizationAlgorithm
    {
        override public string generateSummary(Document newDoc, double compressionRatio)
        {
            //Document newDoc = Document.process(newDocText);
            //Document newDoc = Conf.getDocumentProcessor().process(newDocText);

            foreach (Sentence aSent in newDoc.sentences)
            {
                calcSentenceWeight(IDF.getInstance(), newDoc, aSent);
            }

            //object[] sents = newDoc.sentences.ToArray();
            Sentence[] sents = (Sentence[])newDoc.sentences.ToArray(typeof(Sentence));

            string genSummary = "";
            genSummary = SummaryUtil.SummarizeByCompressionRatio(sents, compressionRatio);

            /*
            Array.Sort(sents, new SentenceComparer());
            Array.Reverse(sents);

            

            int numSents = NUM_SENTENCES;
            if (sents.Length < numSents)
                numSents = sents.Length;

            for (int i = 0; i < numSents; i++)
            {
                genSummary += ((Sentence)sents[i]).fullText + "\r\n";
            }
            //*/

            /*
            string dbgString = "";
            foreach (Sentence aSent in sents)
            {
                dbgString += aSent.fullText + "\r\n";
            }

            debugClipboard(dbgString);
            //*/

            return (genSummary);
        }

        public static double calcSentenceWeight(IDF idf, Document doc, Sentence sent)
        {
            Trace.write(sent.fullText);
            double weight = 0;

            // 1: ScLead
            double sclead = 0;

            if (sent == doc.sentences[0])
                sclead = 2;
            else
                sclead = 1;

            Trace.write("SCLead : " + sclead);

            // 2: ScTitle
            double sctitle = 0;
            foreach (string aWord in sent.words)
            {
                //double tf = docStats.wordsCount[aWord] == null ? 0 : (((int)docStats.wordsCount[aWord]) / docStats.wordTotal);
                //double tf = termFrequency(docStats, aWord);
                double tf = termFrequency(sent, aWord);

                if (doc.title != null)
                {
                    if (doc.title.words.ToArray().Contains(aWord))
                        sctitle += (2 * tf);
                }
            }

            Trace.write("SCTitle : " + sctitle);

            // 3: sccue
            double sccue = 0;

            foreach (string aWord in sent.words)
            {
                if (CueWords.getInstance(Conf.CUE_WORDS_PATH).contains(aWord))
                {
                    double tf = termFrequency(sent, aWord);

                    sccue += tf;
                }
            }

            Trace.write("SCCue : " + sccue);

            // 4: sctfidf
            double sctfidf = 0;

            foreach (string aWord in sent.words)
            {
                //double tf = termFrequency(docStats, aWord);
                double tf = termFrequency(sent, aWord);

                //if (docStats.wordRefs[aWord] != null && tf != 0)
                if (tf != 0)
                    //sctfidf += (((tf - 1) / tf) * Math.Log(docStats.docCount / ((HashSet<Document>)docStats.wordRefs[aWord]).Count));
                    sctfidf += (((tf - 1) / tf) * idf.get(aWord));
            }

            //sctfidf = sctfidf / docStats.sentCount;
            //sctfidf = sctfidf / doc.sentences.Count;
            //sctfidf = sctfidf / sent.words.Length;
            sctfidf = sctfidf / sent.words.Count;

            Trace.write("SCTFIDF : " + sctfidf);

            weight = sclead + sctitle + sccue + sctfidf;

            sent.weight = weight;

            Trace.write("Weight : " + weight);

            return (weight);
        }

        public static double termFrequency(Sentence sent, string word)
        {
            //double tf = sent.wordsCount[firstWord] == null ? 0 : ((int)sent.wordsCount[firstWord] / sent.words.Length);
            double tf = sent.wordsCount[word] == null ? 0 : (int)sent.wordsCount[word];

            return (tf);
        }
        /*
        public static double termFrequency(DocsStatistics docStats, string firstWord)
        {
            double tf = docStats.wordsCount[firstWord] == null ? 0 : ((int)docStats.wordsCount[firstWord] / docStats.wordTotal);

            return (tf);
        }
        //*/
    }
}
