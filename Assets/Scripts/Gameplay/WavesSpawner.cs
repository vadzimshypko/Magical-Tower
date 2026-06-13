using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Scripts.Configs;
using UnityEngine;

namespace Scripts.Gameplay
{
    public class WavesSpawner
    {
        private WavesConfig _wavesConfig;
        private EnemyFactory _enemyFactory;
        
        public WavesSpawner(WavesConfig wavesConfig, EnemyFactory enemyFactory)
        {
            _wavesConfig = wavesConfig;
            _enemyFactory = enemyFactory;
        }

        public async UniTask StartWaves()
        {
            foreach (var wave in _wavesConfig.Waves)
            {
                Debug.Log("wave " + Time.time);
                await SpawnWave(wave);
            }
        }

        private async UniTask SpawnWave(WaveConfig wave)
        {
            var currentWaveDuration = 0f;
            var totalWeight = CalculateWeightForWave(wave.EnemySpawnOptions);
            while (currentWaveDuration < wave.DurationInSeconds)
            {
                var enemyPrefab = ChooseRandomEnemy(wave.EnemySpawnOptions, totalWeight);
                SpawnEnemy(enemyPrefab);
                currentWaveDuration += wave.SpawnInterval;
                await UniTask.WaitForSeconds(wave.SpawnInterval);
            }
        }

        private int CalculateWeightForWave(List<EnemySpawnConfig> enemySpawnConfigs)
        {
            return enemySpawnConfigs.Sum(t => t.SpawnWeight);
        }
        
        private EnemyView ChooseRandomEnemy(List<EnemySpawnConfig> enemySpawnConfigs, int totalWeight)
        {
            var point = Random.Range(0, totalWeight);
            foreach (var enemyConfig in enemySpawnConfigs)
            {
                point -= enemyConfig.SpawnWeight;
                if (point < 0)
                {
                    return enemyConfig.EnemyPrefab;
                }
            }

            return enemySpawnConfigs[-1].EnemyPrefab;
        }

        private void SpawnEnemy(EnemyView enemyPrefab)
        {
            var enemyView = _enemyFactory.Create(enemyPrefab);
            
            Debug.Log("enemy " + enemyView.name + " " + Time.time);
        }

    }
}