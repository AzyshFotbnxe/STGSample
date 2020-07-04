using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STGSample.Loading;

namespace STGSample.GameState
{
    public class GameUpgrade : IGameState
    {
        private readonly List<Button> Button;
        private GraphicsDevice Graphics => stgGame.GraphicsDevice;
        private SpriteFont Font => ConstantsTable.FONT;
        private PlayerArchive Archive => stgGame.Archive;
        private readonly STGMain stgGame;
        public GameUpgrade(STGMain game)
        {
            this.stgGame = game;
            game.IsMouseVisible = true;

            var attack = new Button(Font, "Upgrade Attack to " + (Archive.Attack + 1), new Vector2(100, 240));
            attack.Click += AttackClick;

            var health = new Button(Font, "Upgrade Health to " + (Archive.Health + 10), new Vector2(100, 290));
            health.Click += HealthClick;

            var Defense = new Button(Font, "Upgrade Defense to " + (Archive.Defense + 1), new Vector2(100, 340));
            Defense.Click += DefenseClick;
            
            var FireRate = new Button(Font, "Upgrade FireRate to " + (Archive.FireRate + 1), new Vector2(100, 390));
            FireRate.Click += FireRateClick;
            
            var Weapon = new Button(Font, "Upgrade Weapon to " + (Archive.Weapon + 1), new Vector2(100, 440));
            Weapon.Click += WeaponClick;

            var back = new Button(Font, "Back to menu", new Vector2(300, 550));
            back.Click += BackClick;

            Button = new List<Button>
            {
                attack,
                health,
                Defense,
                FireRate,
                Weapon,
                back
            };
        }
        private void AttackClick(Button button)
        {
            Archive.Attack += 1;
            button.ChangeText("Upgrade Attack to " + (Archive.Attack + 1));
        }
        private void HealthClick(Button button)
        {
            Archive.Health += 10;
            button.ChangeText("Upgrade Health to " + (Archive.Health + 10));
        }
        private void DefenseClick(Button button)
        {
            Archive.Defense += 1;
            button.ChangeText("Upgrade Defense to " + (Archive.Defense + 1));
        }
        private void FireRateClick(Button button)
        {
            Archive.FireRate += 1;
            button.ChangeText("Upgrade FireRate to " + (Archive.FireRate + 1));
        }
        private void WeaponClick(Button button)
        {
            Archive.Weapon += 1;
            button.ChangeText("Upgrade Weapon to " + (Archive.Weapon + 1));
        }
        private void BackClick(Button button)
        {
            XMLUtils.WriteSav(Archive);
            stgGame.State = new GameMenu(stgGame);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Graphics.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            //graphics.Clear(Color.AliceBlue);
            spriteBatch.DrawString(Font, "Balance " + Archive.Balance, new Vector2(300, 100), Color.Black);
            //spriteBatch.Draw(marioTitle, Vector2.Zero, Color.White);
            foreach (var ele in Button)
                ele.Draw(spriteBatch);

        }

        public void Update(GameTime gameTime)
        {
            foreach (var ele in Button)
                ele.Update(gameTime);
        }

        public void Pause()
        {
            // Do Nothing
        }
        public void Reset() { }
    }
}

