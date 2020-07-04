using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Barrage;
using STGSample.Bullets;
using STGSample.Factories;
using STGSample.Utils;

namespace STGSample.Enemies
{
    public class SampleEnemy : AbstractEnemy
    {
        public override Vector2 DrawPoint => new Vector2(Position.X - 10, Position.Y - 10);
        private Vector2 FirePosition => new Vector2(Position.X, Position.Y + 10);
        public SampleEnemy(Vector2 position, float health, float attack, float fireRate, float defense)
        {
            centerPosition = position;
            Health = MaxHealth = health;
            Attack = attack;
            FireRate = fireRate;
            Defense = defense;
            barrages.Add(new LineBarrage(this, -1, 300, new Vector2(3, 4), Vector2.Zero, FirePosition, attack));
            barrages.Add(new LineBarrage(this, -1, 300, new Vector2(-3, -4), Vector2.Zero, FirePosition, attack));
            barrages.Add(new LineBarrage(this, -1, 300, new Vector2(4, -3), Vector2.Zero, FirePosition, attack));
            barrages.Add(new LineBarrage(this, -1, 300, new Vector2(-4, 3), Vector2.Zero, FirePosition, attack));
            //Physics = new Physics(new Vector2(1, 1), Vector2.Zero);
            sprite = SpriteFactory.CreateSprite(GetType());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, DrawBox, layerDepth: 0.5f);
        }

        public override Rectangle DrawBox => HitBox.BoundingBox;

        public override IGraph HitBox => new GraphRectangle(DrawPoint.X, DrawPoint.Y, 20, 20);

        public override void HitPlayer(IPlayer player)
        {
            //Do nothing.
        }

        public override void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
            //centerPosition += Physics.Update(gameTime);
            foreach (var barrage in barrages) barrage.Update(gameTime);
        }

        /*float fireDelay;*/
        public override void Fire(List<IProjectile> bullets, List<IEnemy> enemies)
        {
            foreach (var barrage in barrages) barrage.Fire(bullets, enemies);
            /*            float fireGap = (FireRate + 2) / 2;
            float damage = Attack;
            fireDelay -= fireGap;

            if (fireDelay <= 0)
            {
                fireDelay = 24;
                bullets.Add(new EnemyBullet(this, FirePosition, new Vector2(0, 10), Vector2.Zero, damage, Color.White));
            }*/
        }
    }
}
