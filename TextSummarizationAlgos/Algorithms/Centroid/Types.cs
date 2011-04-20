using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TextSummarizationAlgos.Algorithms.Centroid
{
    public class Centroid
    {
        //string[] words = null;
        //public Hashtable values = null;
        public Hashtable values = null;
        public int noOfDocuments = 0;
        private int keepWords = 0;

        public Centroid(Hashtable centroidValues, int keepWords)
        {
            this.values = centroidValues;
            this.noOfDocuments = 1;
            this.keepWords = keepWords;
        }

        public void addDocument(Hashtable newDoc)
        {
            HashSet<string> allWords = new HashSet<string>();

            foreach (string word in newDoc.Keys)
            {
                allWords.Add(word);
            }

            foreach (string word in this.values.Keys)
            {
                allWords.Add(word);
            }

            foreach (string word in allWords)
            {
                double average = this.values[word] == null ? 0 : ((double)this.values[word]);
                double newValue = newDoc[word] == null ? 0 : ((double)newDoc[word]);

                this.values[word] = ((average * (double)this.noOfDocuments) + newValue) / (this.noOfDocuments + 1);
            }

            /*
            foreach (string firstWord in newDoc.Keys)
            {
                if (this.values[firstWord] == null)
                    this.values[firstWord] = ((double)newDoc[firstWord]) / (this.noOfDocuments + 1);
                else
                    this.values[firstWord] = (((double)this.values[firstWord] * (double)this.noOfDocuments) + ((double)newDoc[firstWord])) / (this.noOfDocuments + 1);
            }

            ArrayList remainingKeys = new ArrayList();

            foreach (string firstWord in this.values)
            {
                if (newDoc[firstWord] == null)
                    remainingKeys.Add(firstWord);
            }

            foreach (string firstWord in remainingKeys)
            {
                this.values[firstWord] = ((double)this.values[firstWord] * (double)this.noOfDocuments) / (this.noOfDocuments + 1);
            }
            //*/

            this.noOfDocuments++;

            //this.values = normalize(this.values, this.keepWords);
        }

        public void applyKeepWords()
        {
            this.values = applyKeepWords(this.values, this.keepWords);
        }

        public static Hashtable applyKeepWords(Hashtable centroidValues, int keepWords)
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
    }

    /*
    private class CentroidEntry
    {
        public string firstWord = null;
        public double value = 0;

        public CentroidEntry(string firstWord, double value)
        {
            this.firstWord = firstWord;
            this.value = value;
        }
    }
    //*/

    // Used for sorting according to value
    public class DictionaryEntryValueComparer : IComparer
    {
        public Int32 Compare(object obj1, object obj2)
        {
            DictionaryEntry value1 = (DictionaryEntry)obj1;
            DictionaryEntry value2 = (DictionaryEntry)obj2;

            return (Comparer.DefaultInvariant.Compare(value1.Value, value2.Value));
        }
    }
}
