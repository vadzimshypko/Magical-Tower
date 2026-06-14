using System;
using DG.Tweening;
using Scripts.Gameplay.Enemies;
using UnityEngine;

namespace Scripts.Gameplay.Spells
{
    public class ProjectileView : MonoBehaviour
    {
        public event Action<EnemyView, Vector3> OnCollision;

        private Tween _moveTween;
        private bool _isFinished;
        
        public void Setup(Vector3 targetPosition, float spellConfigProjectileSpeed, Action<EnemyView, Vector3> onCollision)
        {
            OnCollision = onCollision;

            var duration = Vector3.Distance(transform.position, targetPosition) / spellConfigProjectileSpeed;
            _moveTween = transform
                .DOJump(targetPosition, 3f, 1, duration)
                .SetEase(Ease.Linear)
                .SetUpdate(UpdateType.Fixed)
                .OnComplete(() => Finish(null, transform.position));
        }

        private void OnTriggerEnter(Collider other)
        {
            Finish(other.GetComponentInParent<EnemyView>(), transform.position);
        }

        private void OnDestroy()
        {
            _moveTween?.Kill();
        }

        private void Finish(EnemyView enemy, Vector3 position)
        {
            if (_isFinished)
            {
                return;
            }

            _isFinished = true;
            _moveTween?.Kill();

            OnCollision?.Invoke(enemy, position);
            Destroy(gameObject);
        }
    }
}
