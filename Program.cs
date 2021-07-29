#define SHOW_RAW_SCORES
using System;

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
     *   are then added together to give the TOTAL SCORE (a value between 0.00 and 100.0).
     * Write to the console one line per competitor with the following fields:
     *    Rank#, Name, Country Code, Scores: Total, Run 1, Run 2, Trick 1, Trick 2, Trick 3, Trick 4, Trick 5
     * For example: the first line might read as follows:
     *    1, HORIGOME Yuto, JPN, 8.02, 6.77, 9.03, 0.00, 9.35, 9.50, 9.30, 37.18
     ***/

    class Program
    {
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
                Console.WriteLine($"{e.competitor.name}, {e.competitor.country}, {e.judgeScores[0]}, {e.judgeScores[1]}, {e.judgeScores[2]}, {e.judgeScores[3]}, {e.judgeScores[4]}");
            }
            Console.WriteLine();
#endif

            // TODO - Process the scores to determine the ranked order of competitors final scores
        }


        /**************************************************************************************************/

        private static void InitializeEventStats()
        {
            var rand = new Random(2021);
            const int NUM_ELEMENTS = 7;
            scoreData = new ElementScores[competitors.Length * NUM_ELEMENTS];
            for (int e = 0; e < NUM_ELEMENTS; e++)
            {
                for (int c = 0; c < competitors.Length; c++)
                {
                    var es = new ElementScores(competitors[c]);
                    int r = rand.Next(10, 91);
                    es.judgeScores[0] = r + rand.Next(-10, 11) / 10f;
                    es.judgeScores[1] = r + rand.Next(-10, 11) / 10f;
                    es.judgeScores[2] = r + rand.Next(-10, 11) / 10f;
                    es.judgeScores[3] = r + rand.Next(-10, 11) / 10f;
                    es.judgeScores[4] = r + rand.Next(-10, 11) / 10f;
                    scoreData[c + e * competitors.Length] = es;
                }
            }
        }
    }
}
