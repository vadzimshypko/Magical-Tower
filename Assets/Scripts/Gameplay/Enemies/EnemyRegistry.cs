using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Enemies
{
    public class EnemyRegistry
    {
        public event Action AllEnemiesDefeated;

        public bool HasEnemiesLeft => _enimies.Count > 0;

        private readonly List<EnemyView> _enimies = new();
        private readonly List<EnemyView> _visibleEnemies = new();
        private readonly Plane[] _cameraPlanes = new Plane[6];
        
        public EnemyRegistry(EnemyFactory enemyFactory)
        {
            enemyFactory.Spawn += RegisterEnemy;
            enemyFactory.Despawn += UnregisterEnemy;
        }

        public EnemyView GetRandomVisibleEnemy()
        {
            var visibleEnemies = GetAllVisibleEnemies();
            return visibleEnemies.Count == 0 ? null : visibleEnemies[UnityEngine.Random.Range(0, visibleEnemies.Count)];
        }

        public IReadOnlyList<EnemyView> GetAllVisibleEnemies()
        {
            _visibleEnemies.Clear();
            GeometryUtility.CalculateFrustumPlanes(Camera.main, _cameraPlanes);
            foreach (var enemy in _enimies)
            {
                if (enemy != null && enemy.IsVisible(_cameraPlanes))
                {
                    _visibleEnemies.Add(enemy);
                }
            }
            return _visibleEnemies;
        }

        private void RegisterEnemy(EnemyView enemy)
        {
            _enimies.Add(enemy);
        }

        private void UnregisterEnemy(EnemyView enemy)
        {
            _enimies.Remove(enemy);
            if (_enimies.Count == 0)
            {
                AllEnemiesDefeated?.Invoke();
            }
        }
    }
}
