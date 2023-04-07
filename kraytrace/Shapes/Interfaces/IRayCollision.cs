using System;
using kraytrace;
using kraytrace.Shapes;
namespace kraytrace.Shapes.Interfaces
{
	public interface IRayCollision
	{
		public HitRecord? Intersected(Ray r, float tMin, float tMax);
	}
}

