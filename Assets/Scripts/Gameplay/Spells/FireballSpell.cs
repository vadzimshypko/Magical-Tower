using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Scripts.Configs;
using Scripts.Gameplay.Enemies;
using UnityEngine;

namespace Scripts.Gameplay.Spells
{
    public class FireballSpell : ISpell
    {
        private const string EnemiesLayerName = "Enemies";
        
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
            
            _projectileFactory.CreateProjectile(_fireballConfig, target.transform.position, ExplodeFireball);
        }

        private void ExplodeFireball(EnemyView enemyView, Vector3 position)
        {
            DealDamageInRadius(position, _fireballConfig.projectileDamage);

            var positionOnFloor = new Vector3(position.x, 0, position.z);
            _projectileFactory.CreateZoneEffect(
                _fireballConfig.burningZonePrefab,
                positionOnFloor,
                _fireballConfig.radius,
                _fireballConfig.duration); 
            Burn(position).Forget();
        }

        private async UniTask Burn(Vector3 position)
        {
            var time = 0;
            while (time < _fireballConfig.duration)
            {
                await UniTask.WaitForSeconds(1f);
                time++;
                DealDamageInRadius(position, _fireballConfig.damageByBurningPerSecond);
            }
        }

        private void DealDamageInRadius(Vector3 position, int damage)
        {
            var colliders = Physics.OverlapSphere(
                position,
                _fireballConfig.radius,
                LayerMask.GetMask(EnemiesLayerName),
                QueryTriggerInteraction.Ignore);

            var damagedEnemies = new HashSet<EnemyView>();
            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponentInParent<EnemyView>();
                if (enemy == null || !enemy.IsAlive || !damagedEnemies.Add(enemy))
                {
                    continue;
                }

                enemy.TakeDamage(damage);
            }
        }
    }

}
