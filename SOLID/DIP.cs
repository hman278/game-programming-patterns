// Depenency inversion principle, loose coupling and high cohesion between classes, less dependencies

public interface ISwitchable
{
    public bool IsActive { get; set; }

    public void SetActive(bool isActive);
}

public class Switch : MonoBehaviour
{
    public ISwitchable Client;

    public void Toggle()
    {
        Client.SetActive(!Client.IsActive); 
    }
}

public class Door : MonoBehaviour, ISwitchable
{
    public bool IsActive { get; private set; }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}
