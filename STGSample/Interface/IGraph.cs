using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample
{
    public interface IGraph
    {
        Rectangle BoundingBox { get; }
        bool IsEmpty { get; }
        bool Intersects(IGraph graph);
        bool Intersects(Rectangle rect);
    }
}
