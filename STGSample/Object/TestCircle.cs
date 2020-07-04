using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample;
using STGSample.Factories;
using STGSample.Sprites;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Test
{
    class TestCircle : IGameObject
    {
        public bool IsDestroyed { get; set; }

        public Vector2 Position => new Vector2(center.X, center.Y);

        public Vector2 DrawPoint => new Vector2(center.X - radius, center.Y - radius);
        public Rectangle DrawBox => HitBox.BoundingBox;
        public IGraph HitBox => new GraphCircle(center, radius);

        private Vector2 center;
        private float radius;
        private Sprite sprite;

        public TestCircle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
            sprite = SpriteFactory.CreateSprite(GetType());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, DrawBox);
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
        
        public void Collected(IPlayer player)
        {
            Console.WriteLine("hit");
        }
    }
}
