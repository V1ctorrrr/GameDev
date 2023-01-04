using GameDevelopment.animations;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
using GameDevelopment.UI;
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
    internal class Ghoul : Enemy, IEnemy, IGameObject
    {
        public Ghoul(Vector2 Position): base(Position)
        {
            Speed = new Vector2(2f, 2f);
            textureCounter= 0;
            HitboxPosition = new Vector2(5, 15);
            healthBar = new HealthBar(this);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(textures[0], Position, animations[0].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
            
            if (!IsAlive) return;
            healthBar.DrawHealthBar(spritebatch);
            //Hitboxes
            //spritebatch.Draw(HitboxTexture, Position + HitboxPosition, Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(), 1, SpriteEffects.None, 0f);
            //spritebatch.Draw(HitboxTexture, Position + HitboxPosition + SwordPosition, SwordHitbox[0], Color.Green, 0f, new Vector2(), 1, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive) return;

            Attack(gameTime);
            Move(gameTime);
            Hit(gameTime);
            Death(gameTime);
            IsOnGround = false;

            Hitboxes[textureCounter].Rectangle = new Rectangle((int)Position.X + (int)HitboxPosition.X, (int)Position.Y + (int)HitboxPosition.Y, Hitboxes[textureCounter].Rectangle.Width, Hitboxes[textureCounter].Rectangle.Height);
            SwordHitbox[textureCounter] = new Rectangle(Hitboxes[textureCounter].Rectangle.X + (int)SwordPosition.X, Hitboxes[textureCounter].Rectangle.Y + (int)SwordPosition.Y, 30, (textures[textureCounter].Height / 5 * scale) - 20);
            animations[textureCounter].Update(gameTime);
            healthBar.Update(gameTime, this);
        }

        public override void Move(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalSeconds;
            double delayBetweenStops = Information.random.NextDouble();
            int start = Information.random.Next(0, 2);

            if (isTakingDamage) return;
            if (IsAttacking) return;

            if (counter >= 1d / delayBetweenStops)
            {
                counter = 0;
                if (IsAlive)
                {
                    if (animations[textureCounter].counter >= 8 && animations[textureCounter].counter <= 15)
                    {
                        Speed = new Vector2(0, 0);
                        animations[textureCounter].counter = 0;
                    }
                    else if (animations[textureCounter].counter<=3)
                    {
                        if (start == 0)
                            Speed = new Vector2(2, 0);
                        else
                            Speed = new Vector2(-2, 0);

                        animations[textureCounter].counter = 8;
                    }
                }
            }

            if (Speed.X <= 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(-30,0);
                if (animations[textureCounter].counter>=15)
                {
                    animations[textureCounter].counter = 8;
                }
            }
            else if (Speed.X >= 0)
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition= new Vector2(65,0);
                if (animations[textureCounter].counter >= 15)
                {
                    animations[textureCounter].counter = 8;
                }
            }

            if (Speed.X == 0)
            {
                if (animations[textureCounter].counter>=4)
                {
                    animations[textureCounter].counter = 0;
                }
            }

            if (animations[textureCounter].counter > 3 && animations[textureCounter].counter < 8)
                animations[textureCounter].counter = 0;

            if (!IsOnGround)
                Position += new Vector2(0, Information.Gravity);

            Position += Speed;
        }

        public override void Attack(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            if (isTakingDamage) return;
            if (Attacking)
                IsAttacking = true;

            if (!IsAttacking) return;

            if (animations[textureCounter].counter < 15)
                animations[textureCounter].counter = 15;
            else if(animations[textureCounter].counter > 21 && animations[textureCounter].counter<32)
                time = 1;

            if (AttackDirection == 'L')
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(-30, 0);
            } 
            else if (AttackDirection == 'R')
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition = new Vector2(65, 0);
            } 

            if (time >= 1)
            {
                animations[textureCounter].counter = 0;
                time = 0;
                IsAttacking = false;
            }
        }

        public override void Hit(GameTime gameTime)
        {
            timeSinceInvincibility += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Attacked && timeSinceInvincibility >= 2)
            {
                timeSinceInvincibility = 0;
                Health -= DamageAmount;
                isTakingDamage = true;
            }

            if (!isTakingDamage) return;

            animations[textureCounter].fps = 5;
            if (animations[textureCounter].counter < 24)
            {
                animations[textureCounter].counter = 24;
            }

            if (hitAnimationTime <= 1)
            {
                if (animations[textureCounter].counter >= 27 && animations[textureCounter].counter <= 31)
                {
                    hitAnimationTime = 1;
                    animations[textureCounter].counter = 0;
                }
                else
                { 
                    hitAnimationTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else
            {
                hitAnimationTime = 0;
                animations[textureCounter].counter = 0;
                isTakingDamage = false;
                animations[textureCounter].fps = 15;
            }
        }

        public override void Death(GameTime gameTime)
        {
            if (Health > 0) return;

            if (animations[textureCounter].counter<32)
                animations[textureCounter].counter = 32;

            deathCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed = new Vector2(0, 0);

            if (!(deathCounter > 1)) return;
            animations[textureCounter].counter = 37;
            IsAlive = false;
            Speed = new Vector2(0, 0);
        }

        public override void LoadContent(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("Ghoul/Ghoul Sprite Sheet"));
            animations.Add(new Animation());
            animations[textureCounter].GetFramesFromTextureProperties(textures[textureCounter].Width, textures[textureCounter].Height, 8, 5);
            animations[textureCounter].fps = 15;
            healthBar.LoadContent(Content);
        }

        public override void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            HitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[textureCounter].Width / 8 * scale) - 30, (textures[textureCounter].Height/5*scale)-20), new Rectangle(),Color.White));
            HitboxPosition= new Vector2(20,20);
            SwordHitbox.Add(new Rectangle(Hitboxes[textureCounter].Rectangle.X, Hitboxes[textureCounter].Rectangle.Y,30, (textures[textureCounter].Height / 5 * scale) - 20));
            SwordPosition = new Vector2(65,0);
        }
    }
}
