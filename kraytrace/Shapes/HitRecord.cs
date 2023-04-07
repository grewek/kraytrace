using System;
using kraytrace.LinearAlgebra;

namespace kraytrace.Shapes
{
	public readonly struct HitRecord
	{
		public readonly Vector3 Position;
		public readonly Vector3 Normal;
		public readonly float TValue;

		public readonly bool FrontFace;

		public HitRecord(Vector3 position, Vector3 direction, float tValue, Vector3 outwardNormal)
		{
			Position = position;
            FrontFace = Vector3.DotProduct(direction, outwardNormal) < 0f;
            Normal = FrontFace ? outwardNormal : -outwardNormal;

			TValue = tValue;
        }
	}
}

