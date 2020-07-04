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
    public class RotatingBullet : AbstractBullet
    {
        private float rotateVelocity;
        private Vector2 center;
        private Vector2 rotateVector => Position - center;
        public RotatingBullet(IGameObject shooter, Vector2 position, Vector2 center, Vector2 centerVelocity, float rotateVelocityDegree, float damage, Color? color, float lifeTime = -1)
            :base(shooter, position, damage, color, lifeTime)
        {
            this.center = center;
            rotateVelocity = rotateVelocityDegree;
            Physics = new Physics(centerVelocity, Vector2.Zero);
        }
        public override void Update(GameTime gameTime)
        {
            var velo = rotateVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            center += Physics.Update(gameTime);
            var rotateVect = VectorUtils.RotateVector(rotateVector, velo);
            Position = center + rotateVect;
        }
    }
}
