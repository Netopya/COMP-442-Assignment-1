using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    class LexicalAnalyzer
    {
        public LexicalAnalyzer()
        {
            ICharacterMatch letters = new ListCharacterMatch(generateLetters());
            ICharacterMatch nonZero = new ListCharacterMatch(generateNonZeroes());
            ICharacterMatch digit = new ListCharacterMatch(generateDigits());

            ICharacterMatch zero = new SimpleCharacterMatch('0');
            ICharacterMatch period = new SimpleCharacterMatch('.');
            ICharacterMatch equals = new SimpleCharacterMatch('=');
            ICharacterMatch asterisk = new SimpleCharacterMatch('*');
            ICharacterMatch slash = new SimpleCharacterMatch('/');
            ICharacterMatch greaterThan = new SimpleCharacterMatch('>');
            ICharacterMatch lessThan = new SimpleCharacterMatch('<');
            
            IState err = new SimpleFinalState(false, "Error");

            IState s42 = new SimpleFinalState(false, "Asterisk");
            IState s41 = new SimpleFinalState(false, "Line comment");
            IState s40; // END OF LINE TRANSITION;
            IState s39 = new SimpleFinalState(false, "Block comment");
            IState s38; // END OF FILE TRANSITION;
            IState s37; // END OF FILE TRANSITION;
            IState s36 = new SimpleFinalState(true, "Slash");
            // IState s35 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { asterisk, s37 }, { slash, s40 } }, s36);

            // Brackets
            IState s29 = new SimpleFinalState(false, "Open parenthesis");
            IState s30 = new SimpleFinalState(false, "Close parenthesis");
            IState s31 = new SimpleFinalState(false, "Open curly bracket ");
            IState s32 = new SimpleFinalState(false, "Close curly bracket");
            IState s33 = new SimpleFinalState(false, "Open square bracket");
            IState s34 = new SimpleFinalState(false, "Close square bracket");

            // Equality, greater than or equal signs
            IState s28 = new SimpleFinalState(false, "Greater than or equal");
            IState s27 = new SimpleFinalState(true, "Greater than");
            IState s26 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { equals, s28 } }, s27);
            IState s25 = new SimpleFinalState(false, "Less than or equal");
            IState s24 = new SimpleFinalState(false, "Not equal");
            IState s23 = new SimpleFinalState(true, "Less than");
            IState s22 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { greaterThan, s24 }, { equals, s25} }, s23);
            IState s21 = new SimpleFinalState(false, "Double equals");
            IState s20 = new SimpleFinalState(true, "Equals");
            IState s19 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { equals, s21 } }, s20);

            IState s14 = new SimpleFinalState(false, "Period");
            IState s15 = new SimpleFinalState(false, "Semi-colon");
            IState s16 = new SimpleFinalState(false, "Comma");
            IState s17 = new SimpleFinalState(false, "Plus");
            IState s18 = new SimpleFinalState(false, "Minus");

            // Numbers
            IState s12 = new SimpleFinalState(true, "Float (non-zero)");
            IState s11 = new SimpleFinalState(true, "Float (zero)");
            IState s13 = new SimpleIntermediateState(err);
            IState s10 = new SimpleIntermediateState(err);
            IState s9 = new SimpleIntermediateState(err);
            IState s8 = new SimpleIntermediateState(err);

            s13.addTransition(zero, s13);
            s13.addTransition(nonZero, s10);
            s10.addTransition(nonZero, s10);
            s10.addTransition(zero, s13);
            s9.addTransition(zero, s13);
            s9.addTransition(nonZero, s10);
            s8.addTransition(zero, s9);
            s8.addTransition(nonZero, s10);

            IState s7 = new SimpleFinalState(true, "Integer (non-zero)");
            IState s5 = new SimpleFinalState(true, "Integer (zero)");
            IState s6 = new SimpleIntermediateState(s7);
            s6.addTransition(digit, s6);
            s6.addTransition(period, s8);

            IState s4 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { period, s8 } }, s5);

            // Identifiers
            IState s3 = new SimpleFinalState(true, "Identifier");
            IState s2 = new SimpleIntermediateState(s3);
            s2.addTransition(letters, s2);
            s2.addTransition(digit, s2);
            s2.addTransition(new SimpleCharacterMatch('_'), s2);

            // Main entrypoint
            IState s1 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() {
                {letters, s2 },
                {zero, s4 },
                {nonZero, s6 },
                {period, s14 },
                {new SimpleCharacterMatch(';'), s15 },
                {new SimpleCharacterMatch(','), s16 },
                {new SimpleCharacterMatch('+'), s17 },
                {new SimpleCharacterMatch('-'), s18 },
                {equals, s19 },
                {lessThan, s22 },
                {greaterThan, s26 },
                {new SimpleCharacterMatch('('), s29 },
                {new SimpleCharacterMatch(')'), s30 },
                {new SimpleCharacterMatch('{'), s31 },
                {new SimpleCharacterMatch('}'), s32 },
                {new SimpleCharacterMatch('['), s33 },
                {new SimpleCharacterMatch(']'), s34 },
                // {slash, s35 },
                {asterisk, s42 }
            }, err);

            s1.addTransition(new SimpleCharacterMatch(' '), s1);
        }

        private List<char> generateLetters()
        {
            List<char> letters = new List<char>();

            for (int i = 0; i < 26; i++)
            {
                letters.Add((char)('a' + i));
            }

            for (int i = 0; i < 26; i++)
            {
                letters.Add((char)('A' + i));
            }

            return letters;
        }

        private List<char> generateNonZeroes()
        {
            List<char> digits = new List<char>();

            for (int i = 0; i < 9; i++)
            {
                digits.Add((char)('1' + i));
            }

            return digits;
        }

        private List<char> generateDigits()
        {
            List<char> digits = new List<char>();

            for (int i = 0; i < 10; i++)
            {
                digits.Add((char)('0' + i));
            }

            return digits;
        }
    }

    
}
