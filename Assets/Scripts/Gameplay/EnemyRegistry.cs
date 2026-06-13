using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Gameplay
{
    public class EnemyRegistry
    {
        public event Action AllEnemiesDefeated;

        public bool HasEnemiesLeft => _enimies.Count > 0;
        
        private readonly EnemyFactory _enemyFactory;

        private readonly List<EnemyView> _enimies = new();
        
        public EnemyRegistry(EnemyFactory enemyFactory)
        {
            enemyFactory.Spawn += RegisterEnemy;
            enemyFactory.Despawn += UnregisterEnemy;
        }

        public EnemyView GetRandomVisibleEnemy()
        {
            var visibleEnemies = GetAllVisibleEnemies();
            return visibleEnemies.ElementAt(UnityEngine.Random.Range(0, visibleEnemies.Count()));
        }

        public List<EnemyView> GetAllVisibleEnemies()
        {
            var visibleEnemies= _enimies.Where(enemy => enemy.IsVisible()).ToList();
            return visibleEnemies;
        }

        private void RegisterEnemy(EnemyView enemyView)
        {
            _enimies.Add(enemyView);
        }

        private void UnregisterEnemy(EnemyView enemyView)
        {
            _enimies.Remove(enemyView);
            if (_enimies.Count == 0)
            {
                AllEnemiesDefeated?.Invoke();
            }
        }
    }
}