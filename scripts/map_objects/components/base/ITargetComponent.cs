using System.Threading.Tasks;

public interface ITargetComponent
{
    Task TakeDamage(int damage);
    Task TakeHeal(int heal);
}