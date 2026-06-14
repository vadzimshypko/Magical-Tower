using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.Spells
{
    public class SpellButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject cooldownImage;
        [SerializeField]
        private TextMeshProUGUI skillName;
        [SerializeField]
        private Button button;

        private float _cooldown = 3;
        private ISpell _spell;

        public void Setup(ISpell spell)
        {
            var spellConfig = spell.Config;
            _cooldown = spellConfig.cooldown;
            skillName.text = spellConfig.title;
            _spell = spell;
        }

        public void TriggerSpell()
        {
            _spell.CastSpell();
            cooldownImage.SetActive(true);
            button.interactable = false;
            UnblockAfterCooldown().Forget();
        }

        private async UniTask UnblockAfterCooldown()
        {
            await UniTask.WaitForSeconds(_cooldown, cancellationToken: this.GetCancellationTokenOnDestroy());
            cooldownImage.SetActive(false);
            button.interactable = true;
        }
    }
}
