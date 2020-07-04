using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Input;

namespace STGSample.Controller
{
    public class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> keyDownDictionary = new Dictionary<Keys, ICommand>();
        private readonly Dictionary<Keys, ICommand> keyUpDictionary = new Dictionary<Keys, ICommand>();
        private readonly List<Keys> checkKeyUplist = new List<Keys>();
        private readonly List<Keys> nonHoldableKeys = new List<Keys>();
        private KeyboardState prevState;
        private IPlayer player => game.Player;
        private readonly STGMain game;

        public KeyboardController(STGMain game, params (Keys key, ICommand KeyDownCommand, ICommand KeyUpCommand, bool CanBeHeld)[] args)
        {
            this.game = game;
            foreach (var (key, downCommand, upCommand, canBeHeld) in args)
            {
                keyDownDictionary.Add(key, downCommand);
                keyUpDictionary.Add(key, upCommand);
                if (!canBeHeld)
                    nonHoldableKeys.Add(key);
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 moveVector = Vector2.Zero;
            int moveScale = 5;
            if (state.IsKeyDown(Keys.LeftShift)) moveScale = 2;
            if (state.IsKeyDown(Keys.A)) moveVector.X -= 2;
            if (state.IsKeyDown(Keys.D)) moveVector.X += 2;
            if (state.IsKeyDown(Keys.W)) moveVector.Y -= 2;
            if (state.IsKeyDown(Keys.S)) moveVector.Y += 2;
            if (state.IsKeyDown(Keys.Z) && prevState.IsKeyUp(Keys.Z)) player.UpgradeWeapon(1);
            if (state.IsKeyDown(Keys.X) && prevState.IsKeyUp(Keys.X)) player.UpgradeWeapon(-1);
            if (state.IsKeyDown(Keys.C) && prevState.IsKeyUp(Keys.C)) player.UpgradeFireRate(1);
            if (state.IsKeyDown(Keys.R) && prevState.IsKeyUp(Keys.R)) game.ResetGame();
            if (state.IsKeyDown(Keys.P) && prevState.IsKeyUp(Keys.P)) game.State.Pause();
            moveVector.Normalize();
            moveVector *= moveScale;
            player.Move(moveVector);
            prevState = state;
        }
    }
}
