using System;

using kraytrace.LinearAlgebra;

namespace kraytrace
{
	public class Camera
	{

		private Vector3 _origin;
		private Vector3 _lowerLeftCorner;

        private Vector3 _right;
        private Vector3 _up;
        private Vector3 _forward;
		
		public Camera(float verticalFov, float aspectRatio)
		{
            var theta = MathHelpers.DegreesToRadians(verticalFov);
            var viewportHeight = (float)Math.Tan(theta/2f) * 2.0f;

            var viewportWidth = aspectRatio * viewportHeight;
			var focalLength = 1f;

            _origin = new Vector3();
            _right = new Vector3(viewportWidth, 0.0f, 0.0f);
            _up = new Vector3(0.0f, viewportHeight, 0.0f);
            _forward = new Vector3(0.0f, 0f, focalLength);

            _lowerLeftCorner = _origin - _right / 2.0f - _up / 2.0f - _forward;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(_origin, _lowerLeftCorner + u * _right + v * _up);
        }
	}
}

