using GameDevelopment.animations;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
using GameDevelopment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Entity.Enemy
{
    internal class IceGolem : Enemy, IEnemy
    {
        public IceGolem(Vector2 Position): base(Position)
        {
            Speed = new Vector2(3f, 3f);
            HitboxPosition = new Vector2(5, 15);
            Health = 35;
            textureCounter= 0;
            
            healthBar = new HealthBar(this);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 3, spriteEffect, 0f);
            healthBar.DrawBossHealthBar(spritebatch);
            //Hitbox
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
            SwordHitbox[0] = new Rectangle(Hitboxes[textureCounter].Rectangle.X + (int)SwordPosition.X, Hitboxes[textureCounter].Rectangle.Y + (int)SwordPosition.Y, 200, Hitboxes[0].Rectangle.Height-180);
            animations[textureCounter].Update(gameTime);
            healthBar.Update(gameTime,this);
            animations[textureCounter].fps = 15;
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
                    if (animations[textureCounter].counter >= 16 && animations[textureCounter].counter <= 25)
                    {
                        Speed = new Vector2(0, 0);
                        animations[textureCounter].counter = 0;
                    }
                    else if (animations[textureCounter].counter <= 5)
                    {
                        if (start == 0)
                            Speed = new Vector2(3, 0);
                        else
                            Speed = new Vector2(-3, 0);

                        animations[textureCounter].counter = 16;
                    }
                }
            }

            if (Speed.X >= 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(Hitboxes[0].Rectangle.Width - 50, 120);
                if (animations[textureCounter].counter >= 25)
                {
                    animations[textureCounter].counter = 16;
                }
            }
            else if (Speed.X <= 0)
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition = new Vector2(-100, 120);
                if (animations[textureCounter].counter >= 25)
                {
                    animations[textureCounter].counter = 16;
                }
            }

            if (Speed.X == 0)
            {
                if (animations[textureCounter].counter >= 5)
                {
                    animations[textureCounter].counter = 0;
                }
            }
            
            if (animations[textureCounter].counter > 5 && animations[textureCounter].counter < 16)
                animations[textureCounter].counter = 0;

            if (!IsOnGround)
                Position += new Vector2(0, Information.Gravity);

            Position += Speed;
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

            animations[textureCounter].fps = 10;
            if (animations[textureCounter].counter < 46)
            {
                animations[textureCounter].counter = 47;
            }

            if (hitAnimationTime <= 1)
            {
                if (animations[textureCounter].counter >= 52 && animations[textureCounter].counter < 61)
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
                isTakingDamage = false;
                animations[textureCounter].fps = 15;
            }
        }
        public override void Death(GameTime gameTime)
        {
            if (Health > 0) return;

            if (animations[textureCounter].counter < 61)
                animations[textureCounter].counter = 62;

            deathCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed = new Vector2(0, 0);

            if (!(deathCounter > 1.2)) return;
            IsAlive = false;
            Speed = new Vector2(0, 0);
            animations[textureCounter].counter = 76;
            IsAlive = false;
            Attacking = false;
            Hitboxes[0].Rectangle = new Rectangle(0, 0, 0, 0);
        }
        public override void Attack(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            animations[textureCounter].fps = 15;

            if (isTakingDamage) return;
            if (!IsAlive) return;

            if (Attacking)
                IsAttacking = true;

            if (!IsAttacking) return;

            if (animations[textureCounter].counter < 31)
                animations[textureCounter].counter = 32;
            else if (animations[textureCounter].counter >= 44 && animations[textureCounter].counter < 52)
                time = 1;

            if (AttackDirection == 'L')
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition = new Vector2(-100, 120);

            }
            else if (AttackDirection == 'R')
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(Hitboxes[0].Rectangle.Width - 50, 120);
            }

            if (time >= 1)
            {
                animations[textureCounter].counter = 0;
                time = 0;
                IsAttacking = false;
            }
        }
        public override void LoadContent(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("IceGolem/frost_guardian_free_192x128_SpriteSheet"));
            animations.Add(new Animation());
            animations[textureCounter].GetFramesFromTextureProperties(textures[textureCounter].Width, textures[textureCounter].Height, 16, 5);
            animations[textureCounter].fps = 15;
            animations[textureCounter].counter = 0;

            healthBar.LoadContent(Content);
        }
        public override void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            HitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[textureCounter].Width / 16) * 3 - 340, (textures[textureCounter].Height / 5) * 3 - 100), new Rectangle(), Color.White));
            HitboxPosition = new Vector2(170, 47);
            SwordHitbox.Add(new Rectangle(Hitboxes[textureCounter].Rectangle.X, Hitboxes[textureCounter].Rectangle.Y, 200, Hitboxes[0].Rectangle.Height-120));
            SwordPosition = new Vector2(Hitboxes[0].Rectangle.Width-50, 120);
        }
    }
}
