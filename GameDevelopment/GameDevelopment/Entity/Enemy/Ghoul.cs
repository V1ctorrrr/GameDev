using GameDevelopment.animations;
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

namespace GameDevelopment.Entity.Enemy
{
    internal class Ghoul : IEnemy
    {
        public Texture2D texture;
        private Animation animation = new Animation();
        private int scale = 3;
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        private Texture2D HitboxTexture { get; set; }
        public Vector2 HitboxPosition { get; set; }
        public bool IsOnGround { get; set; }
        public int Health { get; set; } = 5;
        public int Damage { get; set; } = 1;
        public bool IsAlive { get; set; } = true;
        public List<Block> Hitboxes { get; set; }
        public int DamageAmount { get; set; } = 0;
        public bool Attacked { get; set; }
        public bool IsAttacking { get; set; } = false;
        public bool Attacking { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public char AttackDirection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Rectangle> SwordHitbox { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 SwordPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private double counter = 0;
        public Ghoul(Texture2D texture, List<Block> hitboxes, Texture2D hitboxTexture)
        {
            this.texture = texture;
            Hitboxes = hitboxes;
            HitboxTexture = hitboxTexture;      
            
            animation.GetFramesFromTextureProperties(this.texture.Width, this.texture.Height, 8, 5);

            Position = new Vector2(50f, 50f);
            Speed = new Vector2(2f, 2f);
            HitboxPosition = new Vector2(5, 15);

            
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
            //spritebatch.Draw(HitboxTexture, Position + HitboxPosition, Hitbox[textureCounter].Rectangle, Hitbox[textureCounter].Color, 0f, new Vector2(), 1, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            //Hitbox[textureCounter].Rectangle = new Rectangle((int)Position.X + (int)HitboxPosition.X, (int)Position.Y + (int)HitboxPosition.Y, Hitbox[textureCounter].Rectangle.Width, Hitbox[textureCounter].Rectangle.Height);
            //Move(gameTime);
        }

        public void Move(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalSeconds;
            int direction = Information.random.Next(0, 2);
            double delayBetweenStops = Information.random.NextDouble();
            int start = Information.random.Next(0, 2);

            if (counter >= 1d / delayBetweenStops)
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

            Position += Speed;

            if (!IsOnGround)
                Position += new Vector2(0, Information.Gravity);

            if (Speed.X <= 0)
            {
                spriteEffect = SpriteEffects.None;
            }
            else if (Speed.X >= 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            if (Speed.X == 0)
            {
                animation.counter = 0;
            }
        }

        public void Hit(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Death(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            throw new NotImplementedException();
        }
    }
}
