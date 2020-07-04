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
    public class LifeTimeBullet : Bullet
    {
        private float lifeTime;
        public LifeTimeBullet(IGameObject shooter, Vector2 position, Vector2 velocity, Vector2 acceleration, float damage, Color color, float lifeTime)
        {
            this.shooter = shooter;
            Position = position;
            Physics = new Physics(velocity, acceleration);
            Damage = damage;
            sprite = SpriteFactory.CreateSprite(this.GetType());
            sprite.SetColor(new Collection<Color> { color });
            this.lifeTime = lifeTime;
        }

        public override void Update(GameTime gameTime)
        {
            lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lifeTime < 0) { IsDestroyed = true; return; }
            base.Update(gameTime);
        }
    }
}
