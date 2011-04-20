using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TextSummarizationAlgos;

namespace Utils
{
    public abstract class Trace
    {
        private static Trace _trace = new TextFileTrace(Conf.TRACE_FILE);

        protected Trace()
        {
        }

        public static void setInstance(Trace trace)
        {
            _trace = trace;
        }

        abstract public void writeText(string text);

        public static void write(string text)
        {
            _trace.writeText(text);
        }
    }

    public class NullTrace : Trace
    {
        public NullTrace()
        {
        }

        override public void writeText(string text)
        {
        }
    }

    public class TextFileTrace : Trace
    {
        private string traceFilePath = null;

        public TextFileTrace(string tracefile)
        {
            this.traceFilePath = tracefile;
        }

        override public void writeText(string text)
        {
            File.AppendAllText(Conf.TRACE_FILE, text + Environment.NewLine, Encoding.Default);
        }
    }
}
