using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace Utils
{
    public abstract class StopWordsHandler
    {
        private static StopWordsHandler stopWords = null;

        public static void setInstance(StopWordsHandler stopWordsHandler)
        {
            stopWords = stopWordsHandler;
        }

        public static StopWordsHandler getInstance(string stopWordsFilename)
        {
            if (stopWords == null)
                stopWords = new DefaultStopWordsHandler(stopWordsFilename);

            return (stopWords);
        }

        public abstract string remove(string text);
    }

    public class DefaultStopWordsHandler : StopWordsHandler
    {
        string filename;
        ArrayList words;
        Regex stopWordsRegex;

        public DefaultStopWordsHandler(string stopWordsFilename)
        {
            this.filename = stopWordsFilename;
            this.words = null;
            this.stopWordsRegex = null;

            this.load();
        }

        public void load()
        {
            IEnumerable lines = File.ReadLines(this.filename, Encoding.Default);

            this.words = new ArrayList();

            foreach (string line in lines)
            {
                if (line == null)
                    continue;
                if (line.Trim().Equals(""))
                    continue;
                this.words.Add(line);
            }

            string genRegex = "";
            for (int i = 0; i < (this.words.Count - 1); i++)
            {
                genRegex += @"\s+" + this.words[i] + @"\s+" + "|";
            }
            genRegex += this.words[this.words.Count - 1];

            this.stopWordsRegex = new Regex(genRegex, RegexOptions.Compiled);
        }

        override public string remove(string text)
        {
            string result = this.stopWordsRegex.Replace(text, " ");

            return (result);
        }

        ArrayList Words
        {
            get
            {
                return (this.words);
            }
        }
    }

    // When Stop Words removal is disabled
    public class NullStopWordsHandler : StopWordsHandler
    {
        public NullStopWordsHandler()
        {
        }

        public override string remove(string text)
        {
            return (text);
        }
    }
}
