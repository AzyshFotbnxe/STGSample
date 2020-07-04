using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample
{
    public interface IController
    {
        void Update(GameTime gameTime); //Controller will return a vector to tell how to move the player.
    }
}
