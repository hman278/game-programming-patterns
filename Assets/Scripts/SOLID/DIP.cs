// Dependency inversion principle, loose coupling and high cohesion between classes, less dependencies
using UnityEngine;

namespace SOLID
{
    public interface ISwitchable
    {
        public bool IsActive { get; }

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
}

