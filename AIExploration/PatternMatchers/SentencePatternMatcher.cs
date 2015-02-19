// thinking through The ideas I heard on pattern matchers:
//  1. They are regression-like functions of the form Y = C + B1F1 + ... + e
//  2. They compare Y against a threshold that can move up and down via a feedback circuit.
//  3. They accept a range of noise on inputs.  I need a new term for this concept, a bounded inut ( not q variant, but not a constant either. More like a Constant +A -B )
//  4. When they fire, they can either excite or inhibit the next layer.
//  5. I'm drawn to attractor theory rather than linear regression theory.  But, I don't yet know how to model against attractors...
//  6. Outputs cannot be negative numbers.  Any output less than 0 = 0.

// So, what is THIS pattern matcher?
// It looks at sentances as input strings, and determines a score if they're sentances in english or not.
// How?
//  Look for spaces.  
//  Look for puctuation ( , . ? ! )
//  Number of words
//  It might call down to other recognizers -- for example, a verb recognizer or a noun recognizer.
//  Potential other recognizers
//      HTML/XML Tag Recognizer
//      Verb recognizer
//      Noun recognizer.
// Basic Math form:
//  Y = 0 + B1*HasSpaces[10 | 0 -0 +1] + B2*HasPunctuation[10 | 0 -0 +1] + B3*word count[10 | 5 +2 +100] ... - ThresholdScore
// (I'm inveting math.  My notation means, 10 is the beta value, input modal value 0, noise accepted between -0 and +1 from mode )
// I'll seed B1, B2, and B3 with made up values.  Likewise the threshold.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIExploration
{
    class SentencePatternMathcer
    {
        //  Outputs:
        //      0 -- no match
        //      1..infinity match, with "score" of "matchiness"
        // Each of these pattern matchers seem to have input types.  for example, the number of words.  Punctuation.  Words with a type. 
        public int Threshold { get; set; }
        public void increaseThreshold()
        {
            Threshold++;
        }
        public void decreaseThreshold()
        {
            Threshold--;
        }

        public beta betaHasSentencePunctuation { get; set; }
        public beta betaHasSpaces { get; set; }
        public beta betaNumberOfWords { get; set; }
        private string m_strToParse;

        //----------------------------------------------------------------------------------------------
        public SentencePatternMathcer(string inputString)
        {
            m_strToParse = inputString;
            // default the betas/threshold
            betaHasSentencePunctuation = new beta();
            betaHasSpaces = new beta();

            betaNumberOfWords = new beta(); // we won't use the defaults here.
            betaNumberOfWords.betaValue = 10;
            betaNumberOfWords.mode = 5;
            betaNumberOfWords.bottomLimit = 2;
            betaNumberOfWords.topLimit = 100;

            Threshold = 100; // Wild guess.
        }

        //----------------------------------------------------------------------------------------------
        public int RunAndScore()
        {
            int returnScore = 0;
            returnScore = betaHasSentencePunctuation * detectPunctuation() + betaHasSpaces * detectSpaces() + betaNumberOfWords * detectNumberWords();
            return (returnScore);
        }

        //----------------------------------------------------------------------------------------------
        private int detectSpaces()
        {
            if (m_strToParse.Contains(' '))
            {
                return (1);
            }
            return (0);
        }

        //----------------------------------------------------------------------------------------------
        private int detectPunctuation()
        {
            char[] charsToCheck = { ',', '.', '?', '!' };
            foreach (char checkThis in charsToCheck)
            {
                if (m_strToParse.Contains(checkThis))
                {
                    return (1);
                }
            }
            return (0);
        }

        //----------------------------------------------------------------------------------------------
        private int detectNumberWords()
        {
            return (System.Text.RegularExpressions.Regex.Matches(m_strToParse, @"[A-Za-z0-9]+").Count);
        }
    }
}
