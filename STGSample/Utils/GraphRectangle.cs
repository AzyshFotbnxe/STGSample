using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace STGSample.Utils
{
    public struct GraphRectangle : IGraph
    {
        private Rectangle rectangle;
        public bool IsEmpty => rectangle.IsEmpty;
        public Rectangle BoundingBox => rectangle;

        public GraphRectangle(Rectangle rect)
        {
            rectangle = rect;
        }
        public GraphRectangle(Vector2 location, Vector2 size)
        {
            rectangle = new Rectangle(location.ToPoint(), size.ToPoint());
        }
        public GraphRectangle(Point location, Point size)
        {
            rectangle = new Rectangle(location, size);
        }
        public GraphRectangle(float x, float y, float width, float height)
        {
            rectangle = new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        public bool Intersects(IGraph graph)
        {
            if (graph is GraphRectangle) return rectangle.Intersects(((GraphRectangle)graph).rectangle);
            if (graph is GraphCircle) return graph.Intersects(this);
            return false;
        }
        public bool Intersects(Rectangle rect)
        {
            return rectangle.Intersects(rect);
        }

    }
}
