using System;
using Scripts.Gameplay.Enemies;

namespace Scripts.Configs
{
    [Serializable]
    public class EnemySpawnConfig
    {
        public int spawnWeight;
        public EnemyView enemyPrefab;
    }
}