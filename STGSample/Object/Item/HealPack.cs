using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Factories;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Items
{
    public class HealPack : AbstractItem
    {
        private readonly float heal;
        public override Vector2 DrawPoint => Position;
        public override Vector2 Position => position;
        public HealPack()
        {
            sprite = SpriteFactory.CreateSprite(this.GetType());
            heal = 10;
            position = new Vector2(50,50);
            Physics = new Physics(new Vector2(0, -1), new Vector2(0, 0.1f));
        }

        public override IGraph HitBox => new GraphRectangle(Position.ToPoint(), new Point(18,18));
        public override void Update(GameTime gameTime)
        {
            position += Physics.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Collected(IPlayer player)
        {
            player.Heal(heal);
            base.Collected(player);
        }
    }
}
