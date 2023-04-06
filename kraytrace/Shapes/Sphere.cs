using System;
using kraytrace.LinearAlgebra;

namespace kraytrace.Shapes
{
	public class Sphere : Interfaces.IRayCollision
    {
        private HitRecord _record;

        private Vector3 _center;
        private float _radius;

        public HitRecord Record
        {
            get { return _record; }
        }

        public Vector3 Center
        {
            get { return _center; }
        }

		public Sphere()
		{
            _center = new Vector3();
            _radius = 1f; //NOTE: Create a unit sphere at the (0,0,0) Coordinates
		}

        public Sphere(Vector3 center, float radius)
        {
            _center = center;
            _radius = radius;
        }


        public bool Intersected(Ray r, float tMin, float tMax)
        {
            Vector3 sphereCenter = r.Origin - _center;

            var a = r.Direction.LengthSquared();
            var halfB = Vector3.DotProduct(sphereCenter, r.Direction);
            var c = sphereCenter.LengthSquared() - _radius * _radius;

            var discriminant = halfB * halfB - a * c;

            if(discriminant < 0f)
            {
                return false;
            }

            var rDiscriminant = (float)Math.Sqrt(discriminant);

            var root = (-halfB - rDiscriminant) / a;

            if (root < tMin || root > tMax)
            {
                root = (-halfB + rDiscriminant) / a;

                if (root < tMin || root > tMax)
                {
                    return false;
                }
            }

            _record.TValue = root;
            _record.Position = r.At(root);

            Vector3 outwardNormal = (_record.Position - _center) / _radius;
            _record.DetermineFaceNormal(r, outwardNormal);

            return true;

        }
    }
}

