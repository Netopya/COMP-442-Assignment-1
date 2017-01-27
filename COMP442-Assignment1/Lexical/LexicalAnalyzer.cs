﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    class LexicalAnalyzer
    {
        IState root;

        HashSet<string> ReservedWords = new HashSet<string> { "and", "not", "or", "if", "then", "else", "for", "class", "int", "float", "get", "put", "return", "program" };

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

            IState err = new SimpleFinalState("Error");

            IState s1 = new SimpleIntermediateState(err);
            err.setRootState(s1);

            IState s42 = new SimpleFinalState(false, "Asterisk", s1);

            IState s41 = new SimpleFinalState(false, "Line comment", s1);
            IState s43 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { new SimpleCharacterMatch((char)10), s41} }, err);

            IState s40 = new SimpleIntermediateState();
            s40.addTransition(new SimpleCharacterMatch((char)13), s43);

            

            IState s39 = new SimpleFinalState(false, "Block comment", s1);
            IState s37 = new SimpleIntermediateState();
            IState s38 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { slash, s39 } }, s37);

            s37.addTransition(asterisk, s38);

            IState s36 = new SimpleFinalState(true, "Slash", s1);
            IState s35 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { asterisk, s37 }, { slash, s40 } }, s36);

            // Brackets
            IState s29 = new SimpleFinalState(false, "Open parenthesis", s1);
            IState s30 = new SimpleFinalState(false, "Close parenthesis", s1);
            IState s31 = new SimpleFinalState(false, "Open curly bracket ", s1);
            IState s32 = new SimpleFinalState(false, "Close curly bracket", s1);
            IState s33 = new SimpleFinalState(false, "Open square bracket", s1);
            IState s34 = new SimpleFinalState(false, "Close square bracket", s1);

            // Equality, greater than or equal signs
            IState s28 = new SimpleFinalState(false, "Greater than or equal", s1);
            IState s27 = new SimpleFinalState(true, "Greater than", s1);
            IState s26 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { equals, s28 } }, s27);
            IState s25 = new SimpleFinalState(false, "Less than or equal", s1);
            IState s24 = new SimpleFinalState(false, "Not equal", s1);
            IState s23 = new SimpleFinalState(true, "Less than", s1);
            IState s22 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { greaterThan, s24 }, { equals, s25} }, s23);
            IState s21 = new SimpleFinalState(false, "Double equals", s1);
            IState s20 = new SimpleFinalState(true, "Equals", s1);
            IState s19 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { equals, s21 } }, s20);

            IState s14 = new SimpleFinalState(false, "Period", s1);
            IState s15 = new SimpleFinalState(false, "Semi-colon", s1);
            IState s16 = new SimpleFinalState(false, "Comma", s1);
            IState s17 = new SimpleFinalState(false, "Plus", s1);
            IState s18 = new SimpleFinalState(false, "Minus", s1);

            // Numbers
            IState s12 = new SimpleFinalState(true, "Float (non-zero)", s1);
            IState s11 = new SimpleFinalState(true, "Float (zero)", s1);
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

            IState s7 = new SimpleFinalState(true, "Integer (non-zero)", s1);
            IState s5 = new SimpleFinalState(true, "Integer (zero)", s1);
            IState s6 = new SimpleIntermediateState(s7);
            s6.addTransition(digit, s6);
            s6.addTransition(period, s8);

            IState s4 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { period, s8 } }, s5);

            // Identifiers
            IState s3 = new SimpleFinalState(true, "Identifier", s1);
            IState s2 = new SimpleIntermediateState(s3);
            s2.addTransition(letters, s2);
            s2.addTransition(digit, s2);
            s2.addTransition(new SimpleCharacterMatch('_'), s2);

            // Main entrypoint
            /*IState s1 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() {
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
            }, err);*/

            s1.addTransition(new ListCharacterMatch(new List<char> { ' ', (char)10 , (char)13 }), s1);
            s1.addTransition(letters, s2);
            s1.addTransition(zero, s4);
            s1.addTransition(nonZero, s6);
            s1.addTransition(period, s14);
            s1.addTransition(new SimpleCharacterMatch(';'), s15);
            s1.addTransition(new SimpleCharacterMatch(','), s16);
            s1.addTransition(new SimpleCharacterMatch('+'), s17);
            s1.addTransition(new SimpleCharacterMatch('-'), s18);
            s1.addTransition(equals, s19);
            s1.addTransition(lessThan, s22);
            s1.addTransition(greaterThan, s26);
            s1.addTransition(new SimpleCharacterMatch('('), s29);
            s1.addTransition(new SimpleCharacterMatch(')'), s30);
            s1.addTransition(new SimpleCharacterMatch('{'), s31);
            s1.addTransition(new SimpleCharacterMatch('}'), s32);
            s1.addTransition(new SimpleCharacterMatch('['), s33);
            s1.addTransition(new SimpleCharacterMatch(']'), s34);
            s1.addTransition(slash, s35);
            s1.addTransition(asterisk, s42);

            root = s1;
        }

        public List<string> Tokenize(string input)
        {
            input += System.Environment.NewLine;

            List <string> tokens = new List<string>();
            IState state = root;
            int count = 0;
            int tokenStart = 0;

            while(count < input.Length)
            {
                char character = input[count];
                state = state.getNextState(character);

                if (state.isFinalState())
                {
                    bool backtrack = state.backTrack();

                    tokens.Add(CheckIdentifier(state, input.Substring(tokenStart, count - tokenStart - (backtrack ? 1 : 0))));

                    state = root;

                    if (backtrack)
                    {
                        tokenStart = count;
                        continue;
                    }

                    

                    tokenStart = count + 1;
                }

                count++;
            }

            return tokens;
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

        private string CheckIdentifier(IState state, string token)
        {
            if(state.tokenName() == "Identifier" && ReservedWords.Contains(token))
            {
                return token;
            }

            return state.tokenName();
        }
    }

    
}
