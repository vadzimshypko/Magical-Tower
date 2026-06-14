using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Scripts.Gameplay.Enemies
{
    public class EnemyFactory
    {
        public event Action<EnemyView> Spawn;
        public event Action<EnemyView> Despawn;
        
        private Transform _root;
        private TowerView _tower;
        
        private float _radius;

        public EnemyFactory(Transform root, TowerView tower, Collider floor)
        {
            _root = root;
            _tower = tower;

            _radius = CalculateSpawnRadius(floor);
        }

        public EnemyView Create(EnemyView enemyPrefab)
        {
            var towerPosition = _tower.transform.position;
            var spawnPosition = GetRandomSpawnPosition(towerPosition);
            var rotation = Quaternion.LookRotation(towerPosition - spawnPosition);
            var enemyView = Object.Instantiate(enemyPrefab, spawnPosition, rotation, _root);
            enemyView.Init(_tower);
            Spawn?.Invoke(enemyView);
            enemyView.Died += _ => OnEnemyDied(enemyView);
            return enemyView;
        }

        private Vector3 GetRandomSpawnPosition(Vector3 center)
        {
            return center + Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward * _radius;
        }
        
        
        private void OnEnemyDied(EnemyView enemyView)
        {
            Despawn?.Invoke(enemyView);
        }

        private float CalculateSpawnRadius(Collider floor)
        {
            Vector3[] viewportPoints = { new(0f, 0f), new(0f, 1f), new(1f, 1f), new(1f, 0f) };

            var maxDistanceToTower = 1f;
            var towerPosition = _tower.transform.position;
            foreach (var corner in viewportPoints)
            {
                var ray = Camera.main.ViewportPointToRay(corner);
                if (floor.Raycast(ray, out var hit, 1000f))
                {
                    maxDistanceToTower = Math.Max(maxDistanceToTower, Vector3.Distance(towerPosition, hit.point));
                }
            }
            return maxDistanceToTower;
        }
    }
}