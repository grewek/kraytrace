using System;
using kraytrace.LinearAlgebra;
using kraytrace.Surfaces.Interfaces;

namespace kraytrace.Shapes
{
	public readonly struct HitRecord
	{
		public readonly Vector3 Position;
		public readonly Vector3 Normal;
		public readonly float TValue;
		public readonly Material Material;

		public readonly bool FrontFace;

		public HitRecord(Vector3 position, Vector3 direction, float tValue, Vector3 outwardNormal, Material surfaceMaterial)
		{
			Position = position;
            FrontFace = Vector3.DotProduct(direction, outwardNormal) < 0f;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
			Material = surfaceMaterial;
			TValue = tValue;
        }
	}
}

