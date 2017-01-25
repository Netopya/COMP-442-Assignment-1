using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    interface IState
    {
        IState getNextState(char character);
        void addTransition(ICharacterMatch match, IState state);
    }
}
