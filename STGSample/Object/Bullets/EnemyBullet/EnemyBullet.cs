using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Factories;
using STGSample.Sprites;
using STGSample.Utils;

namespace STGSample.Bullets
{
    public class EnemyBullet : AbstractBullet
    {
        public EnemyBullet(IGameObject shooter, Vector2 position, Vector2 velocity, Vector2 acceleration, float damage, Color? color, float lifeTime = -1)
            : base(shooter, position, damage, color, lifeTime)
        {
            Physics = new Physics(velocity, acceleration);
        }

        public override void Update(GameTime gameTime)
        {
            if(lifeTime > 0) lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((int)lifeTime == 0) { IsDestroyed = true; return; }
            base.Update(gameTime);
        }

        public override void HitTarget()
        {
            IsDestroyed = true;
        }

    }
}
