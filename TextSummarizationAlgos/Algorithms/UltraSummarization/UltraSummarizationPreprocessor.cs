using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;
using System.Collections;
using Utils;
using System.IO;

namespace TextSummarizationAlgos.Algorithms.UltraSummarization
{
    public class UltraSummarizationPreprocessor
    {
        public UltraSummarizationPreprocessor()
        {
        }

        private DocsStatistics processFiles(string[] files)
        {
            DocumentProcessor docProcessor = new DocumentProcessor();
            DocsStatistics docStats = new DocsStatistics();

            foreach (string filename in files)
            {
                Document doc = docProcessor.process(filename);

                docStats.addDocument(doc);
            }

            return (docStats);
        }

        public Hashtable preprocessTranslationModel(string[] originalFiles, string[] summariesFiles)
        {
            DocsStatistics originalDocStats = processFiles(originalFiles);
            DocsStatistics summariesDocStats = processFiles(summariesFiles);

            Hashtable translationModel = null;

            foreach (string word in summariesDocStats.wordsCount.Keys)
            {
                if (originalDocStats.wordsCount[word] != null)
                    continue;

                double originalCount = (double)((originalDocStats.wordsCount[word] == null) ? 0 : originalDocStats.wordsCount[word]);
                double summaryCount = (double)((summariesDocStats.wordsCount[word] == null) ? 0 : summariesDocStats.wordsCount[word]);

                if (translationModel == null)
                    translationModel = new Hashtable();

                translationModel[word] = summaryCount / originalCount;
            }

            return (translationModel);
        }

        public void preprocessLanguageModel(string[] documentFiles, string bigramFilePath)
        {
            // No need for Stop Words Removal.
            StopWordsHandler.setInstance(new NullStopWordsHandler());

            DocumentProcessor docProcessor = new DocumentProcessor();
            BigramStatisticsModel bigramStats = new BigramStatisticsModel();

            int i = 0;

            foreach (string filename in documentFiles)
            {
                ++i;
                string fileContent = File.ReadAllText(filename, Encoding.Default);
                Document doc = docProcessor.process(fileContent);

                bigramStats.addDocument(doc);
            }

            bigramStats.toFile(bigramFilePath);
        }

        public class BigramStatisticsModel
        {
            public Hashtable wordsCount = null;
            public Hashtable wordsBigram = null;

            public BigramStatisticsModel()
            {
                this.wordsCount = new Hashtable();
                this.wordsBigram = new Hashtable();
            }

            public void addDocument(Document doc)
            {
                foreach (Sentence sent in doc.sentences)
                {
                    string prevWord = null;

                    for (int i = 0; i < sent.words.Count; i++)
                    {
                        string currWord = (string)sent.words[i];
                        bool isFirst = (prevWord == null);

                        if (this.wordsCount[currWord] == null)
                            this.wordsCount[currWord] = 1;
                        else
                        {
                            this.wordsCount[currWord] = ((int)this.wordsCount[currWord]) + 1;
                        }

                        if (isFirst)
                        {
                            prevWord = currWord;
                            continue;
                        }

                        if (this.wordsBigram[prevWord] == null)
                            this.wordsBigram[prevWord] = new Hashtable();

                        Hashtable currWordBigram = (Hashtable)this.wordsBigram[prevWord];

                        if (currWordBigram[currWord] == null)
                            currWordBigram[currWord] = 1;
                        else
                        {
                            currWordBigram[currWord] = ((int)currWordBigram[currWord]) + 1;
                        }

                        prevWord = currWord;
                    }
                }
            }

            public void toFile(string filepath)
            {
                File.WriteAllText(filepath, this.ToString());
            }

            public override string ToString()
            {
                StringBuilder result = null;

                foreach (string firstWord in this.wordsBigram.Keys)
                {
                    Hashtable currWordBigram = (Hashtable)this.wordsBigram[firstWord];

                    if (currWordBigram != null)
                    {
                        foreach (string secondWord in currWordBigram.Keys)
                        {
                            int currBigramCount = ((int)currWordBigram[secondWord]);
                            int firstWordCount = ((int)this.wordsCount[firstWord]);

                            double bigramValue = (double)currBigramCount / (double)firstWordCount;

                            if (result == null)
                                result = new StringBuilder();

                            result.Append(firstWord + "\t " + secondWord + "\t" + bigramValue + Environment.NewLine);
                        }
                    }
                }

                return (result.ToString());
            }
        }
    }
}
