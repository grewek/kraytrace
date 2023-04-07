using System;

using kraytrace.LinearAlgebra;
using kraytrace.Shapes;

namespace kraytrace.Surfaces
{
	public class DielectricSurface : Interfaces.Material
	{
		private float _ior;

		public DielectricSurface(float refractionIndex)
		{
			_ior = refractionIndex;
		}

        public bool Scatter(Ray inboundRay, HitRecord rec, out Vector3 attenuation, out Ray scatterRay)
        {
			var refractionRatio = rec.FrontFace ? (1f / _ior) : _ior;

			var unitDirection = Vector3.Normalize(inboundRay.Direction);

			var cosTheta = Math.Min(Vector3.DotProduct(-unitDirection, rec.Normal), 1f);
			var sinTheta = Math.Sqrt(1f - cosTheta * cosTheta);

			var cannotRefract = refractionRatio * sinTheta > 1f;

			Vector3 direction;
			if(cannotRefract || reflectance(cosTheta, refractionRatio) > MathHelpers.RandomFloat())
			{
				direction = Vector3.Reflect(unitDirection, rec.Normal);
			} else
			{
				direction = Vector3.Refract(unitDirection, rec.Normal, refractionRatio);
			}

            attenuation = new Vector3(1f, 1f, 1f);
            scatterRay = new Ray(rec.Position, direction);

			return true;
        }

        private float reflectance(float cosine, float reflectionIndex)
        {
            var r0 = (1f - reflectionIndex) / (1f + reflectionIndex);
            r0 = r0 * r0;

            return r0 + (1f - r0) * (float)Math.Pow(1 - cosine, 5);
        }
    }
}

