namespace Apocalypse.Entity
{
    public interface IDamageable
    {
        EntityType EntityType { get; }
        
        void Damage(float damage);
    }

    public enum EntityType
    {
        Player,
        Enemy
    }
}