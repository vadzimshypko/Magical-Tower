using System;
using Cysharp.Threading.Tasks;
using Scripts.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Gameplay.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : MonoBehaviour, IDamagable
    {
        public event Action<IDamagable> Damaged;
        public event Action<IDamagable> Death;

        public int Health { get; private set; }

        public bool IsAlive => Health > 0;

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
            Damaged?.Invoke(this);
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
            Debug.Log(transform.position + " " +  Vector3.Distance(transform.position, _target.transform.position));
            return this != null && Vector3.Distance(transform.position, _target.transform.position) < config.minDistanceForAttacking;
        }

        private void OnDestroy()
        {
            Death?.Invoke(this);
        }

        public bool IsVisible()
        {
            return rendererComponent.isVisible;
        }
    }
}