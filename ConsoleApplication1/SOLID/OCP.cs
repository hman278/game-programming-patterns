// Open-closed principle, extend the old behavior without modifying it

public abstract class Shape 
{
    public abstract float CalculateArea();
}

public class Rectangle : Shape 
{
    public float Width;
    public float Height;

    public override void CalculateArea()
    {
        return Width * Height;   
    }
}

public class Rectangle : Shape 
{
    public float Radius;

    public override void CalculateArea()
    {
        return Radius * Radius * Mathf.PI;
    }
}

public class AreaCalculator 
{
    public float GetArea(Shape shape)
    {
        return shape.CalculateArea();
    }
}