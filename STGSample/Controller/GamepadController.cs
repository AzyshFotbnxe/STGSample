using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace STGSample.Controller
{
    public class GamepadController : IController
    {
        private STGMain game;
        private IPlayer player;
        public GamepadController(STGMain stgGame, IPlayer player)
        {
            game = stgGame;
            this.player = player;
        }
        public void Update(GameTime gameTime)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            //Whatever, I don't use this.
        }
    }
}
