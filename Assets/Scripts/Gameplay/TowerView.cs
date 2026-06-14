using System;
using Scripts.Configs;
using UnityEngine;
using VContainer;

namespace Scripts.Gameplay
{
    public class TowerView : MonoBehaviour, IHealth
    {        
        public event Action<IHealth, int> Damaged;

        public event Action<IHealth> Died;
               
        public Transform RootForSpells => rootForSpells;

        public bool IsAlive => Health > 0;
        public int Health { get; private set; }
        public int MaxHealth => config.maxHealth;
        public Vector3 Position => transform.position;
        
        [SerializeField]
        private Transform rootForSpells;
        [SerializeField]
        private HealthConfig config;
        
        [Inject]
        public void Inject(HealthBarController healthBarController)
        {
            Damaged += (x, _) => healthBarController.SetHealth(x.Health, x.MaxHealth);
        }

        private void Awake()
        {
            Health = config.maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Damaged?.Invoke(this, damage);
            if (!IsAlive)
            {
                Died?.Invoke(this);
            }
        }
    }
}