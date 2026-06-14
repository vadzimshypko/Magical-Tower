using Scripts.Gameplay.Spells;
using UnityEngine;

namespace Scripts.Configs
{
    public abstract class SpellConfig : ScriptableObject
    {
        public string title;
        public float cooldown;
        public ProjectileView projectilePrefab;
        public float projectileSpeed;
        public int projectileDamage;
    }
}