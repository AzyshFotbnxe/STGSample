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
    public class RotatingBullet : EnemyBullet
    {
        private float rotateRadius;
        private float rotateVelocity;
        private Vector2 rotateVector;
        public RotatingBullet(IGameObject shooter, Vector2 position, Vector2 linearVelocity, Vector2 direction, float rotateRadius, float rotateVelocityDegree, float damage, Color? color, float lifeTime = -1)
        {
            sprite = SpriteFactory.CreateSprite(this.GetType());
            this.shooter = shooter;
            Position = position;
            this.rotateRadius = rotateRadius;
            rotateVelocity = rotateVelocityDegree * ConstantsTable.DEGTORAD;
            Damage = damage;
            if (color is null) color = Color.White;
            sprite.SetColor(new Collection<Color> { (Color)color });
            this.lifeTime = lifeTime;
            this.Physics = new Utils.Physics(linearVelocity, Vector2.Zero);
            direction.Normalize();
            rotateVector = direction;
        }
        public override void Update(GameTime gameTime)
        {
            var linvelo = rotateRadius * rotateVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var disp = rotateVector * linvelo;
            rotateVector += VectorUtils.RightVerticalVector(rotateVector, disp.LengthSquared() / rotateRadius);
            rotateVector.Normalize();
            Position += disp;
            base.Update(gameTime);
        }
    }
}
