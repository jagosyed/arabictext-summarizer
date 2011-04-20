using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace TextSummarizationAlgos.Utils
{
    public abstract class Lemmatizer
    {
        private static Lemmatizer _lemmatization = null;

        public static void setInstance(Lemmatizer lemmatizer)
        {
            _lemmatization = lemmatizer;
        }

        public static Lemmatizer getInstance(string filename)
        {
            if (_lemmatization == null)
                _lemmatization = new DefaultLemmatizer(filename);

            return (_lemmatization);
        }

        public abstract string replace(string text);
    }

    public class DefaultLemmatizer : Lemmatizer
    {
        private string filename = null;
        private Regex replacementsRegex = null;
        private Hashtable replacements = null;

        public DefaultLemmatizer(string filename)
        {
            this.filename = filename;

            this.load();
        }

        public void load()
        {
            IEnumerable lines = File.ReadLines(this.filename, Encoding.Default);

            Regex lineRegex = new Regex(@"^(.+)\s+(.+)$", RegexOptions.Compiled);

            string replaceRegexString = null;
            ArrayList sepLines = new ArrayList();

            foreach (string line in lines)
            {
                if (line == null)
                    continue;
                if (line.Trim().Equals(""))
                    continue;

                sepLines.Add(line);
            }

            string lastMatch = null;

            for (int i = 0; i < sepLines.Count; i++)
            {
                string line = (string)sepLines[i];

                Match match = lineRegex.Match(line);

                if (match.Success)
                {
                    if (this.replacements == null)
                        this.replacements = new Hashtable();
                    if (replaceRegexString == null)
                        replaceRegexString = "";

                    this.replacements[match.Groups[1].Value] = match.Groups[2].Value;

                    replaceRegexString += ((i != 0 ? "|" : "") + @"\s" + match.Groups[1].Value + @"\s");

                    lastMatch = match.Groups[1].Value;
                }
            }

            /*
            if (replaceRegexString != null)
                replaceRegexString += "$";
            //*/

            this.replacementsRegex = new Regex(replaceRegexString);
        }

        override public string replace(string text)
        {
            string result = " " + text + " ";
            MatchCollection matchCollection = this.replacementsRegex.Matches(text);

            HashSet<string> matchedWords = new HashSet<string>();

            foreach (Match match in matchCollection)
            {
                matchedWords.Add(match.Value);
            }

            foreach (string matchedWord in matchedWords)
            {
                result = Regex.Replace(result, matchedWord, " " + ((string)this.replacements[matchedWord.Trim()]) + " ");
            }

            return (result.Trim());
        }
    }

    public class NullLemmatizer : Lemmatizer
    {
        public NullLemmatizer()
        {
        }

        override public string replace(string text)
        {
            return (text);
        }
    }
}
