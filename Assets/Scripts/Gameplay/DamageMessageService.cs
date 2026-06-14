using Scripts.Gameplay.Enemies;
using VContainer.Unity;

namespace Scripts.Gameplay
{
    public class DamageMessageService : IInitializable
    {
        private readonly DamageMessageFactory _damageMessageFactory;
        private readonly TowerView _towerView;
        private readonly EnemyFactory _enemyFactory;
        
        public DamageMessageService(
            DamageMessageFactory damageMessageFactory,
            TowerView towerView,
            EnemyFactory enemyFactory)
        {
            _damageMessageFactory = damageMessageFactory;
            _towerView = towerView;
            _enemyFactory = enemyFactory;
        }
        
        public void Initialize()
        {
            _towerView.Damaged += ShowDamageMessage;
            _towerView.Died += Unsubscribe;

            _enemyFactory.Spawn += SubscribeOnShowingDamageMessage;
        }
        
        private void ShowDamageMessage(IHealth health, int damage)
        {
            _damageMessageFactory.Create(damage, health.Position);
        }

        private void SubscribeOnShowingDamageMessage(IHealth health)
        {
            health.Damaged += ShowDamageMessage;
            health.Died += Unsubscribe;
        }

        private void Unsubscribe(IHealth health)
        {
            health.Damaged -= ShowDamageMessage;
            health.Died -= Unsubscribe;
        }
    }
}