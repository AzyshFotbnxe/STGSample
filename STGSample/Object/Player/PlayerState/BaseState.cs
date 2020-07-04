using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using STGSample.Bullets;

namespace STGSample.Players
{
    public class BaseState : IPlayerState
    {
        public Player Player { get; private set; }
        public virtual IPlayerState PrevState { get => null; set => throw new NullReferenceException("Base state should not be set a previous state"); } //Base state does not have any prev state.
        public virtual IPlayerState NextState { get; set; }
        private float fireDelay;
        public BaseState(Player player)
        {
            Player = player;
        }
        public virtual bool Damaged(float damage)
        {
            bool shouldCheck = true;
            if (!(NextState is null)) shouldCheck = NextState.Damaged(damage);
            if (shouldCheck)
            {
                float loseHealth = Math.Max(damage - Player.Defense, damage * 0.1f); //At least each bullet make 10% damage.
                Player.Health -= loseHealth;
            }
            return true;
        }

        public virtual bool Fire(List<IProjectile> bullets)
        {
            bool shouldCheck = true;
            if (!(NextState is null)) shouldCheck = NextState.Fire(bullets);
            if (shouldCheck)
            {
                float fireGap = (Player.FireRate + 2) / 2;
                float damage = (float)(Player.Attack / Player.Weapon * (0.5 + 0.5 * Player.Weapon));
                float fireVelocity = 1 + 0.2f * Player.FireRate;
                int straightBullet = Player.Weapon;
                fireDelay -= fireGap;
                if (fireDelay <= 0)
                {
                    fireDelay = 24;
                    if (straightBullet > 2)
                    {
                        straightBullet -= 2;
                        bullets.Add(new FireBall(Player, new Vector2(Player.DrawPoint.X, Player.DrawPoint.Y - 20), new Vector2(-90, -120) * fireVelocity, new Vector2(0, 0), damage, Color.White));
                        bullets.Add(new FireBall(Player, new Vector2(Player.DrawPoint.X + 30, Player.DrawPoint.Y - 20), new Vector2(90, -120) * fireVelocity, new Vector2(0, 0), damage, Color.White));
                    }
                    float part = 30 / straightBullet;
                    for (int i = 0; i < straightBullet; i++)
                    {
                        bullets.Add(new Bullet(Player, new Vector2((float)(Player.DrawPoint.X + part * (i + 0.5)), Player.DrawPoint.Y - 20), new Vector2(0, -150) * fireVelocity, new Vector2(0, 0), damage, Color.White));
                    }
                }
            }
            return true;
        }

        public virtual bool Update(GameTime gameTime)
        {
            //For update, if a state has a timer, remember to put the timer operation outside the if-else clause.
            bool shouldCheck = true;
            if (!(NextState is null)) shouldCheck = NextState.Update(gameTime);
            if (shouldCheck) {
                //Put what the state really does here. For example, get HOT or DOT.
            }
            return true;
        }
    }
}
