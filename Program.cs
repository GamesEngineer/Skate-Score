//#define SHOW_RAW_SCORES
using System;
using System.Diagnostics;

namespace Skate_Score
{
    /***
     * GAME-U Pro.Code programming CHALLENGE
     * 
     * ~~~ Scoring the Olympic Skateboarding Competition ~~~
     * 
     * GOAL: 
     * Calculate the final scores of the competitors' performances,
     * and display them in ranked order (winner first).
     * 
     * PROVIDED:
     * The 'scoreData' array contains fictional score data for the Men's Street Skateboarding Competition.
     * 
     * REQUIREMENTS:
     * Process the score data according to the STREET EVENT SCORING RULES:
     * - Each competitor performs seven elements (2 runs and 5 tricks).
     * - For each element:
     *     Five judges each award a score ranging from 0.0 (worst) to 10.0 (best).
     *     The highest and lowest scores will be discarded.
     *     The average of the other three scores is awarded to the competitor.
     * - The four highest average scores from the seven elements (i.e., discard lowest 3 scores)
     *   are then added together to give the TOTAL SCORE (a value between 0.00 and 40.00).
     * Write to the console one line per competitor with the following fields:
     *    Rank#, Name, Country Code, Scores: Total, Run 1, Run 2, Trick 1, Trick 2, Trick 3, Trick 4, Trick 5
     * For example: the first line might read as follows:
     *    1, HORIGOME Yuto, JPN, 37.18, 8.02, 6.77, 9.03, 0.00, 9.35, 9.50, 9.30
     ***/

    class Program
    {
        public const int NUM_ELEMENTS = 7;

        public static Competitor[] competitors =
        {
            /*0*/ new Competitor("CARO NARVAEZ Angelo", "PER"),
            /*1*/ new Competitor("EATON Jagger", "USA"),
            /*2*/ new Competitor("GIRAUD Aurelien", "FRA"),
            /*3*/ new Competitor("HOEFLER Kelvin", "BRA"),
            /*4*/ new Competitor("HORIGOME Yuto", "JPN"),
            /*5*/ new Competitor("HUSTON Nyjah", "USA"),
            /*6*/ new Competitor("MILOU Vincent", "FRA"),
            /*7*/ new Competitor("RIBEIRO Gustavo", "POR"),
        };

        public static ElementScores[] scoreData;

        static void Main(string[] args)
        {
            InitializeEventStats();

#if SHOW_RAW_SCORES
            Console.WriteLine("RAW SCORES:");
            foreach (var e in scoreData)
            {
                Console.WriteLine($"{e.competitor.name,-20}, {e.competitor.country,3}, {e.judgeScores[0],5:F1}, {e.judgeScores[1],5:F1}, {e.judgeScores[2],5:F1}, {e.judgeScores[3],5:F1}, {e.judgeScores[4],5:F1}");
            }
            Console.WriteLine();
#endif

            // Process the scores to determine the ranked order of competitors final scores

            Result[] results = new Result[competitors.Length];

            for (int c = 0; c < competitors.Length; c++)
            {
                var result = new Result();
                result.competitor = scoreData[c].competitor;
                float lowestScore = float.PositiveInfinity;
                float secondLowestScore = float.PositiveInfinity;
                for (int e = 0; e < NUM_ELEMENTS; e++)
                {
                    var s = scoreData[c + e * competitors.Length];
                    Debug.Assert(s.competitor == result.competitor);
                    float minScore = float.PositiveInfinity;
                    float maxScore = float.NegativeInfinity;
                    float sum = 0f;
                    for (int j = 0; j < s.judgeScores.Length; j++)
                    {
                        float score = s.judgeScores[j];
                        if (score < minScore) minScore = score;
                        if (score > maxScore) maxScore = score;
                        sum += score;
                    }
                    // Toss out the highest and lowest scores, then compute the average
                    sum -= maxScore;
                    sum -= minScore;
                    float averageScore = sum / (s.judgeScores.Length - 2);
                    result.elementScores[e] = averageScore;
                    result.totalScore += averageScore;
                    if (averageScore < lowestScore) lowestScore = averageScore;
                    else if (averageScore < secondLowestScore) secondLowestScore = averageScore;
                }
                // Toss out the 2 lowest element scores
                result.totalScore -= secondLowestScore;
                result.totalScore -= lowestScore;
                results[c] = result;
            }

            Array.Sort(results, (a, b) => Math.Sign(b.totalScore - a.totalScore));

            Console.WriteLine("NAME                , NOC,  TOTAL,   Run1,   Run2, Trick1, Trick2, Trick3, Trick4, Trick5");
            for (int i = 0; i < results.Length; i++)
            {
                var r = results[i];
                Console.WriteLine($"{r.competitor.name,-20}, {r.competitor.country,3}, {r.totalScore,6:F2}, {r.elementScores[0],6:F2}, {r.elementScores[1],6:F2}, {r.elementScores[2],6:F2}, {r.elementScores[3],6:F2}, {r.elementScores[4],6:F2}, {r.elementScores[5],6:F2}, {r.elementScores[6],6:F2}");
            }
            Console.WriteLine();
        }

        /**************************************************************************************************/

        private static void InitializeEventStats()
        {
            var rand = new Random(2021);
            scoreData = new ElementScores[competitors.Length * NUM_ELEMENTS];
            for (int e = 0; e < NUM_ELEMENTS; e++)
            {
                for (int c = 0; c < competitors.Length; c++)
                {
                    var es = new ElementScores(competitors[c]);
                    int r = rand.Next(10, 91);
                    es.judgeScores[0] = (r + rand.Next(-10, 11)) / 10f;
                    es.judgeScores[1] = (r + rand.Next(-10, 11)) / 10f;
                    es.judgeScores[2] = (r + rand.Next(-10, 11)) / 10f;
                    es.judgeScores[3] = (r + rand.Next(-10, 11)) / 10f;
                    es.judgeScores[4] = (r + rand.Next(-10, 11)) / 10f;
                    scoreData[c + e * competitors.Length] = es;
                }
            }
        }
    }
}
