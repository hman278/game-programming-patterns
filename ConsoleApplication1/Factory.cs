public interface IEntity
{
    public string ProductName { get; set; }

    public void Initialize(string Name);
}

public abstract class EntityFactory : MonoBehavior
{
    public abstract IEntity CreateEntity(Transform transform, string name);
}

public class Enemy : MonoBehavior, IEntity
{
    [SerializeField] private string _name = "";

    public void Initialize(string name)
    {
        _name = name;
        
        // The rest of the intialization code ...
    }
}

public class EnemyFactory : EntityFactory
{
    [SerializeField] private Enemy _enemyPrefab;

    public override IEntity CreateEntity(Transform transform, string name)
    {
        GameObject instance = Instantiate(_enemyPrefab, transform);
        Enemy enemy = instance.GetComponent<Enemy>();

        enemy.Initialize(name);

        return enemy;
    }
}

public class MyClass
{
    private void Start()
    {
        EnemyFactory
    }
}
