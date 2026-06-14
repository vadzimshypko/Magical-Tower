using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Scripts.Configs;
using Random = UnityEngine.Random;

namespace Scripts.Gameplay.Enemies
{
    public class WavesSpawner
    {
        public event Action<EnemyView> Spawn;
        public event Action<EnemyView> Despawn;
        
        private readonly WavesConfig _wavesConfig;
        private readonly EnemyFactory _enemyFactory;
        
        public WavesSpawner(WavesConfig wavesConfig, EnemyFactory enemyFactory)
        {
            _wavesConfig = wavesConfig;
            _enemyFactory = enemyFactory;
        }

        public async UniTask StartWaves()
        {
            foreach (var wave in _wavesConfig.waves)
            {
                await SpawnWave(wave);
            }
        }

        private async UniTask SpawnWave(WaveConfig wave)
        {
            var currentWaveDuration = 0f;
            var totalWeight = CalculateWeightForWave(wave.enemySpawnOptions);
            while (currentWaveDuration < wave.durationInSeconds)
            {
                var enemyPrefab = ChooseRandomEnemy(wave.enemySpawnOptions, totalWeight);
                SpawnEnemy(enemyPrefab);
                currentWaveDuration += wave.spawnInterval;
                await UniTask.WaitForSeconds(wave.spawnInterval);
            }
        }

        private int CalculateWeightForWave(List<EnemySpawnConfig> enemySpawnConfigs)
        {
            return enemySpawnConfigs.Sum(t => t.spawnWeight);
        }
        
        private EnemyView ChooseRandomEnemy(List<EnemySpawnConfig> enemySpawnConfigs, int totalWeight)
        {
            var point = Random.Range(0, totalWeight);
            foreach (var enemyConfig in enemySpawnConfigs)
            {
                point -= enemyConfig.spawnWeight;
                if (point < 0)
                {
                    return enemyConfig.enemyPrefab;
                }
            }

            return enemySpawnConfigs[-1].enemyPrefab;
        }

        private void SpawnEnemy(EnemyView enemyPrefab)
        {
            var enemyView = _enemyFactory.Create(enemyPrefab);
            Spawn?.Invoke(enemyView);
            enemyView.Death += _ => EnemyDied(enemyView);
        }

        private void EnemyDied(EnemyView enemyView)
        {
            Despawn?.Invoke(enemyView);
        }
    }
}