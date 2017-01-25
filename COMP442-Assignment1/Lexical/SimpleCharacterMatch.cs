using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    class SimpleCharacterMatch : ICharacterMatch
    {
        private char _character;

        public SimpleCharacterMatch(char character)
        {
            _character = character;
        }

        public bool doesCharacterMatch(char character)
        {
            return _character == character;
        }
    }
}
