using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample
{
    public interface IGameObject
    {
        bool IsDestroyed { get; set; }
        Vector2 Position { get; } //Position will always be the middle point.
        Vector2 DrawPoint { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        IGraph HitBox { get; }
        Rectangle DrawBox { get; }
    }
}
