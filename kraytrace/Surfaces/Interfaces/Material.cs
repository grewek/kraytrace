using System;

using kraytrace.LinearAlgebra;
using kraytrace.Shapes;

namespace kraytrace.Surfaces.Interfaces
{
	public interface Material
	{
		public bool Scatter(Ray inboundRay, HitRecord rec, out Vector3 attenuation, out Ray scatterRay);
	}
}

