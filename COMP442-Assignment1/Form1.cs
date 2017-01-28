using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMP442_Assignment1.Lexical;

namespace COMP442_Assignment1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LexicalAnalyzer analyzer = new LexicalAnalyzer();

            var code = textBox1.Text;

            var tokens = analyzer.Tokenize(code);

            textBox2.Text = string.Join(System.Environment.NewLine, tokens.Where(x => !x.isError()).Select(x => x.getName()).ToArray());
            textBox3.Text = string.Join(System.Environment.NewLine, tokens.Where(x => x.isError()).Select(x => x.getName()).ToArray());

        }
    }
}
