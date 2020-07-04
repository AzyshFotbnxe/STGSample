using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Factories;
using STGSample.Sprites;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Bullets
{
    public abstract class AbstractBullet : IProjectile
    {
        protected float lifeTime;
        public bool IsDestroyed { get; set; }
        protected float sizeRadius = 5;
        public virtual Vector2 Position { get; protected set; }
        public virtual Vector2 DrawPoint => new Vector2(Position.X - sizeRadius, Position.Y - sizeRadius);
        public float Damage { get; protected set; }
        public virtual IGraph HitBox => new GraphCircle(Position, sizeRadius);
        protected IGameObject shooter;
        public Physics Physics { get; protected set; }
        protected Sprite sprite;
        public virtual Rectangle DrawBox => HitBox.BoundingBox;
        public AbstractBullet() { }
        public AbstractBullet(IGameObject shooter, Vector2 position, float damage, Color? color, float lifeTime = -1)
        {
            this.shooter = shooter;
            Position = position;
            Damage = damage;
            sprite = SpriteFactory.CreateSprite(this.GetType());
            if (color is null) color = Color.White;
            sprite.SetColor(new Collection<Color> { (Color)color });
            this.lifeTime = lifeTime;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, DrawBox, layerDepth: 0.6f);
        }

        public virtual void HitTarget()
        {
            IsDestroyed = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (lifeTime > 0) lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((int)lifeTime == 0) { IsDestroyed = true; return; }
            Position += Physics.Update(gameTime);
        }
    }
}
