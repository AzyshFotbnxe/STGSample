using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Factories;
using STGSample.Sprites;
using STGSample.Utils;

namespace STGSample.Bullets
{
    public class Bullet : AbstractBullet
    {
        public Bullet() { }
        public Bullet(IGameObject shooter, Vector2 position, Vector2 velocity, Vector2 acceleration, float damage, Color? color)
            :base(shooter, position, damage, color)
        {
            Physics = new Physics(velocity, acceleration);
        }
    }
}
