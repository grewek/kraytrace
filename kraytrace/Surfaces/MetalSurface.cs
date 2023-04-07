using System;


using kraytrace.LinearAlgebra;
using kraytrace.Shapes;

namespace kraytrace.Surfaces
{
	public class MetalSurface : Interfaces.Material
	{
		private Vector3 _albedo;
		private float _roughness;

		public MetalSurface(Vector3 color, float roughness)
		{
			_albedo = color;
			_roughness = roughness;
		}

        public bool Scatter(Ray inboundRay, HitRecord rec, out Vector3 attenuation, out Ray scatterRay)
        {
			var rayIndirection = _roughness * Vector3.RandomVectorInUnitSphere();

			Vector3 reflected = Vector3.Reflect(rec.Position, rec.Normal) + rayIndirection;
			scatterRay = new Ray(rec.Position, reflected);
			attenuation = _albedo;

			return (Vector3.DotProduct(scatterRay.Direction, rec.Normal) > 0.0f);
        }
    }
}

