using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using TextSummarizationAlgos.DocumentProcessing;
using Utils;

namespace TextSummarizationAlgos.DocumentProcessing
{
    class KunaDocumentProcessor : DocumentProcessor
    {
        override public Document process(string docText)
        {
            Document doc = new Document();

            doc.originalText = docText;

            // Begin : Preprocessing

            // Remove Extra Characters and Words.
            docText = Regex.Replace(docText, "\r\n([^\r\n])", "$1", RegexOptions.Multiline);
            docText = Regex.Replace(docText, @"\(يتبع\)", "");
            docText = Regex.Replace(docText, @"\(النهاية\)(.*)", "", RegexOptions.Multiline | RegexOptions.Singleline);

            // Normalize Characters
            docText = Regex.Replace(docText, "أ|إ", "ا");
            docText = Regex.Replace(docText, "ى", "ي");

            doc.fullText = docText;
            // End : Preprocessing

            string match = Regex.Match(docText, @"\s(.*/)+.*\s").Value;

            string[] splits = Regex.Split(docText, @"\.<br>|\.|\r\n", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            //debugClipboard(splits);

            ArrayList sentences = new ArrayList();

            foreach (string split in splits)
            {
                string text = split;
                Sentence sent = new Sentence();

                sent.fullText = text;

                text = Regex.Replace(text, @"^\s+", "");
                text = Regex.Replace(text, @"\s+$", "");

                // Remove Stop Words
                text = StopWordsHandler.getInstance(Conf.STOP_WORDS_PATH).remove(text);

                string[] wordSplits = Regex.Split(text, @"\s+", RegexOptions.IgnorePatternWhitespace);
                //sent.words = wordSplits;

                ArrayList words = new ArrayList();
                Hashtable wordsCount = new Hashtable();
                foreach (string word in wordSplits)
                {
                    words.Add(word);
                    if (wordsCount[word] == null)
                        wordsCount[word] = 1;
                    else
                        wordsCount[word] = (int)wordsCount[word] + 1 ;
                }
                sent.words = words;
                sent.wordsCount = wordsCount;

                // is it a title
                if (split == splits[0] && !Regex.IsMatch(text, @"(.*)كونا(.*)"))
                    doc.title = sent;
                else
                    sentences.Add(sent);
            }

            doc.sentences = sentences;

            return doc;
        }
    }
}
