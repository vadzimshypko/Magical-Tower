using System;
using DG.Tweening;
using Scripts.Gameplay.Enemies;
using UnityEngine;

namespace Scripts.Gameplay.Spells
{
    public class ProjectileView : MonoBehaviour
    {
        public event Action<EnemyView, Vector3> OnCollision;
        
        public void Setup(Vector3 targetPosition, float spellConfigProjectileSpeed, Action<EnemyView, Vector3> onCollision)
        {
            var duration = Vector3.Distance(transform.position, targetPosition) / spellConfigProjectileSpeed;
            transform
                .DOJump(targetPosition, 3f, 1, duration)
                .SetEase(Ease.Linear);
            OnCollision = onCollision;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.TryGetComponent<EnemyView>(out var enemyView) ? enemyView : null;
            OnCollision?.Invoke(enemy, transform.position);
            Destroy(gameObject);
        }
    }
}