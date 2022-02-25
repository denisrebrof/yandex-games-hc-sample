using System;

namespace Levels.domain.model
{
    public class Level
    {
        public long ID;
        public int Number;
        public int Reward = 0;

        public Level(long id, int number, int reward)
        {
            ID = id;
            Number = number;
            Reward = reward;
        }
    }
}