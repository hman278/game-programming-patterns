using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public string EntityName { get; set; }

    public void Initialize();
}

public abstract class EntityFactory : MonoBehaviour
{ 
    public abstract IEntity CreateEntity(Transform entityTransform, string entityName);
}

public class Enemy : MonoBehaviour, IEntity
{
    public string EntityName { get; set; }

    public void Initialize()
    {
        // Code ...
    }
}

public class EnemyFactory : EntityFactory
{
    public static EnemyFactory Instance;
    
    [SerializeField] private Enemy enemyPrefab;
    
    private readonly Dictionary<Guid, IEntity> _entities = new Dictionary<Guid, IEntity>();

    private void Awake()
    {
        Instance = this;
    }

    public override IEntity CreateEntity(Transform entityTransform, string entityName)
    {
        IEntity enemy = Instantiate(enemyPrefab, transform);
        enemy.Initialize();
        
        _entities.Add(Guid.NewGuid(), enemy);

        return enemy;
    }

    public IEntity GetEntityByGuid(Guid guid)
    {
        return _entities[guid];
    }

    public void RemoveEntityByGuid(Guid guid)
    {
        _entities.Remove(guid);
    }
}

public class EnemySpawner : MonoBehaviour
{
    private void Start()
    {
        IEntity newEnemy = EnemyFactory.Instance.CreateEntity(transform, "Enemy_01");
    }
}
