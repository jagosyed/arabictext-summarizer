using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using System.Windows.Forms;

namespace UI
{
    class TextBoxTrace : Trace
    {
        private TextBox textBox;

        public TextBoxTrace(TextBox textBox)
        {
            this.textBox = textBox;
        }

        override public void writeText(string text)
        {
            this.textBox.Text += (text + Environment.NewLine);
            this.textBox.SelectionStart = this.textBox.Text.Length;
            this.textBox.ScrollToCaret();
        }
    }
}
