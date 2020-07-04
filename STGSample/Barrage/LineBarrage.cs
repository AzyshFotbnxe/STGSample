using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using STGSample.Bullets;

namespace STGSample.Barrage
{
    public class LineBarrage : AbstractBarrage
    {
        private Vector2 velocity;
        private Vector2 acceleration;
        public LineBarrage(IEnemy creator, int amount, float gap, Vector2 velocity, Vector2 acceleration, Vector2 position, float damage, Type bulletType = null, float lifeTime = -1)
            :base(creator, amount, gap,position, damage, bulletType, lifeTime)
        {
            this.velocity = velocity;
            this.acceleration = acceleration;
        }
        public override void Fire(List<IProjectile> bullets, List<IEnemy> enemies)
        {
            if (IsValid && fireDelay > gap)
            {
                fireDelay -= gap;
                amount--;
                var bullet = (IProjectile)Activator.CreateInstance(bulletType, creator, position, velocity, acceleration, damage, Color.White, lifeTime);
                createdBullets.Add(bullet);
                bullets.Add(bullet);
            }
        }
    }
}
