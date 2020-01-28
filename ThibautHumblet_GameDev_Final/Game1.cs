using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;
using ThibautHumblet_GameDev_Final.Animations;
using ThibautHumblet_GameDev_Final.Cameras;
using ThibautHumblet_GameDev_Final.Maps;
using ThibautHumblet_GameDev_Final.Sounds;
using ThibautHumblet_GameDev_Final.Sprites;
using ThibautHumblet_GameDev_Final.UserInterface;

namespace ThibautHumblet_GameDev_Final
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // display
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        // achtergrond parralax scrolling
        Texture2D laag01, laag02, laag03, laag04, laag05, laag06, laag11;
        static public Vector2 AchtergrondPositie;

        // input
        Input input;

        private State _state;
        private GameModel _gameModel;
        private LevelModel _level;

        public static int Level = 0;

        static public bool mainMenu = true;
        static public bool gewonnen = false;

        public static Player Player;
        public static Vector2 StartingPosition = new Vector2(1500,0);

        private Camera _camera;

        private Menu menu;

        public Game1()
        {
            int basisSchermBreedte = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int basisSchermHoogte = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics = new GraphicsDeviceManager(this)
            {
            };
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
            // TODO: Add your initialization logic here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            input = new Input();
            menu = new Menu(ScreenWidth, ScreenHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera();

            // TODO: use this.Content to load your game content here

            _gameModel = new GameModel()
            {
                ContentManger = Content,
                GraphicsDeviceManager = graphics,
                SpriteBatch = spriteBatch,
            };

            // player inladen
            Player = new Player(input, new Dictionary<string, Animation>()
      {
                { "Walk", new Animation(Content.Load<Texture2D>("SpritesheetWalk"), 10, 0.1f, true) },
                { "Run", new Animation(Content.Load<Texture2D>("SpritesheetRun"), 8) },
                { "Idle", new Animation(Content.Load<Texture2D>("SpritesheetIdle"), 10) },
                { "JumpStart", new Animation(Content.Load<Texture2D>("SpritesheetJumpStart"), 5) },
                { "JumpEnd", new Animation(Content.Load<Texture2D>("SpritesheetJumpEnd"), 4) },
                { "Dead", new Animation(Content.Load<Texture2D>("SpritesheetDead"), 8, 0.17f, false) }
      })
        {
            Position = StartingPosition,
            Layer = 1f,
        };

            _level = new LevelModel(Player);

        // parallax achtergrond inladen
            laag01 = Content.Load<Texture2D>("_01_ground");
            laag02 = Content.Load<Texture2D>("_02_trees and bushes");
            laag03 = Content.Load<Texture2D>("_03_distant_trees");
            laag04 = Content.Load<Texture2D>("_04_bushes");
            laag05 = Content.Load<Texture2D>("_05_hill1");
            laag06 = Content.Load<Texture2D>("_06_hill2");
            laag11 = Content.Load<Texture2D>("_11_background");

            menu.Load(Content);

            Sound.Load(Content);
            MediaPlayer.Play(Sound.music);
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
            input.Update();
            if (mainMenu && input.Keypress(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _camera.Follow(Player);

            _state = new PlayingState(_gameModel, _level);
                    _state.LoadContent();

            _state.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            menu.Draw(spriteBatch);
            spriteBatch.End();

            if (!mainMenu)
            {
                #region Parralax Background
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap); // dit zorgt ervoor dat de achtergrond eindeloos blijft doorscrollen
                spriteBatch.Draw(laag11, new Rectangle(0, -700, laag11.Width, laag11.Height), Color.White);
                spriteBatch.Draw(laag06, new Rectangle(0, -700, laag06.Width, laag06.Height), Color.White);
                spriteBatch.Draw(laag05, new Rectangle(0, -700, laag05.Width, laag05.Height), Color.White);
                spriteBatch.Draw(laag04, new Rectangle((int)(-AchtergrondPositie.X * 0.45f), -700, laag04.Width, laag04.Height), Color.White);
                spriteBatch.Draw(laag03, new Rectangle((int)(-AchtergrondPositie.X * 0.5f), -700, laag03.Width, laag03.Height), Color.White);
                spriteBatch.Draw(laag02, new Rectangle((int)(-AchtergrondPositie.X * 0.6f), -700, laag02.Width, laag02.Height), Color.White);
                spriteBatch.Draw(laag01, new Rectangle((int)(-AchtergrondPositie.X), -700, laag01.Width, laag01.Height), Color.White);
                spriteBatch.End();
                #endregion

                spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
                _state.Draw(gameTime);
                spriteBatch.End();
            }



            base.Draw(gameTime);

        }
    }
}
