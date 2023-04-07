using System;
namespace kraytrace.LinearAlgebra
{
	public static class MathHelpers
	{
		public static Random _random;

		static MathHelpers()
		{
			_random = new Random();
		}

		public static bool CloseToZero(Vector3 v)
		{
			var epsilon = 0.00000001f;

			return (Math.Abs(v[0]) < epsilon) &&
				(Math.Abs(v[1]) < epsilon) &&
				(Math.Abs(v[2]) < epsilon);
		}

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

		public static float RandomFloat()
		{
			return (float)_random.NextDouble();
		}

        public static float RandomFloat(float min, float max)
        {
            return min + (max - min) * MathHelpers.RandomFloat();
        }
    }
}

