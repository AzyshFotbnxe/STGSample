using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample
{
    public interface IFlight : IGameObject
    {
        bool IsAlive { get; }
        float MaxHealth { get; }
        float Health { get; }
        float Attack { get; }
        float FireRate { get; }
        float Defense { get; }
        void Damaged(float damage);
        void Heal(float heal);
        void SetPosition(float x, float y);
        void Move(Vector2 direction);
    }
}
