using System;
using System.Collections.Generic;

namespace Scripts.Configs
{
    [Serializable]
    public class WaveConfig
    {
        public float DurationInSeconds;
        public float SpawnInterval; 
        public List<EnemySpawnConfig> EnemySpawnOptions;
    }
}