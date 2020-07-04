using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Sprites;
using STGSample.Factories;
using STGSample.Utils;
using STGSample.Bullets;
using STGSample.Loading;

namespace STGSample.Players
{
    public class Player : IPlayer
    {
        public float MaxHealth { get; set; }
        public float Health { get; set; }
        public float Attack { get; set; }
        public float FireRate { get; set; }
        public float Defense { get; set; }
        public int Weapon { get; set; }
        public bool IsDestroyed { get; set ;}
        public event Action PlayerDeathEvent;
        public Vector2 DrawPoint => new Vector2(Position.X - 15, Position.Y + 9);
        public Vector2 Position => centerPosition;
        private Vector2 centerPosition; //Center point.
        private readonly Sprite sprite;
        private readonly IPlayerState state;
        public IGraph HitBox => new GraphCircle(centerPosition, 3);
        public Rectangle DrawBox => new Rectangle((int)DrawPoint.X, (int)DrawPoint.Y - 30, 30, 30);
        public bool IsAlive => Health > 0;
        public Player(Vector2 position, PlayerArchive archive)
        {
            centerPosition = position;
            MaxHealth = archive.Health;
            Health = archive.Health;
            Attack = archive.Attack;
            FireRate = archive.FireRate;
            Defense = archive.Defense;
            Weapon = archive.Weapon;
            state = new BaseState(this);
            IsDestroyed = false;
            sprite = SpriteFactory.CreateSprite(GetType());
        }
        public void Damaged(float damage)
        {
            if (Health > 0)
            {
                state.Damaged(damage);
                if (Health < 0)
                {
                    PlayerDeathEvent?.Invoke();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, DrawBox,layerDepth: 0.5f);
        }

        public void Update(GameTime gameTime)
        {
            //DrawPoint = Mouse.GetState().Position.ToVector2();
            //Console.WriteLine("Health" + Health.ToString());
            state.Update(gameTime);
            sprite.Update(gameTime);
        }
        public void Fire(List<IProjectile> bullets)
        {
            state.Fire(bullets);
        }
        public void UpgradeAttack(float level = 1)
        {
            Attack += level;
        }

        public void UpgradeDefense(float level = 1)
        {
            Defense += level;
        }

        public void UpgradeFireRate(float level = 1)
        {
            FireRate += level;
            if (FireRate > 10) { Attack += FireRate - 10; FireRate = 10; }
            if (FireRate < 0) FireRate = 0;
        }

        public void UpgradeHealth(float level = 1)
        {
            MaxHealth += level;
            Health += level;
        }

        public void UpgradeWeapon(int level = 1)
        {
            Weapon += level;
            if(Weapon < 1) { UpgradeFireRate(Weapon - 1); Weapon = 1; }
            if(Weapon > 5) { UpgradeFireRate(Weapon - 5); Weapon = 5; }
        }

        public void Heal(float heal)
        {
            Health += heal;
            if(Health > MaxHealth)
            {
                MaxHealth += (Health - MaxHealth) / 4f;
                Health = MaxHealth;
            }
        }

        public void SetPosition(float x, float y)
        {
            centerPosition = new Vector2(x, y);
        }

        public void Move(Vector2 direction)
        {
            if (!float.IsNaN(direction.X)) centerPosition.X += direction.X;
            if (!float.IsNaN(direction.Y)) centerPosition.Y += direction.Y;
        }

        public void AddState(IPlayerState newState)
        {
            IPlayerState nextState = state.NextState;
            state.NextState = newState;
            newState.NextState = nextState;
            nextState.PrevState = newState;
            newState.PrevState = state;
        }
    }
}
