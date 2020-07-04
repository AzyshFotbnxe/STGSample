using Microsoft.Xna.Framework;
using STGSample.Factories;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Bullets
{
    public class FireBall : AbstractBullet
    {
        private bool explode;
        private float explosionTimer = 0.1f;
        public FireBall(IGameObject shooter, Vector2 position, Vector2 velocity, Vector2 acceleration, float damage, Color color)
            : base(shooter, position, damage, color, lifeTime: -1)
        {
            Physics = new Physics(velocity, acceleration);
            explode = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (explode)
            {
                sprite.Update(gameTime);
                explosionTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (explosionTimer <= 0)
                    IsDestroyed = true;
            }
            else base.Update(gameTime);
        }

        public override void HitTarget()
        {
            explode = true;
            base.HitTarget();
        }
    }
}
