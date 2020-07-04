using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STGSample.Manager.CollisionHandle;
using static STGSample.Utils.ConstantsTable;

namespace STGSample.Manager
{
    public class ObjectsManager
    {
        public IPlayer Player { get; private set; }
        private readonly Texture2D debugbox;
        private readonly List<IProjectile> EnemyBullets;
        private readonly List<IProjectile> MyBullets;
        private readonly List<IItem> Items;
        private readonly List<IEnemy> Enemies;
        private readonly List<IGameObject> NonCollidables;
        public ObjectsManager(STGMain mainGame)
        {
            EnemyBullets = new List<IProjectile>();
            MyBullets = new List<IProjectile>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>();
            NonCollidables = new List<IGameObject>();
            debugbox = mainGame.Content.Load<Texture2D>("debugBox");
        }
        public void Reset(IPlayer player)
        {
            EnemyBullets.Clear();
            MyBullets.Clear();
            Items.Clear();
            Enemies.Clear();
            NonCollidables.Clear();
            Player = player;
        }
        public void AddBullet(IProjectile bullet)
        {
            MyBullets.Add(bullet);
        }
        public void AddEnemyBullet(IProjectile bullet)
        {
            EnemyBullets.Add(bullet);
        }
        public void AddItem(IItem item)
        {
            Items.Add(item);
        }
        public void AddNonCollidable(IGameObject obj)
        {
            NonCollidables.Add(obj);
        }
        public void AddEnemy(IEnemy enemy)
        {
            Enemies.Add(enemy);
        }
        public void Update(GameTime gameTime)
        {
            PlayerUpdate(gameTime);
            Enemies.ForEach(o => EnemyUpdate(o, gameTime));

            EnemyBullets.ForEach(o => o.Update(gameTime));
            MyBullets.ForEach(o => o.Update(gameTime));
            Items.ForEach(o => o.Update(gameTime));
            NonCollidables.ForEach(o => o.Update(gameTime));

            EnemyBullets.ForEach(o => HandleCollision(Player, o));
            Enemies.ForEach(o => MyBullets.ForEach(b => HandleCollision(o, b)));
            Items.ForEach(o => HandleCollision(Player, o));
            Enemies.ForEach(o => HandleCollision(Player, o));

            EnemyBullets.ForEach(o => DestroyCheck(o));
            Enemies.ForEach(o => DestroyCheck(o));
            MyBullets.ForEach(o => DestroyCheck(o));
            Items.ForEach(o => DestroyCheck(o));
            NonCollidables.ForEach(o => DestroyCheck(o));

            for (int i = Items.Count -1; i>=0; i--)
            {
                if (Items[i].IsDestroyed) Items.RemoveAt(i);
            }
            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                if (Enemies[i].IsDestroyed) Enemies.RemoveAt(i);
            }
            for (int i = MyBullets.Count - 1; i >= 0; i--)
            {
                if (MyBullets[i].IsDestroyed) MyBullets.RemoveAt(i);
            }
            for (int i = EnemyBullets.Count - 1; i >= 0; i--)
            {
                if (EnemyBullets[i].IsDestroyed) EnemyBullets.RemoveAt(i);
            }
            for(int i = NonCollidables.Count - 1; i >= 0; i--)
            {
                if (NonCollidables[i].IsDestroyed) NonCollidables.RemoveAt(i);
            }
        }

        private void EnemyUpdate(IEnemy enemy, GameTime gameTime)
        {
            enemy.Update(gameTime);
            enemy.Fire(EnemyBullets, Enemies);
        }

        private void PlayerUpdate(GameTime gameTime)
        {
            Player.Update(gameTime);
            Player.Fire(MyBullets);
            Rectangle hitbox = Player.DrawBox;
            int top = hitbox.Top, bottom = hitbox.Bottom, left = hitbox.Left, right = hitbox.Right;
            float x = Player.Position.X, y = Player.Position.Y;
            if (top < 0) y -= top;
            if (bottom > BATTLEAREAHEIGHT) y -= bottom - BATTLEAREAHEIGHT;
            if (left < 0) x -= left;
            if (right > BATTLEAREAWIDTH) x -= right - BATTLEAREAWIDTH;
            Player.SetPosition(x, y);
        }

        private void DestroyCheck(IGameObject obj)
        {
            if (obj.Position.X > BATTLEAREAWIDTH + DESTROYMARGIN) obj.IsDestroyed = true; //Right bound
            if (obj.Position.X < -DESTROYMARGIN) obj.IsDestroyed = true; //Left bound
            if (obj.Position.Y > BATTLEAREAHEIGHT + DESTROYMARGIN) obj.IsDestroyed = true; //Lower bound
            if (obj.Position.Y < -DESTROYMARGIN) obj.IsDestroyed = true; //Upper bound
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            Enemies.ForEach(o => o.Draw(spriteBatch));
            EnemyBullets.ForEach(o => o.Draw(spriteBatch));
            MyBullets.ForEach(o => o.Draw(spriteBatch));
            Items.ForEach(o => o.Draw(spriteBatch));
            NonCollidables.ForEach(o => o.Draw(spriteBatch));
            //DebugDrawHitbox(spriteBatch);
        }
        private void DebugDrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(debugbox, Player.DrawBox, null, Color.White);
            spriteBatch.Draw(debugbox, Player.HitBox.BoundingBox, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f); //Player has two boxes.
            EnemyBullets.ForEach(o => spriteBatch.Draw(debugbox, o.DrawBox, null, Color.White));
            Enemies.ForEach(o => spriteBatch.Draw(debugbox, o.DrawBox, null, Color.White));
            MyBullets.ForEach(o => spriteBatch.Draw(debugbox, o.DrawBox, null, Color.White));
            Items.ForEach(o => spriteBatch.Draw(debugbox, o.DrawBox, null, Color.White));
            NonCollidables.ForEach(o => spriteBatch.Draw(debugbox, o.DrawBox, null, Color.White));
        }
    }
}
