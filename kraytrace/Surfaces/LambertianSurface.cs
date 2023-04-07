using System;

using kraytrace.LinearAlgebra;
using kraytrace.Shapes;

namespace kraytrace.Surfaces
{
	public class LambertianSurface: Interfaces.Material
	{
		Vector3 _albedo;

		public LambertianSurface()
		{
			//A light grey as the default color
			_albedo = new Vector3(.75f, .75f, .75f);
		}

		public LambertianSurface(Vector3 color)
		{
			_albedo = color;
		}

        public bool Scatter(Ray inboundRay, HitRecord rec, out Vector3 attenuation, out Ray scatterRay)
        {
			var shouldScatter = MathHelpers.RandomFloat();

			var scatterDirection = rec.Normal + Vector3.RandomVectorInHemisphere(rec.Normal);

			if(MathHelpers.CloseToZero(scatterDirection))
			{
				scatterDirection = rec.Normal;
			}

			scatterRay = new Ray(rec.Position, scatterDirection);
			attenuation = _albedo;
			return true;
        }
    }
}

