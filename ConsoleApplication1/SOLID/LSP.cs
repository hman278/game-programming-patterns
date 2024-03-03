// Liskov substitution principle, derived classes must be substitutable for their base class

public interface ITurnable
{
    public void Turn(float normalizedDirection);
}

public interface IMovable
{
    public void Move(float normalizedDirection);
}

public class RoadVehicle() : MonoBehaviour, ITurnable, IMovable 
{
    public float Speed;
    public float TurnSpeed;

    public virtual void Move(Vector3 normalizedDirection)
    {
        // Code ...
    }

    public virtual void Turn(Vector3 normalizedDirection)
    {
        // Code ... 
    }
}

public class RailVehicle() : MonoBehaviour, IMovable 
{
    public float Speed;

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