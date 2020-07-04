using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Sprites;
using STGSample.Utils;

namespace STGSample.Enemies
{
    public abstract class AbstractEnemy : IEnemy
    {
        public virtual bool IsAlive => Health > 0;
        public float MaxHealth { get; protected set; }
        public float Health { get; protected set; }

        public float Attack { get; protected set; }

        public float FireRate { get; protected set; }

        public float Defense { get; protected set; }

        public bool IsDestroyed { get; set; }
        protected readonly List<IBarrage> barrages = new List<IBarrage>();
        public Vector2 Position => centerPosition;

        public virtual Vector2 DrawPoint => throw new NotImplementedException();

        public Physics Physics { get; protected set; }

        protected Vector2 centerPosition;

        protected Sprite sprite;

        public virtual event Action<IEnemy> DeathEvent;

        public virtual void Damaged(float damage)
        {
            if (IsAlive)
            {
                float loseHealth = Math.Max(damage - Defense, damage * 0.2f); //At least each bullet make 20% damage.
                Health -= loseHealth;
                if (Health <= 0)
                {
                    DeathEvent?.Invoke(this);
                    IsDestroyed = true;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch) { sprite.Draw(spriteBatch, DrawBox, 0.5f); }
        public virtual IGraph HitBox=> new GraphRectangle();
        public virtual void Update(GameTime gameTime) { }
        public virtual void HitPlayer(IPlayer player) { }
        public virtual Rectangle DrawBox => HitBox.BoundingBox;
        public virtual void Fire(List<IProjectile> bullets, List<IEnemy> enemies) { }
        public virtual void Heal(float heal)
        {
            Health = Math.Min(MaxHealth, Health + heal);
        }
        public virtual void Move(Vector2 direction)
        {
            if (!float.IsNaN(direction.X)) centerPosition.X += direction.X;
            if (!float.IsNaN(direction.Y)) centerPosition.Y += direction.Y;
        }
        public virtual void SetPosition(float x, float y)
        {
            centerPosition = new Vector2(x, y);
        }
    }
}
