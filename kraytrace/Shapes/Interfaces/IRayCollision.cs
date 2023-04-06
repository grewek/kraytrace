using System;
using kraytrace;
using kraytrace.Shapes;
namespace kraytrace.Shapes.Interfaces
{
	public interface IRayCollision
	{
		public HitRecord Record { get; }
		public bool Intersected(Ray r, float tMin, float tMax);
	}
}

