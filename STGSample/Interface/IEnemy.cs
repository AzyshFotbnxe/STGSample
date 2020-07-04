using System;
using System.Collections.Generic;
using STGSample.Utils;

namespace STGSample
{
    public interface IEnemy : IFlight
    {
        void Fire(List<IProjectile> bullets, List<IEnemy> enemies);
        Physics Physics { get; }
        void HitPlayer(IPlayer player);
        event Action<IEnemy> DeathEvent;
    }
}
