using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    interface IToken
    {
        string getName();
        void setInfo(string content, int line);
        bool isError();
    }
}
