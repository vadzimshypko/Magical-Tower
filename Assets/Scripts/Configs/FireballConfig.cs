using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "FireballConfig", menuName = "ScriptableObjects/Spell/Fireball")]
    public class FireballConfig : SpellConfig
    {
        public float radius;
        public float duration;
        public int damageByBurningPerSecond;
    }
}