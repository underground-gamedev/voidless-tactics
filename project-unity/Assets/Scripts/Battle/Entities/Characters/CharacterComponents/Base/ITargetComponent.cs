using System.Collections;

namespace Battle
{
    public interface ITargetComponent
    {
        IEnumerator TakeDamage(int damage);
        IEnumerator TakeHeal(int heal);
    }
}