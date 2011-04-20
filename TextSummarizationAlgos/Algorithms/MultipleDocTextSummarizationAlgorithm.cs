using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TextSummarizationAlgos.Algorithms
{
    public abstract class MultipleDocTextSummarizationAlgorithm : TextSummarizationAlgorithm
    {
        abstract public string generateSummary(ArrayList docs, double compressionRatio);
    }
}
