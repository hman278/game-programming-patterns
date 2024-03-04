// Liskov substitution principle, derived classes must be substitutable for their base class
using UnityEngine;

namespace SOLID
{
    public interface ITurnable
    {
        public float TurnSpeed { get; }
        
        public void Turn(float normalizedDirection);
    }

    public class RoadVehicle : MonoBehaviour, ITurnable, IMovable
    {
        public Vector3 Velocity { get; set; }
        public float MoveSpeed { get; set; }
        public float TurnSpeed { get; set; }

        public void Move(Vector3 normalizedDirection)
        {
            // Code ...
        }

        public void Turn(float normalizedDirection)
        {
            // Code ... 
        }
    }

    public class RailVehicle : MonoBehaviour, IMovable
    {
        public float MoveSpeed { get; set; }
        public Vector3 Velocity { get; set; }

        public virtual void Move(Vector3 normalizedDirection)
        {
            // Code ...
        }
    }

    public class Car : RoadVehicle
    {

    }

    public class Train : RailVehicle
    {

    }
}