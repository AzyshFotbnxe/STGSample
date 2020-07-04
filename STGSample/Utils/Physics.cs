using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Utils
{
    public class Physics
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Physics(Vector2 velocity, Vector2 acceleration)
        {
            this.Velocity = velocity;
            this.Acceleration = acceleration;
        }
        public Vector2 Update(GameTime gameTime)
        {
            var displacement = (Velocity + Acceleration / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            return displacement;
        }
    }
}
