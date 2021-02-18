namespace Spire.Resources
{
    public interface IDamageable
    {
        void TakeDamage(float amount);
        void Heal(float amount);
    }
}