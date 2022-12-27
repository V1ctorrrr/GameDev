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
    internal class Level2 : ILevel
    {
        public List<Block> blocks { get; set; } = new List<Block>();
        private List<Texture2D> tileSet = new List<Texture2D>();
        private Texture2D _backgroundImage;
        public int[,] gameBoard { get; set; } = new int[,] {
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 2, 2, 2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 1, 1, 1, 1 }
        };

        private Hero hero = new Hero(new Vector2(5,200));
        private List<IEnemy> enemies= new List<IEnemy>();
        private List<IPickUp> pickUps= new List<IPickUp>();

        public Level2()
        {
            enemies.Add(new Ghoul(new Vector2(880, 300)));
            enemies.Add(new Skeleton(new Vector2(880, 870)));
            enemies.Add(new Ghoul(new Vector2(0, 900)));
            pickUps.Add(new Flag(new Vector2(1850, 450)));
            pickUps.Add(new JumpPotion(new Vector2(420, 1000)));
            pickUps.Add(new JumpPotion(new Vector2(1700, 1000)));
            pickUps.Add(new Coin(new Vector2(10,900)));
            pickUps.Add(new Coin(new Vector2(60, 900)));
            pickUps.Add(new Coin(new Vector2(60, 950)));
            pickUps.Add(new Coin(new Vector2(10, 950)));
            pickUps.Add(new Coin(new Vector2(1220, 980)));
            pickUps.Add(new Coin(new Vector2(1220, 930)));
            pickUps.Add(new Coin(new Vector2(1170, 980)));
            pickUps.Add(new Coin(new Vector2(1170, 930)));
            pickUps.Add(new Coin(new Vector2(1120, 930)));
            pickUps.Add(new Coin(new Vector2(1120, 980)));
            pickUps.Add(new Meat(new Vector2(880,390)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundImage, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight), Color.White);
            spriteBatch.Draw(tileSet[0], new Rectangle(560, 480, 800, 800), new Rectangle(64,160,32,32),Color.White);
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
            foreach (var pickup in pickUps)
            {
                pickup.Update(gameTime);
                Collision.Collide(hero, pickup);
            }

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
                Collision.Collide(enemy, hero);
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
            tileSet.Add(Content.Load<Texture2D>("Tiles/Tiles"));
            tileSet.Add(Content.Load<Texture2D>("Tiles/Icecave tools and tileset"));
            _backgroundImage = Content.Load<Texture2D>("Background/Level2/BIGPRE_ORIG_SIZE");

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
