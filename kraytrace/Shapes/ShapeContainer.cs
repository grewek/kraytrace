using System;
using kraytrace.Shapes.Interfaces;

namespace kraytrace.Shapes
{
	//TODO: Container really ?!? this needs a better name...
	public class ShapeContainer
	{
		private List<IRayCollision> _shapes;
		private HitRecord _closestHit; //TODO: Refactor this is a really dumb idea...

		public HitRecord ClosestHit
		{
			get { return _closestHit; }
		}
		public ShapeContainer()
		{
			_shapes = new List<IRayCollision>();
		}

		public void AddShape(IRayCollision collidableObj)
		{
			_shapes.Add(collidableObj);
		}

		public void ClearShapeContainer()
		{
			_shapes.Clear();
		}

		public bool FindClosestIntersection(Ray r, float tMin, float tMax)
		{
			var hitAnything = false;
			var closest = tMax;

			foreach (var shape in _shapes)
			{
				if(shape.Intersected(r, tMin, closest))
				{
					hitAnything = true;
					closest = shape.Record.TValue; //TODO: Ugh icky api -.-
					_closestHit = shape.Record;
				}
			}

			return hitAnything;
		}
	}
}

