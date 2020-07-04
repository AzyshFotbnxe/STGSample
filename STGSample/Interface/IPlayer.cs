using Microsoft.Xna.Framework;
using STGSample.Manager;
using STGSample.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample
{
    public interface IPlayer: IFlight
    {
        void Fire(List<IProjectile> bullets);
        int Weapon { get; }
        void UpgradeHealth(float level = 1);
        void UpgradeAttack(float level = 1);
        void UpgradeFireRate(float level = 1);
        void UpgradeWeapon(int level = 1);
        void UpgradeDefense(float level = 1);
        void AddState(IPlayerState newState);
        event Action PlayerDeathEvent;
    }
}
