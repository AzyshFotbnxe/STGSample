using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Sprites;
using STGSample.Utils;

namespace STGSample.Items
{
    public abstract class AbstractItem : IItem
    {
        public bool IsDestroyed { get; set; }

        public virtual Vector2 Position { get; protected set; }
        protected Vector2 position;
        public virtual Vector2 DrawPoint { get; protected set; }
        public Physics Physics { get; protected set; }
        protected Sprite sprite;

        public virtual Rectangle DrawBox => HitBox.BoundingBox;

        public virtual IGraph HitBox => new GraphRectangle();

        public virtual void Collected(IPlayer player) { IsDestroyed = true; }

        public virtual void Draw(SpriteBatch spriteBatch) { sprite.Draw(spriteBatch, DrawBox); }


        public virtual void Update(GameTime gameTime) { sprite.Update(gameTime); }
    }
}
