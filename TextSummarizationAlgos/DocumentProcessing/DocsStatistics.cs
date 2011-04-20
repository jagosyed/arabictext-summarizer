using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
namespace TextSummarizationAlgos.DocumentProcessing
{
    public class DocsStatistics
    {
        public Hashtable wordsCount;
        public Hashtable wordRefs;
        public Hashtable wordRefsCount;
        public int docCount;
        public int sentCount;
        public int wordTotal;

        public DocsStatistics()
        {
            this.wordsCount = new Hashtable();
            this.wordRefs = new Hashtable();
            this.wordRefsCount = new Hashtable();
            this.docCount = 0;
            this.sentCount = 0;
            this.wordTotal = 0;
        }

        public static DocsStatistics generateStatistics(ArrayList docs)
        {
            DocsStatistics docsStat = new DocsStatistics();

            foreach (Document doc in docs)
            {
                foreach (Sentence sent in doc.sentences)
                {
                    foreach (string currWord in sent.words)
                    {
                        if (docsStat.wordsCount[currWord] == null)
                            docsStat.wordsCount[currWord] = 1;
                        else
                        {
                            docsStat.wordsCount[currWord] = ((int)docsStat.wordsCount[currWord]) + 1;
                        }

                        if (docsStat.wordRefs[currWord] == null)
                            docsStat.wordRefs[currWord] = new HashSet<Document>();

                        ((HashSet<Document>)docsStat.wordRefs[currWord]).Add(doc);

                        docsStat.wordTotal++;
                    }
                    docsStat.sentCount++;
                }
                docsStat.docCount++;
            }

            return (docsStat);
        }

        public void addDocument(Document doc)
        {
            HashSet<string> docWords = new HashSet<string>();
            //Hashtable docWords = new Hashtable();

            foreach (Sentence sent in doc.sentences)
            {
                foreach (string currWord in sent.words)
                {
                    if (this.wordsCount[currWord] == null)
                        this.wordsCount[currWord] = 1;
                    else
                    {
                        this.wordsCount[currWord] = ((int)this.wordsCount[currWord]) + 1;
                    }

                    if (!docWords.Contains(currWord))
                    {
                        if (this.wordRefsCount[currWord] == null)
                            this.wordRefsCount[currWord] = 1;
                        else
                            this.wordRefsCount[currWord] = ((int)this.wordRefsCount[currWord]) + 1;
                    }

                    docWords.Add(currWord);
                    //docWords[currWord] = 1;

                    /*
                    if (this.wordRefs[currWord] == null)
                        this.wordRefs[currWord] = new HashSet<Document>();

                    ((HashSet<Document>)this.wordRefs[currWord]).Add(doc);
                    //*/

                    this.wordTotal++;
                }
                this.sentCount++;
            }
            this.docCount++;
        }
    }
}