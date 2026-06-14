using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]
    public class EnemyConfig : HealthConfig
    {
        public int damage;
        public float minDistanceForAttacking;
        public float cooldown;
    }
}