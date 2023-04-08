using System;

using kraytrace.LinearAlgebra;

namespace kraytrace
{
	public class Camera
	{

		private Vector3 _origin;
		private Vector3 _lowerLeftCorner;

        private Vector3 _target;

        private Vector3 _right;
        private Vector3 _up;
        private Vector3 _forward;
		
		public Camera(float verticalFov, float aspectRatio, Vector3 lookFrom, Vector3 lookAt, Vector3 up)
		{
            var theta = MathHelpers.DegreesToRadians(verticalFov);
            var viewportHeight = (float)Math.Tan(theta/2f) * 2f;

            var viewportWidth = aspectRatio * viewportHeight;

            
            _forward = Vector3.Normalize(lookFrom - lookAt);
            var u = Vector3.Normalize(Vector3.CrossProduct(up, _forward));
            var v = Vector3.CrossProduct(_forward, u);

            _origin = lookFrom;
            _right = viewportWidth * u;
            _up = viewportHeight * v;

            _lowerLeftCorner = _origin - _right / 2.0f - _up / 2.0f - _forward;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(_origin, _lowerLeftCorner + u * _right + v * _up - _origin);
        }
	}
}

