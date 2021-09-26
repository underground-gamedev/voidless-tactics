using System;

namespace Battle
{
    public interface IHealthComponent
    {
        event Action OnDeath;
        event Action OnHealthChanged;
        
        int CurrentHealth { get; }
        int MaxHealth { get; }
        bool IsDead { get; }

        int DealDamage(int damage);
        int Heal(int heal);
    }
}