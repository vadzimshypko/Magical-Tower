using System;
using System.Collections.Generic;

namespace Scripts.Configs
{
    [Serializable]
    public class WaveConfig
    {
        public float durationInSeconds;
        public float spawnInterval; 
        public List<EnemySpawnConfig> enemySpawnOptions;
    }
}