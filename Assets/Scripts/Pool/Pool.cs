using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Component, IPoolable
{
    private readonly Stack<T> PooledObjects = new();

    public int Size => PooledObjects.Count;

    public void PoolObject(T poolableObject)
    {
        if (poolableObject.GetType() != typeof(T))
        {
            Debug.LogError("Attempted to pool an object of incorrect type.");
            return;
        }

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
        
        foreach (T poolableObject in poolableObjects)
        {
            poolableObject.Initialize();
            poolableObject.SetActive(false);
            PooledObjects.Push(poolableObject);
        }
    }
    
    public T Pop()
    {
        if (PooledObjects.Count == 0)
        {
            return PoolableObjectFactory.CreatePoolableObject<T>();
        }
        
        T poolableObject = PooledObjects.Pop();
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

