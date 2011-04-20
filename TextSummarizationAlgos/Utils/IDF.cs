using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TextSummarizationAlgos.DocumentProcessing;
using System.IO;
using Utils;

namespace TextSummarizationAlgos.Utils
{
    public class IDF
    {
        private static IDF _idf = null;

        protected Hashtable idf = null;

        public static void setInstance(IDF idf)
        {
            _idf = idf;
        }

        public static IDF getInstance()
        {
            return (_idf);
        }

        public static IDF fromFile(string filepath)
        {
            return (new IDF(filepath));
        }

        public static IDF fromDocuments(ArrayList docs)
        {
            return (new IDF(docs));
        }

        protected IDF()
        {
            this.idf = new Hashtable();
        }

        private IDF(string filepath)
        {
            this.load(filepath);
        }

        private IDF(ArrayList docs)
        {
            this.load(docs);
        }

        private void load(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            this.idf = new Hashtable();

            foreach (string line in lines)
            {
                string[] splits = line.Split('\t');

                if (splits.Length != 2)
                    continue;

                string word = splits[0];
                double wordIdf = Convert.ToDouble(splits[1]);

                this.idf[word] = wordIdf;
            }
        }

        private void load(ArrayList docs)
        {
            DocsStatistics docStats = DocsStatistics.generateStatistics(docs);

            this.idf = new Hashtable();

            foreach (string word in docStats.wordsCount.Keys)
            {
                double wordRefCount = docStats.wordRefs[word] == null ? 0 : ((HashSet<Document>)docStats.wordRefs[word]).Count;
                double wordIdf = Math.Log(docStats.docCount / (wordRefCount + 1));

                this.idf[word] = wordIdf;
            }
        }

        public double get(string word)
        {
            double idf = 0;

            if (this.idf[word] != null)
            {
                return ((double)this.idf[word]);
            }

            return (idf);
        }

        public void toFile(string filepath)
        {
            File.WriteAllText(filepath, this.ToString());
        }

        public override string ToString()
        {
            /*
            string result = "";

            foreach (string firstWord in this.idf.Keys)
            {
                result += (firstWord + "\t" + this.idf[firstWord] + Environment.NewLine);
            }
            //*/

            StringBuilder result = new StringBuilder();

            foreach (string word in this.idf.Keys)
            {
                result.Append(word + "\t" + this.idf[word] + Environment.NewLine);
            }

            return (result.ToString());
        }

        public class IDFGenerator
        {
            private IDFGenerator()
            {
            }

            public static IDF fromFiles(string[] files)
            {
                DocsStatistics docStats = new DocsStatistics();
                DocumentProcessor docProcessor = new DocumentProcessor();

                int i = 0;

                foreach (string file in files)
                {
                    ++i;
                    //processFile(docStats, file);
                    //*
                    string fileContent = File.ReadAllText(file, Encoding.Default);
                    Document doc = docProcessor.process(fileContent);
                    docStats.addDocument(doc);
                    /*
                    if ((i % 1000) == 0)
                    {
                        System.GC.Collect();
                        Trace.write("Done for : " + i);
                    }
                    //*/
                    //*/

                    //doc = null;
                }

                IDF idf = new IDF();

                foreach (string word in docStats.wordsCount.Keys)
                {
                    //double wordRefCount = docStats.wordRefs[firstWord] == null ? 0 : ((HashSet<Document>)docStats.wordRefs[firstWord]).Count;
                    double wordRefCount = docStats.wordRefsCount[word] == null ? 0 : ((int)docStats.wordRefsCount[word]);
                    double wordIdf = Math.Log(docStats.docCount / (wordRefCount));

                    idf.idf[word] = wordIdf;
                }

                return (idf);
            }

            private static void processFile(DocsStatistics docStats, string filename)
            {
                DocumentProcessor docProcessor = new DocumentProcessor();

                string fileContent = File.ReadAllText(filename, Encoding.Default);
                using (Document doc = docProcessor.process(fileContent))
                {
                    docStats.addDocument(doc);
                }
            }
        }
    }


}