using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Bullets;
using STGSample.Items;
using STGSample.Players;
using STGSample.Utils;

namespace STGSample.GameState
{
    public class GameBossEncounter : IGameState
    {
        private readonly STGMain stgGame;
        private IEnemy boss;
        private GameOnGoing ongoing;
        //public int Coin {get; private set;}
        //public int Score {get; private set;}
        public GameBossEncounter(STGMain game, IEnemy boss, GameOnGoing ongoing)
        {
            stgGame = game;
            this.boss = boss; //if boss die, the game goes back to ongoing state. In boss state, no "normal" enemy will be created.
            this.ongoing = ongoing;
            boss.DeathEvent += EnemyDie;
            game.Objects.AddEnemy(boss);
            //Reset();
        }
        //float bulletDelay;
        public void Update(GameTime gameTime)
        {
            stgGame.Controller.Update(gameTime);
            stgGame.Objects.Update(gameTime);
            stgGame.Camera.Update();
/*            bulletDelay += 1;
            if(bulletDelay >= 90)
            {
                bulletDelay = 0;
                for(int i = 0; i < 20; i++)
                {
                    stgGame.Objects.AddEnemyBullet(new EnemyBullet(null, new Vector2(0, 60 * i), new Vector2(5,0), Vector2.Zero, 0, Color.White));
                    stgGame.Objects.AddEnemyBullet(new EnemyBullet(null, new Vector2(60 * i, 0), new Vector2(0, 5), Vector2.Zero, 0, Color.White));
                    for (int j = 0; j < 50; j++)
                    {
                        stgGame.Objects.AddEnemyBullet(new EnemyBullet(null, new Vector2(300, 100), new Vector2((float)(ConstantTable.RANDOM.NextDouble() - 0.5) * 20, (float)(ConstantTable.RANDOM.NextDouble() - 0.5) * 20), Vector2.Zero, 1, Color.White));
                    }
                }
            }*/

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            ongoing.Draw(spriteBatch);
            spriteBatch.DrawString(ConstantsTable.FONT, "BOSS", new Vector2(stgGame.Camera.RightBound + 10, 400), Color.Red, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
        }

        public void Pause()
        {
            stgGame.State = new GamePause(stgGame, this);
        }

        public void Reset()
        {
            var player = new Player(new Vector2(200, 500), stgGame.Archive);
            player.PlayerDeathEvent += PlayerDie;
            stgGame.Objects.Reset(player);
            stgGame.Camera.SetFocus(player);
            stgGame.Objects.AddItem(new HealPack());
        }

        private void PlayerDie()
        {
            stgGame.Archive.Balance += 10;
            stgGame.State = new GameLeaving(stgGame, this);
        }

        private void EnemyDie(IEnemy enemy)
        {
            ongoing.Score += 100000;
            stgGame.State = ongoing;
        }
    }
}
