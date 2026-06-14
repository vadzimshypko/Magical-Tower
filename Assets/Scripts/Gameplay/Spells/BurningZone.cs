using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.Gameplay.Spells
{
    public class BurningZone : MonoBehaviour
    {
        [SerializeField]
        private Transform floor;
        [SerializeField]
        private ParticleSystem particles;
    
        public void Init(float radius, float duration)
        {
            floor.localScale = new Vector3(radius * 2f , 0.1f, radius * 2f);

            var main = particles.main;
            main.duration = duration;

            var shape = particles.shape;
            shape.radius = radius;

            PlayEffect(duration).Forget();
        }

        private async UniTask PlayEffect(float duration)
        {
            particles.gameObject.SetActive(true);
            particles.Play();
            await UniTask.WaitForSeconds(duration);
            Destroy(gameObject);
        }
    }
}
