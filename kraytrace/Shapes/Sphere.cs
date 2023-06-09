﻿using System;

using kraytrace.LinearAlgebra;
using kraytrace.Surfaces.Interfaces;

namespace kraytrace.Shapes
{
	public class Sphere : Interfaces.IRayCollision
    {
        private Vector3 _center;

        private float _radius;

        private Material _surfaceMaterial;

        public Vector3 Center
        {
            get { return _center; }
        }

		public Sphere()
		{
            _center = new Vector3();
            _radius = 1f; //NOTE: Create a unit sphere at the (0,0,0) Coordinates
            //TODO: Create a default surface material !

		}

        public Sphere(Vector3 center, float radius, Material surfaceMaterial)
        {
            _center = center;
            _radius = radius;
            _surfaceMaterial = surfaceMaterial;
        }


        public HitRecord? Intersected(Ray r, float tMin, float tMax)
        {
            Vector3 sphereCenter = r.Origin - _center;

            var a = r.Direction.LengthSquared();
            var halfB = Vector3.DotProduct(sphereCenter, r.Direction);
            var c = sphereCenter.LengthSquared() - _radius * _radius;

            var discriminant = halfB * halfB - a * c;

            if(discriminant < 0f)
            {
                return null;
            }

            var rDiscriminant = (float)Math.Sqrt(discriminant);

            var root = (-halfB - rDiscriminant) / a;

            if (root < tMin || root > tMax)
            {
                root = (-halfB + rDiscriminant) / a;

                if (root < tMin || root > tMax)
                {
                    return null;
                }
            }

            var position = r.At(root);
            var outwardNormal = (position - _center) / _radius;

            return new HitRecord(position, r.Direction, root, outwardNormal, _surfaceMaterial);

        }
    }
}

