using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Players
{
    public interface IPlayerState
    {
        Player Player { get; }
        IPlayerState PrevState { get; set; }
        IPlayerState NextState { get; set; } //Make it a linkedlist so it allows multiple states. 
        //Bool here means if states will be cummulated.
        bool Damaged(float damage);
        bool Update(GameTime gameTime);
        bool Fire(List<IProjectile> bullets);
    }
}
