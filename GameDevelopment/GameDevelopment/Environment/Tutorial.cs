using GameDevelopment.Entity.Enemy;
using GameDevelopment.Entity.Hero;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Environment
{
    internal class Tutorial : ILevel
    {
        public List<Block> blocks { get; set; } = new List<Block>();
         private Texture2D tileSet;
        private Texture2D _backgroundImage;
        public int[,] gameBoard { get; set; } = new int[,] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 3, 2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };
        public Hero hero = new Hero(new Vector2(0, 620));
        public List<IEnemy> enemies = new List<IEnemy>();

        private SpriteFont spriteFont;
        private string text1;
        private string text2;
        private string text3;
        private string text4;
        private Vector2 textPosition;
        public Tutorial()
        {
            enemies.Add(new Boar(new Vector2(1100, 500)));
            textPosition = new Vector2(10, 500);
            text1 = "Move with \narrow keys or Q/D";
            text2 = "Jump with \nup arrow key \nor space or Z";
            text3 = "Attack with ctrl";
            text4 = "Crouch with \ndown arrow key or S";
        }

        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            hero.AddHitboxes(GraphicsDevice);
            enemies[0].AddHitboxes(GraphicsDevice);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundImage, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight), Color.White);
            spriteBatch.DrawString(spriteFont,text1,new Vector2(10,750),Color.White);
            spriteBatch.DrawString(spriteFont, text2, new Vector2(325, 600), Color.White);
            spriteBatch.DrawString(spriteFont, text3, new Vector2(800, 700), Color.White);
            spriteBatch.DrawString(spriteFont, text4, new Vector2(1200, 400), Color.White);
            hero.Draw(spriteBatch);

            enemies[0].Draw(spriteBatch);

            foreach (var block in blocks)
            {
                block.Draw(spriteBatch);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            hero.LoadContent(Content);
            enemies[0].LoadContent(Content);
            tileSet = Content.Load<Texture2D>("Tiles/Tiles");
            _backgroundImage = Content.Load<Texture2D>("Background/Background");
            spriteFont = Content.Load<SpriteFont>("UI/Fonts/Font");
            blocks = BlockFactory.CreateBlocks(gameBoard, blocks, tileSet);
        }

        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            enemies[0].Update(gameTime);
            Collision.Collide(enemies[0], hero);

            foreach (var block in blocks)
            {
                Collision.Collide(hero, block);
                Collision.Collide(enemies[0], block);
            }
        }
    }
}
