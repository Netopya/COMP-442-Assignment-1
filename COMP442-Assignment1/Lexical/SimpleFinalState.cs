using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    class SimpleFinalState : IState
    {
        private bool _backTrack;
        private string _tokenName;
        private bool _tokenShowContent;

        public SimpleFinalState(bool backTrack, string tokenName, bool tokenShowContent)
        {
            this._backTrack = backTrack;
            this._tokenName = tokenName;
            _tokenShowContent = tokenShowContent;
        }

        public SimpleFinalState(string tokenName)
        {
            _backTrack = false;
            this._tokenName = tokenName;
            _tokenShowContent = false;
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

        public bool isFinalState()
        {
            return true;
        }

        public IToken token()
        {
            if(_tokenName == "Identifier")
            {
                return new IndentifierToken();
            }
            else if(_tokenName == "Error")
            {
                return new ErrorToken();
            }
            else
            {
                return new SimpleToken(_tokenName, _tokenShowContent);
            }
        }
    }
}
