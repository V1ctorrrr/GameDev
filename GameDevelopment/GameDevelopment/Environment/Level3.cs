using GameDevelopment.Entity.Character;
using GameDevelopment.Entity.Enemy;
using GameDevelopment.Entity.PickUps;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Input;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        private List<Texture2D> tileSet = new List<Texture2D>();
        private Texture2D _backgroundImage;
        private bool hasChanged = false;
        public int[,] gameBoard { get; set; }

        private Hero hero = new Hero(new Vector2(5, 200));
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<IPickUp> pickUps = new List<IPickUp>();

        public Level3()
        {
            enemies.Add(new IceGolem(new Vector2(600,600)));
            pickUps.Add(new Flag(new Vector2(1850, 930)));
            pickUps.Add(new JumpPotion(new Vector2(300, 1000)));
            pickUps.Add(new JumpPotion(new Vector2(1400, 1000)));
            pickUps.Add(new Meat(new Vector2(980, 400)));

            gameBoard = new int[,] {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 5, 5, 9, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 12, 6, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11, 5, 5 },
                { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0 },
                { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0 },
                { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0 },
                { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
            };
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

            if (!enemies[0].IsAlive && !hasChanged)
            {
                gameBoard = new int[,] {
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 5, 5, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 12, 6, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11, 5, 5 },
                    { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
                };
                blocks = new List<Block>();

                BlockFactory.CreateBlocks(gameBoard, blocks, tileSet);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            hero.LoadContent(Content);
            tileSet.Add(Content.Load<Texture2D>("Tiles/Tiles"));
            tileSet.Add(Content.Load<Texture2D>("Tiles/Icecave tools and tileset"));
            _backgroundImage = Content.Load<Texture2D>("Background/Level3/BIGPRE_ORIG_SIZE");

            foreach (var enemy in enemies)
                enemy.LoadContent(Content);

            foreach (var pickup in pickUps)
                pickup.LoadContent(Content);

            BlockFactory.CreateBlocks(gameBoard, blocks, tileSet);
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
