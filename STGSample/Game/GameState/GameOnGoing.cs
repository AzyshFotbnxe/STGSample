using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Bullets;
using STGSample.Enemies;
using STGSample.Items;
using STGSample.Players;
using STGSample.Utils;

namespace STGSample.GameState
{
    public class GameOnGoing : IGameState
    {
        private readonly STGMain stgGame;
        public int Score;
        private float switchState = 20;
        private Texture2D background;
        //private float upper = 2400;
        //private Rectangle resRect => new Rectangle((int)stgGame.Camera.LeftBound, (int)upper, 400, 600);
        //public int Coin {get; private set;}
        //public int Score {get; private set;}
        public GameOnGoing(STGMain game)
        {
            Score = 0;
            stgGame = game;
            //background = game.Content.Load<Texture2D>("background");

            Reset();
        }
        //float bulletDelay;
        public void Update(GameTime gameTime)
        {
            stgGame.Controller.Update(gameTime);
            stgGame.Objects.Update(gameTime);
            stgGame.Camera.Update();
            //upper -= 2;
            //if (upper <= 0) upper = 2400;
                        /*bulletDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (bulletDelay > 0.2)
                        {
                            bulletDelay -= 0.2f;
                            stgGame.Objects.AddEnemyBullet(new RotatingBullet(null, new Vector2(200, 300), new Vector2(200, 400), Vector2.Zero, 87f, 0, Color.White));
                        }*/
/*            if (switchState > 0) switchState = Math.Max((float)(switchState - gameTime.ElapsedGameTime.TotalSeconds), 0);
            if ((int)switchState == 0)
            {
                switchState = 20;
                stgGame.State = new GameBossEncounter(stgGame, new SampleBoss(new Vector2(100, 30), 1000, 20, 3, 0), this);
            }*/
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
/*            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(texture:background, destinationRectangle: new Rectangle(0, 0, 400, 600), sourceRectangle : resRect, color:Color.White, rotation:0, origin:Vector2.Zero, effects:SpriteEffects.None, layerDepth: 0.1f);
            spriteBatch.End();*/
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix:stgGame.Camera.Transform);
            stgGame.Objects.Draw(spriteBatch);
            spriteBatch.Draw(stgGame.Content.Load<Texture2D>("statepanelbackground"), new Vector2(stgGame.Camera.RightBound, 0),null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth: 0.99f);
            spriteBatch.DrawString(ConstantsTable.FONT, "HEA  "+stgGame.Player.Health.ToString("0.00") + "/" + stgGame.Player.MaxHealth.ToString("0.00"), new Vector2(stgGame.Camera.RightBound + 10, 10), Color.Red,0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth: 1f);
            spriteBatch.DrawString(ConstantsTable.FONT, "ATK  " + stgGame.Player.Attack , new Vector2(stgGame.Camera.RightBound + 10, 40), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth: 1f);
            spriteBatch.DrawString(ConstantsTable.FONT, "DEF  " + stgGame.Player.Defense, new Vector2(stgGame.Camera.RightBound + 10, 70), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth: 1f);
            spriteBatch.DrawString(ConstantsTable.FONT, "F.RT  " + stgGame.Player.FireRate, new Vector2(stgGame.Camera.RightBound + 10, 100), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth: 1f);
            spriteBatch.DrawString(ConstantsTable.FONT, "Score  " + Score.ToString("0000000000"), new Vector2(stgGame.Camera.RightBound + 10, 300), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        public void Pause()
        {
            stgGame.State = new GamePause(stgGame, this);
        }
        public void Reset()
        {
            var player = new Player(new Vector2(200, 500), stgGame.Archive);
            player.PlayerDeathEvent += PlayerDie;
            switchState = 3;
            stgGame.Objects.Reset(player);
            stgGame.Camera.SetFocus(player);
            /*            for (int i = 0; i < 10; i++)
                        {
                            var enemy = new NormalEnemy(new Vector2(150 + i * 25, 200), 20, 2, 0, 0);
                            enemy.DeathEvent += EnemyDie;
                            stgGame.Objects.AddEnemy(enemy);
                        }*/
            stgGame.Objects.AddEnemy(new Tracer(Vector2.Zero, stgGame.Player));
            stgGame.Objects.AddItem(new HealPack());
        }
        private void PlayerDie()
        {
            stgGame.Archive.Balance += Score / 1000;
            stgGame.State = new GameLeaving(stgGame, this);
        }
        private void EnemyDie(IEnemy enemy)
        {
            Score += 200; // feel free to change this method.
        }
    }
}
