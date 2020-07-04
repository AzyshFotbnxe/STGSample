using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Utils
{
    //Reference from http://rbwhitaker.wikidot.com/circle-collision-detection
    public struct GraphCircle : IGraph
    {
        public float Radius { get; private set; }
        public Vector2 Center { get; private set; }
        private float X => Center.X;
        private float Y => Center.Y;
        public bool IsEmpty => float.IsNaN(Radius);
        public Rectangle BoundingBox => new Rectangle((int)(X - Radius), (int)(Y - Radius), (int)(2 * Radius), (int)(2 * Radius));

        public GraphCircle(Vector2 center, float radius)
            : this()
        {
            Center = center;
            Radius = radius;
        }

        public bool Intersects(Rectangle rectangle)
        {
            // 8 point detection. Not that good.
            /*          var center = rectangle.Center;
                        var corners = new[]
                        {
                        new Vector2(rectangle.Left, rectangle.Top),
                        new Vector2(rectangle.Right, rectangle.Top),
                        new Vector2(rectangle.Left, rectangle.Bottom),
                        new Vector2(rectangle.Right, rectangle.Bottom),
                        new Vector2(center.X, rectangle.Top),
                        new Vector2(center.X, rectangle.Bottom),
                        new Vector2(rectangle.Left, center.Y),
                        new Vector2(rectangle.Right, center.Y)
                        };

                        foreach (Vector2 corner in corners)
                        {
                            if (Contains(corner))
                                return true;
                        }

                        return false;*/
            var xDistance = Math.Abs(Center.X - rectangle.Center.X);
            var yDistance = Math.Abs(Center.Y - rectangle.Center.Y);

            if (xDistance > (rectangle.Width / 2 + Radius)) { return false; }
            if (yDistance > (rectangle.Height / 2 + Radius)) { return false; }

            if (xDistance <= (rectangle.Width / 2)) { return true; }
            if (yDistance <= (rectangle.Height / 2)) { return true; }

            var dist = Math.Pow((xDistance - rectangle.Width / 2),2) +
                                 Math.Pow((yDistance - rectangle.Height / 2),2);

            return (dist <= (Math.Pow(Radius,2)));
        }

        public bool Intersects(GraphCircle circle)
        {
            return Vector2.Distance(circle.Center, Center) <= Radius + circle.Radius;
        }

        public bool Contains(Vector2 Vector2)
        {
            return (Vector2 - Center).Length() <= Radius;
        }

        public bool Intersects(IGraph graph)
        {
            if (graph is GraphCircle) return Intersects((GraphCircle)graph);
            if (graph is GraphRectangle) return Intersects(graph.BoundingBox);
            return false;
        }

    }
}
