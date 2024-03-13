public interface IPoolable
{
    public abstract void Initialize();
    public abstract void Uninitialize();
    public void SetActive(bool active);
}