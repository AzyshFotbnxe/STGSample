using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample
{
    public interface IProjectile : IGameObject
    {
        float Damage { get; }
        void HitTarget();
        Physics Physics { get; }
    }
}
