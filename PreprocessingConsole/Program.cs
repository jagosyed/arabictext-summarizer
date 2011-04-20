using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.Utils;
using System.IO;
using TextSummarizationAlgos.Algorithms.UltraSummarization;

namespace PreprocessingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0].Equals("-idf"))
            {
                string docsFolder = @"D:\Files\College\Advanced AI\Data Sets\CNNArabic2\Dest2\";
                string idfFile = @"IDF2.txt";

                preprocessIDF(docsFolder, idfFile);
            }
            else if (args[0].Equals("-bigram"))
            {
                //string docsFolder = @"D:\Files\College\Advanced AI\Data Sets\UltraSummarization";
                string docsFolder = @"D:\Files\College\Advanced AI\Data Sets\CNNArabic2\Dest2\";
                string bigramFilepath = @"D:\Files\College\Advanced AI\Code\TextSummarizationAlgos\Files\BIGRAM.txt";

                preprocessBigramModel(docsFolder, bigramFilepath);
            }
        }

        private static void preprocessIDF(string docsFolder, string idfFile)
        {
            IDF idf = IDF.IDFGenerator.fromFiles(Directory.GetFiles(docsFolder, "*.txt", SearchOption.TopDirectoryOnly));

            idf.toFile(idfFile);
        }

        private static void preprocessBigramModel(string docsFolder, string bigramFilepath)
        {
            UltraSummarizationPreprocessor preprocessor = new UltraSummarizationPreprocessor();

            preprocessor.preprocessLanguageModel(Directory.GetFiles(docsFolder, "*.txt", SearchOption.TopDirectoryOnly), bigramFilepath);
        }
    }
}
