using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Spells
{
    public class SpellService
    {
        private readonly IEnumerable<ISpell> _spells;
        private readonly Transform _root;
        private readonly SpellButton _spellButton;

        private readonly List<SpellButton> _buttons = new();

        public SpellService(IEnumerable<ISpell> spells, Transform root, SpellButton spellButton)
        {
            _spells = spells;
            _root = root;
            _spellButton = spellButton;
            CreateButtonForSpells();
        }

        public void Stop()
        {
            foreach (var button in _buttons)
            {
                Object.Destroy(button.gameObject);
            }
            _buttons.Clear();
        }

        private void CreateButtonForSpells()
        {
            foreach (var spell in _spells)
            {
                var button = Object.Instantiate(_spellButton, _root, false);
                _buttons.Add(button);
                button.Setup(spell);
            }
        }
    }
}