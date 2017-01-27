using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    class SimpleIntermediateState : IState
    {
        private readonly Dictionary<ICharacterMatch, IState> _transitions;
        private IState _defaultState;

        public SimpleIntermediateState(Dictionary<ICharacterMatch, IState> transitions, IState defaultState)
        {
            _transitions = transitions;
            _defaultState = defaultState;
        }

        public SimpleIntermediateState(IState defaultState)
        {
            _transitions = new Dictionary<ICharacterMatch, IState>();
            _defaultState = defaultState;
        }

        public SimpleIntermediateState()
        {
            _transitions = new Dictionary<ICharacterMatch, IState>();
            _defaultState = this;
        }

        public void addTransition(ICharacterMatch match, IState state)
        {
            _transitions.Add(match, state);
        }

        public IState getNextState(char character)
        {
            KeyValuePair<ICharacterMatch, IState>? nextStatePair = _transitions.FirstOrDefault(x => x.Key.doesCharacterMatch(character));

            if (nextStatePair.Value.Value != null)
                return nextStatePair.Value.Value;
            else
                return _defaultState;
        }

        public bool isFinalState()
        {
            return false;
        }

        public bool backTrack()
        {
            throw new NotImplementedException();
        }

        public string tokenName()
        {
            throw new NotImplementedException();
        }
    }
}
