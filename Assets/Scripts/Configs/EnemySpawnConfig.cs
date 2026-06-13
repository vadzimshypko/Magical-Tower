using System;
using Scripts.Gameplay;

namespace Scripts.Configs
{
    [Serializable]
    public class EnemySpawnConfig
    {
        public int SpawnWeight;
        public EnemyView EnemyPrefab;
    }
}