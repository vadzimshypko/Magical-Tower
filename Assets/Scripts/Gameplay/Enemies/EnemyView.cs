using System;
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
        private EnemyConfig enemyConfig;
        
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
            Health = enemyConfig.maxHealth;
            _target = towerView;
            agent.destination = _target.transform.position;
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