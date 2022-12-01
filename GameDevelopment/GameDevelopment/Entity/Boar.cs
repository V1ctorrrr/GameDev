﻿using GameDevelopment.animations;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevelopment.Interfaces;

namespace GameDevelopment.Entity
{
    internal class Boar: IGameObject
    {
        public List<Texture2D> textures;
        private List<Animation> animations = new List<Animation>();
        private int scale = 3;
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public bool IsOnGround { get; set; }
        private double counter = 0;
        public List<Block> Hitboxes { get; set; }
        private Texture2D HitboxTexture { get; set; }
        private Vector2 HitBoxPosition { get; set; }
        private Random random= new Random();
        public int Health { get; set; } = 10;
        public bool IsAlive { get; set; } = true;
        public Boar(List<Texture2D> textures, List<Block> hitboxes, Texture2D hitboxTexture)
        {
            this.textures = textures;
            this.Hitboxes = hitboxes;
            this.HitboxTexture = hitboxTexture;

            for (int i = 0; i < textures.Count; i++)
                animations.Add(new Animation());

            animations[0].GetFramesFromTextureProperties(this.textures[0].Width, this.textures[0].Height, 4, 1);
            animations[1].GetFramesFromTextureProperties(this.textures[1].Width, this.textures[1].Height, 6, 1);
            animations[2].GetFramesFromTextureProperties(this.textures[2].Width, this.textures[2].Height, 4, 1);

            Position= new Vector2(560f, 560f - 20);
            Speed= new Vector2(3f, 0);
            HitBoxPosition = new Vector2(5,15);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (!IsAlive) return;
            spritebatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
            spritebatch.Draw(HitboxTexture, Position + HitBoxPosition, Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(),1, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            if (!IsAlive) return;
                Move(gameTime);
                IsOnGround = false;
                Dying();
                animations[textureCounter].Update(gameTime);
                Hitboxes[textureCounter].Rectangle = new Rectangle(((int)Position.X + (int)HitBoxPosition.X), ((int)Position.Y + (int)HitBoxPosition.Y), Hitboxes[textureCounter].Rectangle.Width, Hitboxes[textureCounter].Rectangle.Height);
        }

        public void Move(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalSeconds;
            int direction = random.Next(0, 2);
            double delayBetweenStops = random.NextDouble();
            int start = random.Next(0, 2);

            if (counter >= (1d / delayBetweenStops))
            {
                counter = 0;
                if (IsAlive)
                {
                    if (textureCounter == 1)
                    {
                        textureCounter = 0;
                        Speed = new Vector2(0, 0);
                    }
                    else if (textureCounter == 0)
                    {
                        textureCounter = 1;
                        if (start == 0)
                            Speed = new Vector2(3, 0);
                        else 
                            Speed = new Vector2(-3, 0);
                        
                    }
                }
            }

            if (textureCounter==0)
            {
                if (direction==0)
                {
                    spriteEffect = SpriteEffects.None;
                } else
                {
                    spriteEffect= SpriteEffects.FlipHorizontally;
                }
            }
            Position += Speed;

            if (!IsOnGround)
            {
                Position += new Vector2(0, Information.Gravity);
            }

            if (Speed.X <= 0)
                spriteEffect = SpriteEffects.None;
            else
                spriteEffect = SpriteEffects.FlipHorizontally;

            if (Speed.X==0)
            {
                textureCounter = 0;
            }
            else
            {
                textureCounter = 1;
            }
        }

        public void Dying()
        {
            if (Health<=0)
            {
                if (textureCounter==2 && animations[textureCounter].counter>4)
                {
                    IsAlive = false;
                    Position = new Vector2(-200, -200);
                }
                textureCounter = 2;
            }
        }
    }
}