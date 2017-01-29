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
    /*
        The main driver class for the lexical analyzer.
        Code inputed into the textbox is sent to the lexical analyzer for
        tokenization and the output is shown in the appropriate textbox

        For COMP 442 Assignment 1, Michael Bilinsky 26992358
    */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The "Compile!" button
        private void button1_Click(object sender, EventArgs e)
        {
            LexicalAnalyzer analyzer = new LexicalAnalyzer();

            var code = textBox1.Text;

            var tokens = analyzer.Tokenize(code);

            // Seperate the correct and error output
            textBox2.Text = string.Join(System.Environment.NewLine, tokens.Where(x => !x.isError()).Select(x => x.getName()).ToArray());
            textBox3.Text = string.Join(System.Environment.NewLine, tokens.Where(x => x.isError()).Select(x => x.getName()).ToArray());
        }
    }
}
