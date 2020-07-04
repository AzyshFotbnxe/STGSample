using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample
{
    //Barrage class is a bullet machine, it will create enemy bullets and put them into the bullet list.
    //Some special barrage, such as tracer, is implemented by create an enemy.
    public interface IBarrage
    {
        bool IsValid { get; }
        void Update(GameTime gameTime);
        void Fire(List<IProjectile> bullets, List<IEnemy> enemies);
        void Follow(IGameObject target);
        void Destroy(); //Destroy all the bullets it created.
    }
}
