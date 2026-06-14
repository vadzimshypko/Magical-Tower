using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public int maxHealth;
    }
}