using Scripts.Configs;

namespace Scripts.Gameplay.Spells
{
    public interface ISpell
    {
        void CastSpell();
        SpellConfig Config { get; }
    }
}