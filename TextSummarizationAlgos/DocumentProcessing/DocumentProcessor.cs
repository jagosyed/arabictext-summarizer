using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using TextSummarizationAlgos.DocumentProcessing;
using Utils;
using TextSummarizationAlgos.Utils;

namespace TextSummarizationAlgos.DocumentProcessing
{
    public class DocumentProcessor
    {
        virtual public Document process(string docText)
        {
            Document doc = new Document();

            doc.originalText = docText;

            // Begin : Preprocessing

            // Remove Extra Characters and Words.
            /*
            docText = Regex.Replace(docText, "\r\n([^\r\n])", "$1", RegexOptions.Multiline);
            docText = Regex.Replace(docText, @"\(يتبع\)", "");
            docText = Regex.Replace(docText, @"\(النهاية\)(.*)", "", RegexOptions.Multiline | RegexOptions.Singleline);
            //*/

            // Normalize Characters
            docText = Regex.Replace(docText, "أ|إ", "ا");
            docText = Regex.Replace(docText, "ى", "ي");

            doc.fullText = docText;
            // End : Preprocessing

            //string match = Regex.Match(docText, @"\s(.*/)+.*\s").Value;

            string[] splits = Regex.Split(docText, @"\.<br>|\.|\r\n", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            //debugClipboard(splits);

            ArrayList sentences = new ArrayList();

            foreach (string split in splits)
            {
                string text = split;

                if (text == null)
                    continue;

                if (text.Trim().Equals(""))
                    continue;

                Sentence sent = new Sentence();

                sent.fullText = text;

                text = Regex.Replace(text, @"^\s+", "");
                text = Regex.Replace(text, @"\s+$", "");

                // Remove Stop Words
                text = StopWordsHandler.getInstance(Conf.STOP_WORDS_PATH).remove(text);

                // Lemmatizer
                /*
                Trace.write("Before lemmatization");
                Trace.write(text);
                //*/
                text = Lemmatizer.getInstance(Conf.LEMMATIZATION_WORDS_PATH).replace(text);
                /*
                Trace.write("After lemmatization");
                Trace.write(text);
                //*/

                string[] wordSplits = Regex.Split(text, @"\s+", RegexOptions.IgnorePatternWhitespace);

                //sent.words = wordSplits;

                ArrayList words = new ArrayList();
                Hashtable wordsCount = new Hashtable();
                Regex validWordRegex = new Regex(@"[\u0600-\u06FF\u0750-\u076D]", RegexOptions.Compiled);
                Regex toRemove = new Regex(@"[0-9\u066B\u066C\u060C]", RegexOptions.Compiled);
                int sentOrder = 0;

                foreach (string word in wordSplits)
                {
                    if (!validWordRegex.IsMatch(word))
                        continue;

                    string afterRemoval = toRemove.Replace(word, "");

                    if (afterRemoval.Length < 2)
                        continue;

                    words.Add(afterRemoval);
                    if (wordsCount[afterRemoval] == null)
                        wordsCount[afterRemoval] = 1;
                    else
                        wordsCount[afterRemoval] = (int)wordsCount[afterRemoval] + 1;
                }
                sent.words = words;
                sent.wordsCount = wordsCount;

                // is it a title
                // Compare references not values
                if ((object)split == (object)splits[0])
                    doc.title = sent;
                else
                {
                    sent.order = ++sentOrder;
                    sentences.Add(sent);
                }
            }

            doc.sentences = sentences;

            return doc;
        }
    }
}