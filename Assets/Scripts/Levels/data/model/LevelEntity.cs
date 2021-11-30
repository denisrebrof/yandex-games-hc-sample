using System;
using UnityEngine;

namespace Levels.data.model
{
    [Serializable]
    public class LevelEntity
    {
        public int reward = 0;
        public GameObject scenePrefab;

        public LevelEntity(int reward, GameObject scenePrefab)
        {
            this.reward = reward;
            this.scenePrefab = scenePrefab;
        }
    }
}