using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Loading
{
    public class SpriteNode
    {
        public string ObjectName { get; set; }
        public string SpriteName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TotalFrame { get; set; }
        public float Delay { get; set; }

        public SpriteNode()
        {

        }

        public SpriteNode(string objectName, string spriteName, int width = 0, int height = 0, int totalFrame = 1, float delay = 0)
        {
            this.ObjectName = objectName;
            this.SpriteName = spriteName;
            this.Width = width;
            this.Height = height;
            this.TotalFrame = totalFrame;
            this.Delay = delay;
        }
    }
}
