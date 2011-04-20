using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

namespace Utils
{
    public class CueWords
    {
        string filename;
        ArrayList words;
        Regex cueWordsRegex;

        private static CueWords stopWords = null;

        public static CueWords getInstance(string stopWordsFilename)
        {
            if (stopWords == null)
                stopWords = new CueWords(stopWordsFilename);

            return (stopWords);
        }

        private CueWords(string stopWordsFilename)
        {
            this.filename = stopWordsFilename;
            this.words = null;
            this.cueWordsRegex = null;

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

            string genRegex = "^";
            for (int i = 0; i < (this.words.Count - 1); i++)
            {
                genRegex += this.words[i] + "|";
            }
            genRegex += this.words[this.words.Count - 1] + "$";

            this.cueWordsRegex = new Regex(genRegex, RegexOptions.Compiled);
        }

        public bool contains(string word)
        {
            return this.cueWordsRegex.IsMatch(word);
        }

        ArrayList Words
        {
            get
            {
                return (this.words);
            }
        }
    }

    //*
    public abstract class CueWordsHandler
    {
        public abstract bool contains(string word);
    }

    public class DefaultCueWordsHandler
    {
        string filename;
        ArrayList words;
        Regex cueWordsRegex;

        public DefaultCueWordsHandler(string stopWordsFilename)
        {
            this.filename = stopWordsFilename;
            this.words = null;
            this.cueWordsRegex = null;

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

            string genRegex = "^";
            for (int i = 0; i < (this.words.Count - 1); i++)
            {
                genRegex += this.words[i] + "|";
            }
            genRegex += this.words[this.words.Count - 1] + "$";

            this.cueWordsRegex = new Regex(genRegex, RegexOptions.Compiled);
        }

        public bool contains(string word)
        {
            return this.cueWordsRegex.IsMatch(word);
        }

        ArrayList Words
        {
            get
            {
                return (this.words);
            }
        }
    }

    public class NullCueWordsHandler : CueWordsHandler
    {
        override public bool contains(string word)
        {
            return (false);
        }
    }
    //*/
}
