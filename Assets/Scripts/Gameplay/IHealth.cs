using System;
using UnityEngine;

namespace Scripts.Gameplay
{
    public interface IHealth
    {
        event Action<IHealth, int> Damaged;
        event Action<IHealth> Died;

        bool IsAlive { get; }
        int Health { get; }
        int MaxHealth { get; }
        void TakeDamage(int damage);
        Vector3 Position { get; }
    }
}