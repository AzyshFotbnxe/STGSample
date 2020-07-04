using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.GameState
{
    public class Button
    {
        private MouseState currentMouse;
        private MouseState previousMouse;
        private SpriteFont spriteFont;
        private bool isHovering;

        public event Action<Button> Click;
        public bool Disabled { get; set; }
        private Vector2 position;
        private Rectangle rectangle;
        private Color normalColor;
        private Color hoverColor;
        private string text;
        private Camera gameCamera;
        public Button(SpriteFont spriteFont, string text, Vector2 position, Camera camera = null, bool disable = false)
        {
            this.spriteFont = spriteFont;
            this.text = text;
            this.position = position;
            rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, (int)spriteFont.MeasureString(text).X, (int)spriteFont.MeasureString(text).Y);
            normalColor = Color.White;
            hoverColor = Color.Gray;
            gameCamera = camera;
            Disabled = disable;
        }
        public Button(SpriteFont spriteFont, string text, Vector2 position, Color normal, Color hover, Camera camera = null, bool disable = false)
        {
            this.spriteFont = spriteFont;
            this.text = text;
            this.position = position;
            rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, (int)spriteFont.MeasureString(text).X, (int)spriteFont.MeasureString(text).Y);
            normalColor = normal;
            hoverColor = hover;
            gameCamera = camera;
            Disabled = disable;
        }
        public void ChangeText(string text)
        {
            this.text = text;
        }
        public void ChangeColor(Color? normal, Color? hover)
        {
            if (!(normal is null)) normalColor = (Color)normal;
            if (!(hover is null)) hoverColor = (Color)hover;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var color = isHovering? hoverColor: normalColor;

            if (!string.IsNullOrEmpty(text))
            {
                spriteBatch.DrawString(spriteFont, text, position, color,0f,Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!Disabled)
            {
                previousMouse = currentMouse;
                currentMouse = Mouse.GetState();
                float offset = gameCamera is null ? 0 : gameCamera.LeftBound;
                var mousePoint = new Point((int)(currentMouse.X + offset), currentMouse.Y);
                isHovering = false;
                if (rectangle.Contains(mousePoint))
                {
                    isHovering = true;
                    if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        Click?.Invoke(this);
                    }
                }
            }
        }
    }
}

