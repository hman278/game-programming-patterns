using UnityEngine;

public class BotPooler : MonoBehaviour
{
    [SerializeField, Range(0, 10000)] private int newObjCount;

    private readonly Pool<Bot> _botPool = new();
    
    private void Start()
    {
        _botPool.PoolObjects(FindObjectsByType<Bot>(FindObjectsInactive.Include, FindObjectsSortMode.None));   
        
        uint i = 0;
        while (i < newObjCount)
        {
            Bot newBot = PoolableObjectFactory.CreatePoolableObject<Bot>();
            if (newBot != null)
            {
                _botPool.PoolObject(newBot);
            }
            ++i;
        }
    }

    private void OnApplicationQuit()
    {
        _botPool.Clear();
    }
}
