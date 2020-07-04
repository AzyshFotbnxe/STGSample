using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Barrage;
using STGSample.Bullets;
using STGSample.Enemies;
using STGSample.Factories;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Enemies
{
    public class SampleBoss : AbstractEnemy
    {
        public override Vector2 DrawPoint => new Vector2(Position.X - 10, Position.Y - 10);
        private Vector2 FirePosition => new Vector2(Position.X, Position.Y + 10);
        public override event Action<IEnemy> DeathEvent;
        public SampleBoss(Vector2 position, float health, float attack, float fireRate, float defense)
        {
            centerPosition = position;
            Health = MaxHealth = health;
            Attack = attack;
            FireRate = fireRate;
            Defense = defense;
            IBarrage barrage = new BlastBarrage(this, -1, 1, 20, FirePosition, attack, 100f, 20f);
            barrage.Follow(this);
            barrages.Add(barrage);
            barrages.Add(new GridBarrage(this, -1, 2, 100, 10, 10, attack));
            //Physics = new Physics(new Vector2(1, 1), Vector2.Zero);
            sprite = SpriteFactory.CreateSprite(GetType());
            Physics = new Physics(new Vector2(200, 0), new Vector2());
        }
        public override void Damaged(float damage)
        {
            if (IsAlive)
            {
                float loseHealth = Math.Max(damage - Defense, damage * 0.2f); //At least each bullet make 20% damage.
                Health -= loseHealth;
                if (Health <= 0)
                {
                    DeathEvent?.Invoke(this);
                    foreach (IBarrage barrage in barrages) barrage.Destroy();
                    IsDestroyed = true;
                }
            }
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

        private float moveDelay;
        public override void Update(GameTime gameTime)
        {
            moveDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (moveDelay > 2) {
                moveDelay -= 2;
                Physics.Velocity = -Physics.Velocity;
            }
            sprite.Update(gameTime);
            centerPosition += Physics.Update(gameTime);
            foreach (var barrage in barrages) barrage.Update(gameTime);
        }

        //float bulletDelay;
        public override void Fire(List<IProjectile> bullets, List<IEnemy> enemies)
        {
            foreach (var barrage in barrages) barrage.Fire(bullets, enemies);
/*            if (bulletDelay >= 90)
            {
                bulletDelay -=90;
                for (int i = 0; i < 20; i++)
                {
                    bullets.Add(new EnemyBullet(null, new Vector2(0, 60 * i), new Vector2(50, 0), Vector2.Zero, Attack, Color.White));
                    bullets.Add(new EnemyBullet(null, new Vector2(60 * i, 0), new Vector2(0, 50), Vector2.Zero, Attack, Color.White));
                }
            }*/
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
