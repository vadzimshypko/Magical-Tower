using Scripts.Configs;
using Scripts.Gameplay.Enemies;
using UnityEngine;

namespace Scripts.Gameplay.Spells
{
    public class FireballSpell : ISpell
    {
        public SpellConfig Config => _fireballConfig;
        
        private readonly EnemyRegistry _enemyRegistry;
        private readonly FireballConfig _fireballConfig;
        private readonly ProjectileFactory _projectileFactory;

        public FireballSpell(
            EnemyRegistry enemyRegistry, 
            FireballConfig fireballConfig, 
            ProjectileFactory projectileFactory)
        {
            _enemyRegistry = enemyRegistry;
            _fireballConfig = fireballConfig;
            _projectileFactory = projectileFactory;
        }
        
        public void CastSpell()
        {
            var target = _enemyRegistry.GetRandomVisibleEnemy();
            if (target == null)
            {
                return;
            }
            
            _projectileFactory.Create(_fireballConfig, target.transform.position, ExplodeFireball);
        }

        private void ExplodeFireball(EnemyView enemyView, Vector3 position)
        {
            //
        }
    }

}