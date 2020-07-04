using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using STGSample.Factories;
using STGSample.Manager;
using STGSample.Utils;
using STGSample.Players;
using System;
using STGSample.Bullets;
using STGSample.Test;
using STGSample.Enemies;
using STGSample.Controller;
using STGSample.GameState;
using System.IO;
using STGSample.Loading;

namespace STGSample
{
    //public enum ObjectState { Normal = 0, NonCollidable, Destroy } /* Not used yet */
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class STGMain : Game
    {
        public int WindowHeight { get => Graphics.PreferredBackBufferHeight; private set { Graphics.PreferredBackBufferHeight = value; Graphics.ApplyChanges(); } }
        public int WindowWidth { get => Graphics.PreferredBackBufferWidth; private set { Graphics.PreferredBackBufferWidth = value; Graphics.ApplyChanges(); } }
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch spriteBatch { get; private set; }
        public ObjectsManager Objects { get; private set; }
        public IController Controller { get; private set; }
        public IGameState State { get; set; }
        public IPlayer Player => Objects.Player;
        public Camera Camera { get; private set; }
        public PlayerArchive Archive;
        public STGMain()
        {
            Graphics = new GraphicsDeviceManager(this);
            WindowWidth = 700;
            WindowHeight = 600;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SpriteFactory.Initialize(Content);
            ConstantsTable.FONT = Content.Load<SpriteFont>("Font/mariofont");
            Camera = new Camera();
            Controller = new KeyboardController(this);
            Objects = new ObjectsManager(this);
            Archive = XMLUtils.ReadSav();
            State = new GameMenu(this);
            ResetGame();
            base.Initialize();
        }
        public void ResetGame()
        {
            State.Reset();
            //Objects.AddEnemyBullet(new EnemyBullet(null, new Vector2(100, 100), new Vector2(), new Vector2(), 10, Color.Red));

            /*Objects.AddItem(new TestCircle(new Vector2(200, 200), 100));*/
            //Objects.AddEnemy(new Tracer(Vector2.Zero, Player));
            
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            State.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            State.Draw(spriteBatch);
            // TODO: Add your drawing code here
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
