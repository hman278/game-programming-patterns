// Interface segregation principle, no client should be forced to depend on methods it does not use 
// Composition over inheritance

public interface IDamageable
{
    public float Health { get; set; }

    public void TakeDamage();
    public void Die();
}

public interface IMovable
{
    public float MoveSpeed { get; set; }
    public Vector3 Velocity { get; set; }

    public void Move(Vector3 normalizedDirection);
}

public interface IUnitStats
{
    public float Strength { get; set; }
    public float Armor { get; set; }
}

public class EnemyUnit : MonoBehaviour, IDamageable, IMovable, IUnitStats
{
    // Code ...
}