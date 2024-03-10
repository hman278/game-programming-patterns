// This file showcases object pooling and factory working together

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Uninitialize();
}

public class Bot : PoolableObject
{
    public override void Initialize()
    {
        gameObject.SetActive(false);
    }

    public override void Uninitialize()
    {
        gameObject.SetActive(true);
    }
}

public class PoolableObjectFactory : MonoBehaviour
{
    public static PoolableObjectFactory Instance;
    
    [SerializeField] private GameObject objectToPool;

    private void Awake()
    {
        Instance ??= this;
    }

    public bool TryCreatePoolableObject(out PoolableObject poolableObject)
    {
        GameObject obj = Instantiate(objectToPool, transform);
        obj.transform.parent = transform;

        if (obj.TryGetComponent(out PoolableObject objBehaviour))
        { 
            poolableObject = objBehaviour;
            
            return true;
        }
        
        Debug.LogWarning($"Failed to pool {objectToPool}, missing component {nameof(PoolableObject)}");
        
        Destroy(obj);

        poolableObject = null;
        return false;
    }
}

public static class Pool
{
    private static readonly Stack<PoolableObject> PooledObjects = new();
    private static uint _size;

    public static void PoolObject(PoolableObject poolableObject)
    {
        _size++;

        poolableObject.Initialize();
        PooledObjects.Push(poolableObject);
    }
    
    public static void PoolObjects(PoolableObject[] poolableObjects)
    {
        _size += (uint) poolableObjects.Length;

        foreach (PoolableObject poolableObject in poolableObjects)
        {
            poolableObject.Initialize();
            PooledObjects.Push(poolableObject);
        }
    }

    public static List<PoolableObject> FetchObjectsByType<T>() where T : PoolableObject
    {
        return PooledObjects.Where(e => e.GetType() == typeof(T)).ToList();
    }
    
    public static PoolableObject Pop()
    {
        if (PooledObjects.Count == 0)
        {
            if (PoolableObjectFactory.Instance.TryCreatePoolableObject(out PoolableObject obj))
            {
                return obj;
            }
        }
        
        PoolableObject poolableObject = PooledObjects.Pop();
        poolableObject.Uninitialize();
        
        return poolableObject;
    }

    public static void Push(ref PoolableObject poolableObject)
    {
        PooledObjects.Push(poolableObject);
        poolableObject.Uninitialize();
    }

    public static void Clear()
    {
        PooledObjects.Clear();
    }
}

public class PoolManager : MonoBehaviour
{
    [SerializeField, Range(0, 10000)] private int poolableObjectCount; 
    
    private void Start()
    {
        uint i = 0;
        while (i < poolableObjectCount)
        {
            if (PoolableObjectFactory.Instance.TryCreatePoolableObject(out PoolableObject obj))
            {
                Pool.PoolObject(obj);
            }

            ++i;
        }
    }

    private void OnApplicationQuit()
    {
        Pool.Clear();
    }
}

