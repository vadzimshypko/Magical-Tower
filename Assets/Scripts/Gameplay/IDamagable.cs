using System;

namespace Scripts.Gameplay
{
    public interface IDamagable
    {
        event Action<IDamagable> Damaged;
        event Action<IDamagable> Death;

        bool IsAlive { get; }
        int Health { get; }
        void TakeDamage(int damage);
    }
}