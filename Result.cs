using System;
using System.Collections.Generic;
using System.Text;

namespace Skate_Score
{
    class Result
    {
        public Competitor competitor;
        public float totalScore;
        public float[] elementScores = new float[Program.NUM_ELEMENTS];
    }
}
