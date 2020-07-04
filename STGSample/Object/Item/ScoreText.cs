using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Utils;

namespace STGSample.Items
{
    public class ScoreText : AbstractItem, IItem
    {
        private float timer = 0.5f;
        private Vector2 velocity = new Vector2(0, -100);
        private readonly SpriteFont spriteFont;
        private readonly string str;
        public ScoreText(Vector2 location, SpriteFont inputSpriteFont, string str)
        {
            Position = location;
            Physics = new Physics(velocity, Vector2.Zero);
            spriteFont = inputSpriteFont;
            this.str = str;
        }

        public override void Update(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Physics.Update(gameTime);
            if (timer <= 0)
                IsDestroyed = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, str, Position, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
