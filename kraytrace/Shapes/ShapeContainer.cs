using System;
using kraytrace.Shapes.Interfaces;

namespace kraytrace.Shapes
{
	//TODO: Container really ?!? this needs a better name...
	public class ShapeContainer
	{
		private List<IRayCollision> _shapes;

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

		public bool FindClosestIntersection(Ray r, float tMin, float tMax, out HitRecord? rec)
		{
			var hitAnything = false;
			var closest = tMax;
			rec = null;

			foreach (var shape in _shapes)
			{
				HitRecord? record = shape.Intersected(r, tMin, closest);

				if(record.HasValue)
				{
					hitAnything = true;
					closest = record.Value.TValue;
					rec = record.Value;
				}
			}

			return hitAnything;
		}
	}
}

