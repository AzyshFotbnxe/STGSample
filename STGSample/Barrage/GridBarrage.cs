using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using STGSample.Bullets;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Barrage
{
    public class GridBarrage : AbstractBarrage
    {
        private readonly List<LineBarrage> lines = new List<LineBarrage>();
        public GridBarrage(IEnemy creator, int amount, float gap, float velocity, int rows, int columns, float damage, Type bulletType = null)
        {
            this.amount = amount;
            if (bulletType is null) this.bulletType = typeof(EnemyBullet);

            float rowDistance = ConstantsTable.BATTLEAREAHEIGHT;
            if (rows > 1)  rowDistance /= (float)(rows-1);
            for(int i = 0; i < rows; i++)
            {
                lines.Add(new LineBarrage(creator, amount,gap, new Vector2(velocity, 0), Vector2.Zero, new Vector2(0, i*rowDistance), damage, bulletType));
            }

            float columnDistance = ConstantsTable.BATTLEAREAWIDTH;
            if(columns > 1) columnDistance /= (float)(columns-1);
            for (int i = 0; i < rows; i++)
            {
                lines.Add(new LineBarrage(creator, amount, gap, new Vector2(0 ,velocity), Vector2.Zero, new Vector2(i * columnDistance, 0), damage, bulletType));
            }
        }
        public override void Fire(List<IProjectile> bullets, List<IEnemy> enemies)
        {
            foreach (var line in lines) line.Fire(bullets, enemies);
            if (IsValid && fireDelay > gap)
            {
                fireDelay -= gap;
                amount--;
            }
        }

        public override void Update(GameTime gameTime)
        {
            fireDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var line in lines) line.Update(gameTime);
        }

        public override void Destroy()
        {
            foreach (var line in lines) line.Destroy();
        }
    }
}
