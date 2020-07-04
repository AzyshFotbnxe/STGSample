using Microsoft.Xna.Framework;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STGSample.Utils.ConstantsTable;
namespace STGSample.Bullets
{
    public class BouncingBullet : AbstractBullet
    {
        public BouncingBullet(IGameObject shooter, Vector2 position, Vector2 velocity, Vector2 acceleration, float damage, Color? color, float lifeTime = -1)
            : base(shooter, position, damage, color, lifeTime)
        {
            Physics = new Physics(velocity, acceleration);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Position.X > BATTLEAREAWIDTH)
            {
                var newX = 2 * Position.X - BATTLEAREAWIDTH;
                Physics.Velocity.X = -Physics.Velocity.X;
                Physics.Acceleration.X = Math.Abs(Physics.Acceleration.X * Physics.Velocity.X) / Physics.Velocity.X;
                Position = new Vector2(newX, Position.Y);
            }
            if (Position.X < 0)
            {
                var newX = -Position.X;
                Physics.Velocity.X = -Physics.Velocity.X;
                Physics.Acceleration.X = Math.Abs(Physics.Acceleration.X * Physics.Velocity.X) / Physics.Velocity.X;
                Position = new Vector2(newX, Position.Y);
            }
        }
    }
}
