using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Manager
{
    public static class CollisionHandle
    {
        public static void HandleCollision(IPlayer player, IProjectile bullet)
        {
            if (!player.HitBox.Intersects(bullet.HitBox)) return;
            player.Damaged(bullet.Damage);
            bullet.HitTarget();
        }

        public static void HandleCollision(IPlayer player, IItem item)
        {
            var hb1 = player.DrawBox;
            var hb2 = item.HitBox;
            if (!hb2.Intersects(hb1)) return;
            item.Collected(player);
        }

        public static void HandleCollision(IPlayer player, IEnemy enemy)
        {
            enemy.HitPlayer(player);
        }

        public static void HandleCollision(IEnemy enemy, IProjectile bullet)
        {
            if (!enemy.HitBox.Intersects(bullet.HitBox)) return;
            enemy.Damaged(bullet.Damage);
            bullet.HitTarget();
        }
    }
}
