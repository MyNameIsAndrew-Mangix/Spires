namespace Spire.Resources
{
    public interface IDamageable
    {
        void TakeDamage(float attack, float defense);
        void Heal(float amount, float modifier);
    }
}