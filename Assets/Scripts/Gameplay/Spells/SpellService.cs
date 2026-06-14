using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Scripts.Gameplay.Spells
{
    public class SpellService : IStartable
    {
        private IEnumerable<ISpell> _spells;
        private Transform _root;
        private SpellButton _spellButton;

        private List<SpellButton> _buttons = new();

        public SpellService(IEnumerable<ISpell> spells, Transform root, SpellButton spellButton)
        {
            _spells = spells;
            _root = root;
            _spellButton = spellButton;
        }

        public void Start()
        {
            CreateButtonForSpells();
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