using GameDevelopment.Entity.Character;
using GameDevelopment.Entity.Enemy;
using GameDevelopment.Entity.PickUps;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Input;
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
    internal class Level3 : ILevel
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
            { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };

        private Hero hero = new Hero(new Vector2(5, 200));
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<IPickUp> pickUps = new List<IPickUp>();

        public Level3()
        {
            

            pickUps.Add(new Flag(new Vector2(1850, 930)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundImage, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight), Color.White);
            foreach (var block in blocks)
                block.Draw(spriteBatch);

            foreach (var enemy in enemies)
                enemy.Draw(spriteBatch);

            foreach (var pickup in pickUps)
                pickup.Draw(spriteBatch);

            hero.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
                Collision.Collide(enemy, hero);
            }

            foreach (var pickup in pickUps)
            {
                pickup.Update(gameTime);
                Collision.Collide(hero, pickup);
            }
            foreach (var block in blocks)
            {
                Collision.Collide(hero, block);
                foreach (var enemy in enemies)
                {
                    Collision.Collide(enemy, block);
                }
            }
        }

        public void LoadContent(ContentManager Content)
        {
            hero.LoadContent(Content);
            tileSet = Content.Load<Texture2D>("Tiles/Tiles");
            _backgroundImage = Content.Load<Texture2D>("Background/Level3/BIGPRE_ORIG_SIZE");

            foreach (var enemy in enemies)
                enemy.LoadContent(Content);

            foreach (var pickup in pickUps)
                pickup.LoadContent(Content);

            blocks = BlockFactory.CreateBlocks(gameBoard, blocks, tileSet);
        }

        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            hero.AddHitboxes(GraphicsDevice);
            foreach (var enemy in enemies)
            {
                enemy.AddHitboxes(GraphicsDevice);
            }
        }
    }
}
