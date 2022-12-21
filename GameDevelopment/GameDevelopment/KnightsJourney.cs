using GameDevelopment.Environment;
using GameDevelopment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevelopment
{
    public class KnightsJourney : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private bool firstUpdate = true;

        internal Level1 level1 = new Level1();
        internal Level2 level2 = new Level2();
        internal Level3 level3 = new Level3();
        internal Tutorial tutorial = new Tutorial();
        public WorldState worldState = WorldState.MainMenu;
        public KnightsJourney()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            Information.KnightsJourney= this;
            _graphics.PreferredBackBufferWidth = Information.screenWidth;
            _graphics.PreferredBackBufferHeight = Information.screenHeight;
            _graphics.ApplyChanges();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            MainMenu.LoadContent(Content);
            Death.LoadContent(Content);

            tutorial.LoadContent(Content);
            tutorial.AddHitboxes(GraphicsDevice);

            level1.LoadContent(Content);
            level1.AddHitboxes(GraphicsDevice);
                /*
            level2.LoadContent(Content);
            level2.AddHitboxes(GraphicsDevice);

            level3.LoadContent(Content);
            level3.AddHitboxes(GraphicsDevice);
                */
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                worldState= WorldState.MainMenu;
            
            if (worldState != WorldState.Death)
                Information.prevState = worldState;

            if (firstUpdate)
            {
                MainMenu.Update(gameTime);
                Death.Update(gameTime);
                level1.Update(gameTime);
                tutorial.Update(gameTime);
                
                firstUpdate= false;
            }
            else
            {
                switch (worldState)
                {
                    case WorldState.MainMenu:
                        MainMenu.Update(gameTime);
                        break;
                    case WorldState.Death:
                        Death.Update(gameTime);
                        break;
                    case WorldState.Level1:
                        level1.Update(gameTime);
                        break;
                    case WorldState.Level2:
                        break;
                    case WorldState.Level3:
                        break;
                    case WorldState.Won:
                        break;
                    case WorldState.Tutorial:
                        tutorial.Update(gameTime);
                        break;
                    default:
                        break;
                }
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            switch (worldState)
            {
                case WorldState.MainMenu:
                    MainMenu.Draw(_spriteBatch);
                    break;
                case WorldState.Death:
                    Death.Draw(_spriteBatch);
                    break;
                case WorldState.Level1:
                    level1.Draw(_spriteBatch);
                    break;
                case WorldState.Level2:
                    break;
                case WorldState.Level3:
                    break;
                case WorldState.Won:
                    break;
                case WorldState.Tutorial:
                    tutorial.Draw(_spriteBatch);
                    break;
                default:
                    break;
            }
            
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}