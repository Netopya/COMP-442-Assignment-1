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
        private IState _root;

        public SimpleFinalState(bool backTrack, string tokenName, IState root)
        {
            this._backTrack = backTrack;
            this._tokenName = tokenName;
            _root = root;
        }

        public SimpleFinalState(string tokenName)
        {
            _backTrack = false;
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
            return _root;
        }

        public bool isFinalState()
        {
            return true;
        }

        public string tokenName()
        {
            return _tokenName;
        }

        public void setRootState(IState state)
        {
            _root = state;
        }
    }
}
