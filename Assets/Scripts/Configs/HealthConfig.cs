using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "ScriptableObjects/HealthConfig")]
    public class HealthConfig : ScriptableObject
    {
        public int maxHealth;
    }
}