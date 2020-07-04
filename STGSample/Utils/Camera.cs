using Microsoft.Xna.Framework;
using STGSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Utils
{
    public class Camera
    {
        public float LeftBound { get; private set; }
        public float RightBound { get; private set; }
        public IPlayer Focus { get; private set; }
        public Matrix Transform { get; private set; }
        private float windowWidth => ConstantsTable.CAMERAWIDTH;
        public Camera() { }
        public void SetFocus(IPlayer obj)
        {
            Focus = obj;
        }
        public void Reset()
        {
            LeftBound = 100;
            RightBound = LeftBound + windowWidth;
        }
        public void Reset(IPlayer obj)
        {
            LeftBound = 100;
            RightBound = LeftBound + windowWidth;
            Focus = obj;
        }
        public void Update()
        {
            if (Focus is null) return;
            Vector2 targetPosition = Focus.Position;
            LeftBound = Math.Max(0, targetPosition.X + Focus.DrawBox.Width / 2 - windowWidth / 2);
            RightBound = Math.Min(600, LeftBound + windowWidth);
            LeftBound = RightBound - windowWidth;
            var position = Matrix.CreateTranslation(-LeftBound - windowWidth / 2, 0, 0);
            var offset = Matrix.CreateTranslation(windowWidth / 2, 0, 0);
            Transform = position * offset;
        }
    }
}
