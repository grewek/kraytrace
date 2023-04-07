using System;
using kraytrace;
namespace kraytrace.LinearAlgebra;

//TODO: This will probably be too slow at some point, can we use SIMD with c# ?
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

	public static Vector3 RandomVector()
	{
		return new Vector3(
			MathHelpers.RandomFloat(),
			MathHelpers.RandomFloat(),
			MathHelpers.RandomFloat()
		);
	}

	public static Vector3 RandomUnitVector(float min, float max)
	{
		return Vector3.Normalize(new Vector3(
			MathHelpers.RandomFloat(min, max),
			MathHelpers.RandomFloat(min, max),
			MathHelpers.RandomFloat(min, max)
		));
	}

	public static Vector3 RandomVectorInHemisphere(Vector3 normal)
	{
		Vector3 unitSphere = Vector3.RandomVectorInUnitSphere();

		if(Vector3.DotProduct(unitSphere, normal) > 0.0f)
		{
			return unitSphere;
		}

		return -unitSphere;
	}

	public static Vector3 RandomVectorInUnitSphere()
	{
		while(true)
		{
			var result = RandomUnitVector(-1f, 1f);
			if (result.LengthSquared() >= 1f) continue;
			return result;
		}
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

	//TODO: This is the hadamard product and it should really be
	//		a method instead of a operator overloading...
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

	public static float DotProduct(Vector3 lhs, Vector3 rhs)
	{
		return lhs[0] * rhs[0] + lhs[1] * rhs[1] + lhs[2] * rhs[2];
	}

	public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
	{
		return new Vector3(
			lhs[1] * rhs[2] - lhs[2] * rhs[1],
			lhs[2] * rhs[0] - lhs[0] * rhs[2],
			lhs[0] * rhs[1] - lhs[1] * rhs[0]
		);
	}

	public static Vector3 Reflect(Vector3 incoming, Vector3 normal)
	{
		return incoming - 2 * DotProduct(incoming, normal) * normal;
	}

	public static Vector3 Normalize(Vector3 v)
	{
		return v / v.Length();
	}

	public override string ToString()
	{
		return _vals[0].ToString() + ' ' + _vals[1].ToString() + ' ' + _vals[2].ToString();
	}

}

