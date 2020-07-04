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
    public class GamePause : IGameState
    {
        private readonly List<Button> Button;
        private SpriteFont font => ConstantsTable.FONT;
        private readonly STGMain stgGame;
        private Camera Camera => stgGame.Camera;
        private readonly IGameState lastState;
        public GamePause(STGMain game, IGameState ongoing)
        {
            stgGame = game;
            game.IsMouseVisible = true;

            var resumeBotton = new Button(font, "Resume", new Vector2(Camera.RightBound + 20, 550), Camera);
            resumeBotton.Click += ResumeClick;

            var quitButton = new Button(font, "Quit", new Vector2(Camera.RightBound + 150, 550), Camera);
            quitButton.Click += QuitGameClick;

            Button = new List<Button>
            {
                resumeBotton,
                quitButton
            };
            lastState = ongoing;
        }

        private void ResumeClick(Button button)
        {
            stgGame.State = lastState;
        }
        private void QuitGameClick(Button button)
        {
            stgGame.State = new GameLeaving(stgGame, lastState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            lastState.Draw(spriteBatch);
            foreach (Button ele in Button) ele.Draw(spriteBatch);
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
        public void Reset() { }
    }
}

