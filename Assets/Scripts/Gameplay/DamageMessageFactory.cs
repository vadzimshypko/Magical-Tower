using UnityEngine;

namespace Scripts.Gameplay
{
    public class DamageMessageFactory
    {
        private readonly DamageMessageView _damageMessagePrefab;

        public DamageMessageFactory(DamageMessageView damageMessagePrefab)
        {
            _damageMessagePrefab = damageMessagePrefab;
        }
        
        public DamageMessageView Create(int damage, Vector3 position)
        {
            var damageMessageView = Object.Instantiate(_damageMessagePrefab, position, Quaternion.identity);
            damageMessageView.Setup(damage);
            return damageMessageView;
        }
    }
}