// This file showcases object pooling and factory working together
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Uninitialize();
    
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

public class Bot : PoolableObject
{
    public override void Initialize()
    {
    }

    public override void Uninitialize()
    {
    }
}

public class PoolableObjectFactory : MonoBehaviour
{
    public static PoolableObjectFactory Instance;
    
    [SerializeField] private PoolableObject objectToPool;

    private void Awake()
    {
        Instance ??= this;
    }

    public bool TryCreatePoolableObject(out PoolableObject poolableObject)
    {
        PoolableObject newPoolableObject = Instantiate(objectToPool, transform);
        
        if (newPoolableObject != null)
        { 
            newPoolableObject.transform.parent = transform;
            poolableObject = newPoolableObject;
            
            return true;
        }
        
        Debug.LogWarning($"Failed to pool {objectToPool}, check for validity.");
        
        poolableObject = null;
        return false;
    }
}

public class Pool<T> where T : PoolableObject
{
    private readonly Stack<T> PooledObjects = new();
    private uint _size;

    public void PoolObject(T poolableObject)
    {
        if (poolableObject.GetType() != typeof(T))
        {
            Debug.LogError("Attempted to pool an object of incorrect type.");
            return;
        }
        
        _size++;

        poolableObject.Initialize();
        poolableObject.SetActive(false);
        PooledObjects.Push(poolableObject);
    }
    
    public void PoolObjects(T[] poolableObjects)
    {
        if (poolableObjects.GetType() != typeof(T[]))
        {
            Debug.LogError("Attempted to pool an object of incorrect type.");
            return;
        }
        
        _size += (uint) poolableObjects.Length;

        foreach (T poolableObject in poolableObjects)
        {
            poolableObject.Initialize();
            poolableObject.SetActive(false);
            PooledObjects.Push(poolableObject);
        }
    }
    
    public PoolableObject Pop()
    {
        if (PooledObjects.Count == 0)
        {
            if (PoolableObjectFactory.Instance.TryCreatePoolableObject(out PoolableObject obj))
            {
                return obj;
            }
        }
        
        PoolableObject poolableObject = PooledObjects.Pop();
        poolableObject.SetActive(true);
        
        return poolableObject;
    }

    public void Push(T poolableObject)
    {
        PooledObjects.Push(poolableObject);
        poolableObject.SetActive(false);
    }

    public void Clear()
    {
        PooledObjects.Clear();
    }
}

public class MyClass : MonoBehaviour
{
    [SerializeField, Range(0, 10000)] private int poolableObjectCount;

    private Pool<Bot> _botPool;
    
    private void Start()
    {
        uint i = 0;
        while (i < poolableObjectCount)
        {
            if (PoolableObjectFactory.Instance.TryCreatePoolableObject(out PoolableObject obj))
            {
                _botPool.PoolObject((Bot) obj);
            }

            ++i;
        }
    }

    private void OnApplicationQuit()
    {
        _botPool.Clear();
    }
}
