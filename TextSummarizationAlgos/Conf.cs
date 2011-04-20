using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSummarizationAlgos.DocumentProcessing;

namespace TextSummarizationAlgos
{
    public class Conf
    {
        //public const string CENTROID_CLUSTERS_PATH = @"D:\Files\College\Advanced AI\Data Sets\CNNArabic2\Clusters\";
        public const string CENTROID_CLUSTERS_PATH = @"..\..\..\Files\CentroidClusters\";

        public const string IDFFILE_PATH = @"..\..\..\Files\IDFDB.txt";

        public const string CUE_WORDS_PATH = @"..\..\..\Files\CueWords.txt";
        public const string STOP_WORDS_PATH = @"..\..\..\Files\StopWords.txt";
        public const string LEMMATIZATION_WORDS_PATH = @"..\..\..\Files\LemmatizationWords.txt";

        private static DocumentProcessor docProcessor = null;

        public const string TRACE_FILE = @"..\..\..\Files\TRACE.txt";

        // Kuna Documents
        /*
        public const string TRAINING_PATH = @"D:\Files\College\Advanced AI\Data Sets\DataSet_Economics_4\Training\";
        public const string TESTING_PATH = @"D:\Files\College\Advanced AI\Data Sets\DataSet_Economics_4\Testing\";

        public static DocumentProcessor getDocumentProcessor()
        {
            if (docProcessor == null)
                docProcessor = new KunaDocumentProcessor();
            return (docProcessor);
        }
        //*/

        // CNN Documents
        //*
        public const string TRAINING_PATH = @"D:\Files\College\Advanced AI\Data Sets\CNNArabic2\Training\";
        public const string TESTING_PATH = @"D:\Files\College\Advanced AI\Data Sets\CNNArabic2\Testing\";
        // Relative Paths
        //public const string TRAINING_PATH = @"Training\";
        //public const string TESTING_PATH = @"Testing\";

        public static DocumentProcessor getDocumentProcessor()
        {
            if (docProcessor == null)
                docProcessor = new DocumentProcessor();
            return (docProcessor);
        }
        //*/

        private Conf()
        {
        }
    }
}
