using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                SetupInstance();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        RemoveDuplicates();
    }

    private static void SetupInstance()
    {
        _instance = (T)FindObjectOfType(typeof(T));

        if (!_instance)
        {
            GameObject newInstance = new()
            {
                name = typeof(T).Name
            };
            
            _instance = newInstance.AddComponent<T>();
            DontDestroyOnLoad(newInstance);
        }
    }

    private void RemoveDuplicates()
    {
        if (!_instance)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}