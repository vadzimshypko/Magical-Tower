using System;
using Scripts.Configs;
using UnityEngine;

namespace Scripts.Gameplay
{
    public class TowerView : MonoBehaviour, IDamagable
    {        
        public event Action<IDamagable> Damaged;
        public event Action<IDamagable> Death;
               
        public Transform RootForSpells => rootForSpells;

        public bool IsAlive => Health > 0;
        public int Health { get; }
        
        [SerializeField]
        private Transform rootForSpells;
        [SerializeField]
        private HealthConfig config;

        public TowerView(HealthBarController healthBarController)
        {
            Damaged += (x) => healthBarController.SetHealth(x.Health, config.maxHealth);
            Health = config.maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }
    }
}