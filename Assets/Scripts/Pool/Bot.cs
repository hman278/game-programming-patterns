using UnityEngine;

public class Bot : MonoBehaviour, IPoolable
{
    public void Initialize() { }
    public void Uninitialize() { }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
