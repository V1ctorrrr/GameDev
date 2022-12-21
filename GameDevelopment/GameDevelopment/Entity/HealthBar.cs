﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevelopment.Entity;
using GameDevelopment.Entity.Character;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Entity
{
    internal class HealthBar
    {
        private Rectangle healthBar;
        private List<Texture2D> heartTextures = new List<Texture2D>();
        private int amount;
        private Vector2 enemyPos;
        private int max;
        public HealthBar(Hero hero) 
        {
            amount = hero.Health / 5;
        }

        public HealthBar(IEnemy enemy) 
        { 
            amount = enemy.Health / 5;
            max = enemy.Health / 5;
            healthBar = new Rectangle((int)enemy.Position.X,(int)enemy.Position.Y, amount, 14);
            enemyPos = enemy.Position;
        }

        public void DrawHearts(SpriteBatch spriteBatch)
        {
            for (int i = 1; i <= amount; i++)
            {
                spriteBatch.Draw(heartTextures[0], new Vector2(i * 20 * 4, 20), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                spriteBatch.Draw(heartTextures[1], new Vector2(i * 20 * 4, 20), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                spriteBatch.Draw(heartTextures[2], new Vector2(i * 20 * 4, 20), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
            }
        }

        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heartTextures[4], enemyPos, null, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
            spriteBatch.Draw(heartTextures[3],healthBar,null,Color.White);
        }

        public void Update(GameTime gameTime, Hero hero) 
        {
            amount = hero.Health / 5;
        }

        public void Update(GameTime gameTime, IEnemy enemy)
        {
            amount = enemy.Health / 5;
            enemyPos.X = enemy.Position.X;

            var temp = (heartTextures[3].Width * 2) / max;

            if (amount != 0)
                healthBar = new Rectangle((int)enemy.Position.X + 28, (int)enemyPos.Y + 4, (heartTextures[3].Width * 2) - temp * (max - amount), 28);
            else
                healthBar = new Rectangle((int)enemy.Position.X + 28, (int)enemyPos.Y + 4, 0, 28);

        }

        public void LoadContent(ContentManager Content)
        {
            heartTextures.Add(Content.Load<Texture2D>("UI/HealthBar/background"));
            heartTextures.Add(Content.Load<Texture2D>("UI/HealthBar/border"));
            heartTextures.Add(Content.Load<Texture2D>("UI/HealthBar/heart"));
            heartTextures.Add(Content.Load<Texture2D>("UI/HealthBar/health_bar"));
            heartTextures.Add(Content.Load<Texture2D>("UI/HealthBar/health_bar_decoration"));
        }
    }
}