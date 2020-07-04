using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.GameState
{
    public class GameMenu : IGameState
    {
        private readonly List<Button> Button;
        private GraphicsDevice graphics => stgGame.GraphicsDevice;
        private SpriteFont font => ConstantsTable.FONT;
        private readonly STGMain stgGame;
        public GameMenu(STGMain game)
        {
            this.stgGame = game;
            game.IsMouseVisible = true;

            var startButton = new Button(font, "New Game", new Vector2(300, 240));
            startButton.Click += NewGameClick;

            var upgradeButton = new Button(font, "Upgrade", new Vector2(300, 300));
            upgradeButton.Click += UpgradeClick;

            var quitButton = new Button(font, "Quit", new Vector2(300, 360));
            quitButton.Click += QuitGameClick;

            Button = new List<Button>
            {
                startButton,
                upgradeButton,
                quitButton
            };
            quitButton.ChangeColor(Color.Red, null);
        }

        private void NewGameClick(Button button)
        {
            stgGame.State = new GameOnGoing(stgGame);
        }
        private void UpgradeClick(Button button)
        {
            stgGame.State = new GameUpgrade(stgGame);
        }
        private void QuitGameClick(Button button)
        {
            stgGame.Exit();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            //graphics.Clear(Color.AliceBlue);
            spriteBatch.DrawString(font, "Test", new Vector2(300, 100), Color.Black);
            //spriteBatch.Draw(marioTitle, Vector2.Zero, Color.White);
            foreach (var ele in Button)
                ele.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var ele in Button)
                ele.Update(gameTime);
        }

        public void Pause()
        {
            // Do Nothing
        }

        public void Reset()
        {
            // Do Nothing
        }
    }
}

