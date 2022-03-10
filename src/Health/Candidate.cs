using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class Candidate : Patient
    {
        public enum MatchConfidenceLevel
        {
            NoMatch = -1,
            PossibleMatch = 0,
            Match = 1
        }

        /// <summary>
        /// Returns the match confidence score for this Candidate
        /// </summary>
        public double MatchScore { get; set; }

        /// <summary>
        /// Returns the match confidence level for this Candidate
        /// </summary>
        public MatchConfidenceLevel MatchConfidence { get { return CalculateMatchConfidence(MatchScore); } }


        /// <summary>
        /// Calculate the confidence level that a match is accurate given a 
        /// match score from the the EMPI
        /// </summary>
        /// <param name="score">EMPI confidence score</param>
        /// <returns>EMPI confidence level</returns>
        private static MatchConfidenceLevel CalculateMatchConfidence(double score)
        {
            MatchConfidenceLevel matchConfidence;
            if (score < 9.0)
                matchConfidence = MatchConfidenceLevel.NoMatch;
            else if (score < 15.5)
                matchConfidence = MatchConfidenceLevel.PossibleMatch;
            else
                matchConfidence = MatchConfidenceLevel.Match;

            return matchConfidence;
        }




    }

}
