using GameDevelopment.Entity.Enemy;
using GameDevelopment.Entity.Character;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameDevelopment.Entity;
using GameDevelopment.Entity.PickUps;
using GameDevelopment.Input;

namespace GameDevelopment.Environment
{
    internal class Level1: ILevel
    {
        public List<Block> blocks { get; set; }= new List<Block>();
        private Texture2D tileSet;
        private Texture2D _backgroundImage;
        public int[,] gameBoard { get; set; } = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 2, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3, 3, 3},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3},
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3},
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},
            { 1, 1, 1, 1, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3},
            { 3, 3, 3, 3, 0, 0, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3},
            { 3, 3, 3, 3, 0, 0, 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3},
        };

        public Hero hero = new Hero(new Vector2(0,620));
        public List<IEnemy> enemies= new List<IEnemy>();
        public List<IPickUp> pickUps= new List<IPickUp>();
        public Level1()
        {
            enemies.Add(new Boar(new Vector2(1040,400)));
            enemies.Add(new Skeleton(new Vector2(1600, 880)));

            pickUps.Add(new Flag(new Vector2(1850,285)));
            pickUps.Add(new Coin(new Vector2(10, 100)));
            pickUps.Add(new Coin(new Vector2(60, 100)));
            pickUps.Add(new Coin(new Vector2(110, 100)));
            pickUps.Add(new Coin(new Vector2(160, 100)));
            pickUps.Add(new Coin(new Vector2(420, 750)));
            pickUps.Add(new Coin(new Vector2(1700,900)));
            pickUps.Add(new Coin(new Vector2(1650, 900)));
            pickUps.Add(new Coin(new Vector2(1700, 950)));
            pickUps.Add(new Coin(new Vector2(1650, 950)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundImage, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight), Color.White);
            foreach (var pickup in pickUps)
                pickup.Draw(spriteBatch);

            foreach (var enemy in enemies)
                enemy.Draw(spriteBatch);

            foreach (var block in blocks)
                block.Draw(spriteBatch);

            hero.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
                Collision.Collide(enemy,hero);
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
                    Collision.Collide(enemy,block);
                }
            }
        }

        public void LoadContent(ContentManager Content)
        {
            hero.LoadContent(Content);
            foreach (var enemy in enemies)
            {
                enemy.LoadContent(Content);
            }
            tileSet = Content.Load<Texture2D>("Tiles/Tiles");
            _backgroundImage = Content.Load<Texture2D>("Background/Level1/pre");
            blocks = BlockFactory.CreateBlocks(gameBoard, blocks, tileSet);

            foreach (var pickup in pickUps)
            {
                pickup.LoadContent(Content);
            }
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
