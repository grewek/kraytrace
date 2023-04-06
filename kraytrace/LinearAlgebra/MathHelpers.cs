using System;
namespace kraytrace.LinearAlgebra
{
	public class MathHelpers
	{
		public static float DegreesToRadians(float deg)
		{
			return deg * float.Pi / 180f;
		}

		public static float ClampValue(float v, float min, float max)
		{
			if (v < min) return min;
			if (v > max) return max;

			return v;
		}
	}
}

