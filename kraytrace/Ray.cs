using System;
using kraytrace;
using kraytrace.LinearAlgebra;

namespace kraytrace
{
	public class Ray
	{
		private Vector3 _origin;
		private Vector3 _direction;

		public Vector3 Origin
		{
			get { return _origin; }
		}

		public Vector3 Direction
		{
			get { return _direction; }
		}

		public Ray()
		{
			_origin = new Vector3();
			_direction = new Vector3();
		}

		public Ray(Vector3 origin, Vector3 direction)
		{
			_origin = origin;
			_direction = direction;
		}

		public Vector3 At(float t)
		{
			return _origin + (t * _direction);
		}
	}
}

