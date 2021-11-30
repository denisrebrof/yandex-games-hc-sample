using System;

namespace Levels.domain.model
{
    public class Level
    {
        public long ID;
        public int Number;
        public bool CompletedState = false;
        public int Reward = 0;

        public Level(long id, int number, bool completedState, int reward)
        {
            ID = id;
            Number = number;
            CompletedState = completedState;
            Reward = reward;
        }
    }
}