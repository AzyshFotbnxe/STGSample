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
            IBarrage barrage = new BlastBarrage(this, 3, 1, 20, FirePosition, attack, 100f, new Vector2(0, 20f), 20f, -1, typeof(BouncingBullet));
            barrage.Follow(this);
            barrages.Add(barrage);
            //barrages.Add(new GridBarrage(this, -1, 2, 100, 10, 10, attack));
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
        private int state;
        public override void Update(GameTime gameTime)
        {
            switch (state) {
                case 0:
                    {
                        moveDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (moveDelay > 2)
                        {
                            moveDelay -= 2;
                            Physics.Velocity = -Physics.Velocity;
                        }
                        sprite.Update(gameTime);
                        centerPosition += Physics.Update(gameTime);
                        foreach (var barrage in barrages) {
                            barrage.Update(gameTime);
                            if (!barrage.IsValid) state = 1;
                        }
                        break;
                    }
                case 1:
                    {
                        barrages.Clear();
                        Defense = 10;
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(-30, 50), new Vector2(25, 3), new Vector2(Position.X - 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(30, 50), new Vector2(-25, 3), new Vector2(Position.X + 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(-40, 50), new Vector2(20, 3), new Vector2(Position.X - 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(40, 50), new Vector2(-20, 3), new Vector2(Position.X + 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(-50, 50), new Vector2(10, 3), new Vector2(Position.X - 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(50, 50), new Vector2(-10, 3), new Vector2(Position.X + 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(-60, 50), new Vector2(20, 3), new Vector2(Position.X - 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(60, 50), new Vector2(-20, 3), new Vector2(Position.X + 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(-100, 50), new Vector2(25, 3), new Vector2(Position.X - 10, Position.Y + 10), Attack));
                        barrages.Add(new LineBarrage(this, -1, 0.3f, new Vector2(100, 50), new Vector2(-25, 3), new Vector2(Position.X + 10, Position.Y + 10), Attack));

                        /*                        barrages.Add(new BlastBarrage(this, -1, 3, 10, Position, Attack / 2, 20f, new Vector2(10f, 5f), 5f, -1, typeof(BouncingBullet)));
                        barrages.Add(new BlastBarrage(this, -1, 3, 10, Position, Attack / 2, 20f, new Vector2(-10f, 5f), 5f, -1, typeof(BouncingBullet)));*/
                        barrages.Add(new BlastBarrage(this, -1, 5, 10, Position, Attack / 2, 30f, new Vector2(0, 10f), 20f));
                        barrages.Add(new GridBarrage(this, -1, 1f, 75, 9, 9, Attack));
                        state = 2;
                        break;
                    }
                case 2:
                    {
                        foreach (var barrage in barrages) barrage.Update(gameTime);
                        break;
                    }
            }
        }

        //float fireDelay;
        public override void Fire(List<IProjectile> bullets, List<IEnemy> enemies)
        {
            foreach (var barrage in barrages) barrage.Fire(bullets, enemies);
/*            float fireGap = (FireRate + 2) / 2;
            float damage = Attack;
            fireDelay -= fireGap;
            if (fireDelay <= 0)
            {
                fireDelay = 24;
                bullets.Add(new BouncingBullet(this, FirePosition, new Vector2(90, 10), Vector2.Zero, damage, Color.White));
                bullets.Add(new BouncingBullet(this, FirePosition, new Vector2(-90, 10), Vector2.Zero, damage, Color.White));
            }*/
        }
    }
}
