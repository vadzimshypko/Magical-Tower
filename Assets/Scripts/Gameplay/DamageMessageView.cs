using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Scripts.Gameplay
{
    public class DamageMessageView : MonoBehaviour
    {        
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private TextMeshProUGUI _damageText;
        
        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }
        
        public void Setup(int damage)
        {
            _damageText.text = $"-{damage} HP";
            HideMessage().Forget();
        }
        
        private async UniTask HideMessage()
        {
            var sequence = DOTween.Sequence()
                .Join(_damageText.DOFade(0f, 2f))
                .Join(transform.DOMoveY(transform.position.y + 1.5f, 2f));
                
            await sequence.AsyncWaitForCompletion();
            Destroy(gameObject);
        }
    }
}