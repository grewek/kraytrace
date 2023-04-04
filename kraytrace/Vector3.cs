using System;
namespace kraytrace
{
	public struct Vector3
	{
		private float[] _vals;

		public float X { get { return _vals[0]; } }
		public float Y { get { return _vals[1]; } }
		public float Z { get { return _vals[2]; } }

		public float this[int i]
		{
			get { return _vals[i]; }
			set { _vals[i] = value; }
		}

		public Vector3()
		{
			_vals = new float[3] { 0, 0, 0 };
		}
		public Vector3(float x, float y, float z)
		{
			_vals = new float[3] { x, y, z };
		}

		public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				lhs[0] + rhs[0],
				lhs[1] + rhs[1],
				lhs[2] + rhs[2]
			);
		}

		public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				lhs[0] - rhs[0],
				lhs[1] - rhs[1],
				lhs[2] - rhs[2]
			);
		}

		public static Vector3 operator *(Vector3 lhs, Vector3 rhs) {
			return new Vector3(
				lhs[0] * rhs[0],
				lhs[1] * rhs[1],
				lhs[2] * rhs[2]
			);
		}

		public static Vector3 operator *(float scalar, Vector3 rhs)
		{
			return new Vector3(
				scalar * rhs[0],
				scalar * rhs[1],
				scalar * rhs[2]
			);
		}

		public static Vector3 operator *(Vector3 lhs, float scalar)
		{
			return scalar * lhs;
		}

		public static Vector3 operator /(Vector3 lhs, float scalar)
		{
			//TODO: Division by Zero ! Floating Point Comparsion Helper !
			return new Vector3(
				lhs[0] / scalar,
				lhs[1] / scalar,
				lhs[2] / scalar
			);
		}

		public static Vector3 operator -(Vector3 lhs)
		{
			return new Vector3(
				-lhs.X,
				-lhs.Y,
				-lhs.Z
			);
		}

		public float Length()
		{
			//TODO: Is there a method that returns floats instead of doubles ?
			return (float)Math.Sqrt(this.LengthSquared());
		}

		public float LengthSquared()
		{
			return _vals[0] * _vals[0] + _vals[1] * _vals[1] + _vals[2] * _vals[2];
		}

		public float DotProduct(Vector3 lhs, Vector3 rhs)
		{
			return lhs[0] * rhs[0] + lhs[1] * rhs[1] + lhs[2] * rhs[2];
		}

		public Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				lhs[1] * rhs[2] - lhs[2] * rhs[1],
				lhs[2] * rhs[0] - lhs[0] * rhs[2],
				lhs[0] * rhs[1] - lhs[1] * rhs[0]
			);
		}

        public override string ToString()
        {
			return _vals[0].ToString() + ' ' + _vals[1].ToString() + ' ' + _vals[2].ToString();
        }
    }
}

