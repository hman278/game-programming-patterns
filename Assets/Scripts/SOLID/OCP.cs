// Open-closed principle, extend the old behavior without modifying it
using UnityEngine;

namespace SOLID
{
    public abstract class Shape 
    {
        public abstract float CalculateArea();
    }

    public class Rectangle : Shape 
    {
        public float Width;
        public float Height;

        public override float CalculateArea()
        {
            return Width * Height;   
        }
    }

    public class Circle : Shape 
    {
        public float Radius;

        public override float CalculateArea()
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
}
