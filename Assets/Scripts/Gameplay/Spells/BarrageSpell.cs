using Scripts.Configs;
using Scripts.Gameplay.Enemies;
using UnityEngine;

namespace Scripts.Gameplay.Spells
{
    public class BarrageSpell : ISpell
    {
        public SpellConfig Config => _barrageConfig;
        
        private readonly EnemyRegistry _enemyRegistry;
        private readonly BarrageConfig _barrageConfig;
        private readonly ProjectileFactory _projectileFactory;

        public BarrageSpell(
            EnemyRegistry enemyRegistry, 
            BarrageConfig barrageConfig, 
            ProjectileFactory projectileFactory)
        {
            _enemyRegistry = enemyRegistry;
            _barrageConfig = barrageConfig;
            _projectileFactory = projectileFactory;
        }
        
        public void CastSpell()
        {
            foreach (var enemy in _enemyRegistry.GetAllVisibleEnemies())
            {
                _projectileFactory.CreateProjectile(_barrageConfig, enemy.transform.position, DamageEnemy);
            }
        }

        private void DamageEnemy(EnemyView enemyView, Vector3 destination)
        {
            if (enemyView != null && enemyView.IsAlive)
            {
                enemyView.TakeDamage(_barrageConfig.projectileDamage);
            }
        }
    }
}