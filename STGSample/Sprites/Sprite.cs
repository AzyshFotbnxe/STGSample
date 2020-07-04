using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Sprites
{
    public class Sprite
    {
        private readonly Texture2D texture;
        private int currentFrame;
        private readonly int width;
        private readonly int height;
        private readonly int totalFrame;
        private int delay;
        private Collection<Color> SpriteColor;
        private int colorIndex;
        public Sprite(Texture2D texture, int frame)
        {
            delay = 0;
            colorIndex = 0;
            currentFrame = 0;
            this.texture = texture;
            totalFrame = frame;
            width = texture.Width / totalFrame;
            height = texture.Height;
            SpriteColor = new Collection<Color> { Color.White };
        }
        /* Set color and Set layer can be passed into the Draw Method 
            but that serves only for very few cases
            so we just make some methods instead
         */
        public void SetColor(Collection<Color> colors)
        {
            SpriteColor = colors;
        }
        public void Update(GameTime gameTime)
        {
            /* temporary solution, will add time delay later
                since it takes time to tweak around different delay time for diff objects
                we will make that happen in Sprint 5
             */
            if (delay % 5 == 0)
            {
                currentFrame++;
                if (currentFrame == totalFrame)
                    currentFrame = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle targetRectangle, float layerDepth = 1f, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            int row = (int)((float)currentFrame / (float)totalFrame);
            int column = currentFrame % totalFrame;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            delay++;
            if (delay % 5 == 0)
                colorIndex++;
            /* This condition is used for alternating colors for star mario
            *  aim to slow color changing rate 
            */
            if (colorIndex % SpriteColor.Count == 0 || colorIndex > SpriteColor.Count)
                colorIndex = 0;
            Color spriteColor = SpriteColor[colorIndex];
            spriteBatch.Draw(texture, targetRectangle, sourceRectangle, spriteColor, 0f, Vector2.Zero, spriteEffects, layerDepth);
        }
    }
}
