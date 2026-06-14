using UnityEngine;

namespace Scripts.Gameplay
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform damageRect;

        public void SetHealth(int currentHealth, int maxHealth)
        {
            var hpPercent = currentHealth * 1f / maxHealth;
            damageRect.anchorMin = new Vector2(hpPercent, damageRect.anchorMin.y);
        }
    }
}