using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Component, IPoolable
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
    
    public T Pop()
    {
        if (PooledObjects.Count == 0)
        {
            return PoolableObjectFactory.CreatePoolableObject<T>();
        }
        
        T poolableObject = PooledObjects.Pop();
        poolableObject.SetActive(true);

        _size--;
        
        return poolableObject;
    }

    public void Push(T poolableObject)
    {
        PooledObjects.Push(poolableObject);
        poolableObject.SetActive(false);
        _size++;
    }

    public void Clear()
    {
        PooledObjects.Clear();
    }
}

