using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Bullets;
using STGSample.Utils;

namespace STGSample.GameState
{
    public class GameLeaving : IGameState
    {
        private readonly STGMain stgGame;
        private readonly IGameState state;
        public GameLeaving(STGMain game, IGameState ongoingOrBossState)
        {
            stgGame = game;
            state = ongoingOrBossState;
            XMLUtils.WriteSav(stgGame.Archive);
        }
        public void Update(GameTime gameTime)
        {
            if (stgGame.Player.DrawPoint.Y < -100)
            {
                stgGame.State = new GameMenu(stgGame);
                return;
            }
            stgGame.Player.Move(new Vector2(0, -20));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            state.Draw(spriteBatch);
        }

        public void Pause() { }
        public void Reset() { }
    }
}
