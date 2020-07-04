﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.GameState
{
    public interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Pause();
        void Reset();
    }
}
