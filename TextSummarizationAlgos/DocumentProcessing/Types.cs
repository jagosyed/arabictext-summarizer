using System.Collections;
using System;
namespace TextSummarizationAlgos.DocumentProcessing
{
    public class Document : IDisposable
    {
        public string originalText;
        public string fullText;
        public Sentence title;
        public ArrayList sentences;

        public void Dispose()
        {
            this.originalText = null;
            this.fullText = null;
            this.title.Dispose();

            for (int i = 0; i < this.sentences.Count; i++)
            {
                ((Sentence)this.sentences[i]).Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }

    public class Sentence : IDisposable
    {
        public string fullText;
        //public string[] words;
        public ArrayList words;
        public double weight;
        public Hashtable wordsCount;
        public int order = 0;

        public Sentence()
        {
            this.fullText = null;
            this.words = null;
            this.weight = 0;
            this.wordsCount = null;
            this.order = 0;
        }

        public void Dispose()
        {
            this.fullText = null;
            this.words = null;
            this.wordsCount = null;
            GC.SuppressFinalize(this);
        }
    }

    // Used for sorting sentences according to weight.
    class SentenceComparer : IComparer
    {
        public Int32 Compare(object obj1, object obj2)
        {
            Sentence sent1 = (Sentence)obj1;
            Sentence sent2 = (Sentence)obj2;

            return (Comparer.DefaultInvariant.Compare(sent1.weight, sent2.weight));
        }
    }
}