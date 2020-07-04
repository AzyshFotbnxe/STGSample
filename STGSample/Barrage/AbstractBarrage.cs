using Microsoft.Xna.Framework;
using STGSample.Bullets;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Barrage
{
    public abstract class AbstractBarrage : IBarrage
    {
        protected IEnemy creator;
        protected int amount; //The amount of Fire being called, not the amount of bullets created.
        protected float gap;
        protected Vector2 position;
        protected float damage;
        protected Type bulletType;
        protected IGameObject follows;
        protected List<IProjectile> createdBullets = new List<IProjectile>();
        public bool IsValid => amount != 0;
        protected float fireDelay;
        protected float lifeTime;
        public AbstractBarrage() { }
        public AbstractBarrage(IEnemy creator, int amount, float gap, Vector2 position, float damage, Type bulletType = null, float lifeTime = -1)
        {
            this.creator = creator;
            this.amount = amount;
            this.gap = gap;
            this.position = position;
            this.damage = damage;
            this.bulletType = bulletType;
            this.lifeTime = lifeTime;
            if (bulletType is null) this.bulletType = typeof(EnemyBullet);
        }
        public virtual void Update(GameTime gameTime)
        {
            if (!(follows is null)) position = follows.Position;
            fireDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            for(int i = createdBullets.Count - 1; i >= 0; i--)
            {
                if (createdBullets[i].IsDestroyed) createdBullets.RemoveAt(i);
            }
        }
        public virtual void Fire(List<IProjectile> bullets, List<IEnemy> enemies) { }
        public virtual void Follow(IGameObject target = null)
        {
            follows = target;
        }

        public virtual void Destroy()
        {
            foreach (var bullet in createdBullets) bullet.IsDestroyed = true;
            createdBullets.Clear();
        }
    }
}
