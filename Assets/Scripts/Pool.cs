using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour { }

// This object must be instantiated externally
public class PooledObject : PoolableObject
{
    public void Initialize()
    {
        // Code ...
    }
}

public static class Pool
{
    private static Stack<PoolableObject> _pooledObjects;
    public static void InitializePool(int initialSize)
    {
        _pooledObjects = new Stack<PoolableObject>();

        uint i = 0;
        while (i < initialSize)
        {
            PoolableObject obj;
            _pooledObjects.Push(obj);
            
            ++i;
        }
    }
    
    /// <summary>
    /// Returns the first active element from the pool
    /// </summary>
    /// <returns></returns>
    public static PooledObject GetPooledObject()
    {
        if (_pooledObjects.Count == 0)
        {
            return (PooledObject) _init();
        }

        return (PooledObject) _pooledObjects.Pop();
    }
}

public class PoolManager : MonoBehaviour
{
    [SerializeField] private int poolSize;
    [SerializeField] private PoolableObject objectToPool;
    
    private void Start()
    {
        Pool.InitializePool(poolSize);
    }

    // Downside of this implementation is that all the objects will be initialized the same way
    private PoolableObject InitializePoolableObjects()
    {
        return Instantiate(objectToPool, transform);
    }
}

