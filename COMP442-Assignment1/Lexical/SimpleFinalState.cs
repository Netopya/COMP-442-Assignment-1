using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    class SimpleFinalState : IState, IFinalState
    {
        private bool _backTrack;
        private string _tokenName;

        public SimpleFinalState(bool backTrack, string tokenName)
        {
            this._backTrack = backTrack;
            this._tokenName = tokenName;
        }

        public void addTransition(ICharacterMatch match, IState state)
        {
            throw new NotImplementedException();
        }

        public bool backTrack()
        {
            return _backTrack;
        }

        public IState getNextState(char character)
        {
            return null;
        }

        public string tokenName()
        {
            return _tokenName;
        }
    }
}
