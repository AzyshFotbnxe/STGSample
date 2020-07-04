using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Bullets;
using STGSample.Manager;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Enemies
{
    public class Tracer : AbstractEnemy
    {
        private float updateTime = 0.2f;
        private float MaxLength = 20;
        private IPlayer target;
        public float Damage { get; private set; } = 0;

        public override Vector2 DrawPoint => Position;

        public override void Draw(SpriteBatch spriteBatch) { }

        private float timer;
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public override void Damaged(float damage)
        {
            return; //This does not take damage.
        }

        public override void Fire(List<IProjectile> bullets, List<IEnemy> enemies)
        {
            if (timer > updateTime)
            {
                timer = 0;
                var directionVector = target.Position - Position;
                float dist = Math.Min(directionVector.Length(), MaxLength);
                directionVector.Normalize();
                directionVector *= dist;
                if(!float.IsNaN(directionVector.X)) centerPosition.X += directionVector.X;
                if (!float.IsNaN(directionVector.Y)) centerPosition.Y += directionVector.Y;
                bullets.Add(new EnemyBullet(this, Position, Vector2.Zero, Vector2.Zero, Damage, Color.White, 2));
            }
        }

        public Tracer(Vector2 position, IPlayer player)
        {
            centerPosition = position;
            target = player;
        }
    }
}
