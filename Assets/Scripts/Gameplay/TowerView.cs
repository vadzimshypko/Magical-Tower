using System;
using Scripts.Configs;
using UnityEngine;
using VContainer;

namespace Scripts.Gameplay
{
    public class TowerView : MonoBehaviour, IDamagable
    {        
        public event Action<IDamagable> Damaged;
        public event Action<IDamagable> Death;
               
        public Transform RootForSpells => rootForSpells;

        public bool IsAlive => Health > 0;
        public int Health { get; private set; }
        
        [SerializeField]
        private Transform rootForSpells;
        [SerializeField]
        private HealthConfig config;
        
        [Inject]
        public void Inject(HealthBarController healthBarController)
        {
            Damaged += x => healthBarController.SetHealth(x.Health, config.maxHealth);
        }

        private void Awake()
        {
            Health = config.maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Damaged?.Invoke(this);
            if (!IsAlive)
            {
                Death?.Invoke(this);
            }
        }
    }
}