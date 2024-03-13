using UnityEngine;

public static class PoolableObjectFactory
{
    public static T CreatePoolableObject<T>() where T : Component, IPoolable => new GameObject().AddComponent<T>();
}