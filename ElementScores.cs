namespace Skate_Score
{
    class ElementScores
    {
        public Competitor competitor;
        public float[] judgeScores;
        public ElementScores(Competitor competitor)
        {
            this.competitor = competitor;
            this.judgeScores = new float[5];
        }
    }
}
