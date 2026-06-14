using System;
using Cysharp.Threading.Tasks;
using Scripts.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Gameplay.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : MonoBehaviour, IHealth
    {
        public event Action<IHealth, int> Damaged;
        public event Action<IHealth> Died;

        public int Health { get; private set; }
        public int MaxHealth => config.maxHealth;
        public bool IsAlive => Health > 0;
        public Vector3 Position =>  transform.position;

        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField] 
        private Renderer rendererComponent;
        [SerializeField]
        private EnemyConfig config;
        
        private TowerView _target;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Damaged?.Invoke(this, damage);
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void Init(TowerView towerView)
        {
            Health = config.maxHealth;
            _target = towerView;
            agent.destination = _target.transform.position;
            WaitUntilReachedTower().Forget();
        }

        private async UniTask WaitUntilReachedTower()
        {
            await UniTask.WaitUntil(IsInAttackRange);

            Attack().Forget();
        }
     

        private async UniTask Attack()
        {
            while (this is { IsAlive: true } && _target is {IsAlive: true })
            {
                _target.TakeDamage(config.damage);
                await UniTask.WaitForSeconds(config.cooldown);
            }
        }

        private bool IsInAttackRange()
        {
            return this != null && Vector3.Distance(transform.position, _target.transform.position) < config.minDistanceForAttacking;
        }

        private void OnDestroy()
        {
            Died?.Invoke(this);
        }

        public bool IsVisible()
        {
            return rendererComponent.isVisible;
        }
    }
}