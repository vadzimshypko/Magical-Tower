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
        private bool _isAttacking;

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
            _isAttacking = false;
            agent.destination = _target.transform.position;
        }
        
        public void Update()
        {
            if (!_isAttacking && _target != null && IsInAttackRange())
            {
                Attack().Forget();
            }
        }

        private async UniTask Attack()
        {
            _isAttacking = true;
            while (_target != null && _target.IsAlive)
            {
                _target.TakeDamage(config.damage);
                await UniTask.WaitForSeconds(config.cooldown);
            }
            _isAttacking = false;

        }

        private bool IsInAttackRange()
        {
            return agent.remainingDistance < config.minDistanceForAttacking;
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