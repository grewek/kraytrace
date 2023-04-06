using System;
using kraytrace.LinearAlgebra;

namespace kraytrace.Shapes
{
	public struct HitRecord
	{
		public Vector3 Position;
		public Vector3 Normal;
		public float TValue;

		private bool _frontface;

		public void DetermineFaceNormal(Ray r, Vector3 outwardNormal)
		{
			_frontface = Vector3.DotProduct(r.Direction, outwardNormal) < 0f;
			Normal = _frontface ? outwardNormal : -outwardNormal;
		}
	}
}

