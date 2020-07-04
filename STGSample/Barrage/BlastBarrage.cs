using Microsoft.Xna.Framework;
using STGSample.Bullets;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Barrage
{
    public class BlastBarrage : AbstractBarrage
    {
        private readonly float maxVelocity;
        private readonly float minVelocity;
        private readonly Vector2 acceleration;
        private readonly int dense;
        public BlastBarrage(IEnemy creator, int amount, float gap, int dense, Vector2 position, float damage, float maxVelocity, Vector2 acceleration, float minVelocity = 0, float lifeTime = -1, Type bulletType = null)
            : base(creator, amount, gap, position, damage, bulletType, lifeTime)
        {
            this.maxVelocity = maxVelocity;
            this.minVelocity = minVelocity;
            this.dense = dense;
            this.acceleration = acceleration;
        }
        public override void Fire(List<IProjectile> bullets, List<IEnemy> enemies)
        {
            if (IsValid && fireDelay > gap)
            {
                fireDelay -= gap;
                amount--;
                for (int i = 0; i < dense; i++)
                {
                    var bullet = (IProjectile) Activator.CreateInstance(bulletType, creator, position, VectorUtils.RandomLimitedLengthVector(maxVelocity, minVelocity), acceleration, damage, Color.White, lifeTime);
                    createdBullets.Add(bullet);
                    bullets.Add(bullet);
                }
            }
        }
    }
}
