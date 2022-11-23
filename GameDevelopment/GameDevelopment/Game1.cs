using GameDevelopment.animations;
using GameDevelopment.Entity;
using GameDevelopment.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.MediaFoundation;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace GameDevelopment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Hero
        private Hero hero;
        //First texture = idle, then run, then jump, then fall,then crouch, t crouch walk, then attack, then crouch attack
        private List<Texture2D> _heroTextures = new List<Texture2D>();
        private KeyboardReader keyboard;

        //Block
        private Texture2D blockTexture;
        private Block greenBlock;

        //collisionbox of hero
        private Block blockHero;
        private Rectangle futureBlock;
        private static Color color;

        //boundingbox
        private int heroWidth;
        private int heroHeight;
        private Vector2 heroBlockPosition;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            keyboard= new KeyboardReader();
            hero = new Hero(/*_heroRunTexture, _heroIdleTexture, _heroAttackTexture*/_heroTextures, keyboard);
            
            greenBlock = new Block(blockTexture, new Rectangle(250,0, 150,80),Color.Green);
            heroBlockPosition = new Vector2((int)hero.Position.X + 41, (int)hero.Position.Y+41);
            heroWidth = (/*_heroRunTexture.Width*/_heroTextures[1].Width/10)-90;
            heroHeight = /*_heroRunTexture.Height*/_heroTextures[1].Height -41;
            blockHero = new Block(blockTexture, new Rectangle((int)heroBlockPosition.X, (int)heroBlockPosition.Y, heroWidth, heroHeight), Color.Red);
            futureBlock = blockHero.Rectangle;

            color = Color.CornflowerBlue;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _heroTextures.Add(Content.Load<Texture2D>("_Idle"));
            _heroTextures.Add(Content.Load<Texture2D>("_Run"));
            _heroTextures.Add(Content.Load<Texture2D>("_Jump"));
            _heroTextures.Add(Content.Load<Texture2D>("_Fall"));
            _heroTextures.Add(Content.Load<Texture2D>("_CrouchAll"));
            _heroTextures.Add(Content.Load<Texture2D>("_CrouchWalk"));
            _heroTextures.Add(Content.Load<Texture2D>("_Attack"));
            _heroTextures.Add(Content.Load<Texture2D>("_CrouchAttack"));

            blockTexture = new Texture2D(GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var tempPosition = hero.Position;
            hero.Update(gameTime);
            heroBlockPosition.X = hero.Position.X+41;
            heroBlockPosition.Y = hero.Position.Y+41;
            futureBlock = new Rectangle((int)heroBlockPosition.X, (int)heroBlockPosition.Y,heroWidth, heroHeight);


            if (futureBlock.Intersects(greenBlock.Rectangle))
            {
                color = Color.Black;
                hero.Position = tempPosition;
            }
            else
            {
                blockHero.Rectangle = new Rectangle((int)heroBlockPosition.X, (int)heroBlockPosition.Y,heroWidth, heroHeight);
                color = Color.CornflowerBlue;
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            _spriteBatch.Begin();

            hero.Draw(_spriteBatch);
            greenBlock.Draw(_spriteBatch);
            //blockHero.Draw(_spriteBatch);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}