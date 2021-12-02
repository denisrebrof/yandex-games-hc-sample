namespace Levels.domain.model
{
    public class CompleteLevelResult
    {
        public Level CompletedLevel;
        public Level NextLevel;

        public CompleteLevelResult(Level completedLevel, Level nextLevel)
        {
            this.CompletedLevel = completedLevel;
            this.NextLevel = nextLevel;
        }
    }
}