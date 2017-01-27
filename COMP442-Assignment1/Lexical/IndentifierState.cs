using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    class IndentifierState : IState
    {
        HashSet<string> ReservedWords = new HashSet<string> { "and", "not", "or", "if", "then", "else", "for", "class", "int", "float", "get", "put", "return", "program" };

        public void addTransition(ICharacterMatch match, IState state)
        {
            throw new NotImplementedException();
        }

        public bool backTrack()
        {
            return true;
        }

        public IState getNextState(char character)
        {
            return null;
        }

        public bool isFinalState()
        {
            return true;
        }

        public void setRootState(IState state)
        {
            throw new NotImplementedException();
        }

        public IToken token()
        {
            throw new NotImplementedException();
        }
    }
}
