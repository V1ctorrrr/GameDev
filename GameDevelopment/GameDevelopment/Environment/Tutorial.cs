using GameDevelopment.Entity.Enemy;
using GameDevelopment.Entity.Character;
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
using GameDevelopment.Entity.PickUps;
using GameDevelopment.Input;

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
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 3, 2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };
        public Hero hero = new Hero(new Vector2(0, 620));
        public List<IEnemy> enemies = new List<IEnemy>();
        private SpriteFont spriteFont;
        private List<IPickUp> pickUps= new List<IPickUp>();
        public Tutorial()
        {
            enemies.Add(new Boar(new Vector2(1100, 900)));

            pickUps.Add(new Flag(new Vector2(1800, 925)));
            pickUps.Add(new Coin(new Vector2(200,990)));
            pickUps.Add(new Coin(new Vector2(1800, 500)));
            pickUps.Add(new JumpPotion(new Vector2(1400, 990)));
        }

        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            hero.AddHitboxes(GraphicsDevice);
            enemies[0].AddHitboxes(GraphicsDevice);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundImage, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight), Color.White);

            foreach (var block in blocks)
                block.Draw(spriteBatch);
            
            spriteBatch.DrawString(spriteFont, "Move with \narrow keys or Q/D", new Vector2(10, 800), Color.White);
            spriteBatch.DrawString(spriteFont, "Pickup coins to\ngain points", new Vector2(410, 900), Color.White);
            spriteBatch.DrawString(spriteFont, "Jump with \nup arrow key \nor space or Z", new Vector2(375, 600), Color.White);
            spriteBatch.DrawString(spriteFont, "Attack with ctrl\nEvade the boar's front\nonce you have attacked it \nas it will damage you", new Vector2(800, 700), Color.White);
            spriteBatch.DrawString(spriteFont, "Crouch with \ndown arrow key or S", new Vector2(1200, 450), Color.White);
            spriteBatch.DrawString(spriteFont, "Pickup potion to\njump higher", new Vector2(1300, 800), Color.White);
            spriteBatch.DrawString(spriteFont, "Touch flag to go\nto next level", new Vector2(1600, 650), Color.White);

            hero.Draw(spriteBatch);
            enemies[0].Draw(spriteBatch);

            foreach (var pickUp in pickUps)
                pickUp.Draw(spriteBatch);

        }

        public void LoadContent(ContentManager Content)
        {
            hero.LoadContent(Content);
            enemies[0].LoadContent(Content);
            tileSet = Content.Load<Texture2D>("Tiles/Tiles");
            _backgroundImage = Content.Load<Texture2D>("Background/Background");
            spriteFont = Content.Load<SpriteFont>("UI/Fonts/Font");
            blocks = BlockFactory.CreateBlocks(gameBoard, blocks, tileSet);

            foreach (var pickUp in pickUps)
                pickUp.LoadContent(Content);
        }

        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            enemies[0].Update(gameTime);

            Collision.Collide(enemies[0], hero);
            foreach (var pickUp in pickUps)
            {
                pickUp.Update(gameTime);
                Collision.Collide(hero, pickUp);
            }

            foreach (var block in blocks)
            {
                Collision.Collide(hero, block);
                Collision.Collide(enemies[0], block);
            }
        }
    }
}
