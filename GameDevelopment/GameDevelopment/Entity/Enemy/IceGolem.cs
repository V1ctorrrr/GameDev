using GameDevelopment.animations;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
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
    internal class IceGolem : IEnemy
    {
        #region Properties
        public Texture2D texture;
        private Animation animation = new Animation();
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        private Texture2D HitboxTexture { get; set; }
        public Vector2 HitboxPosition { get; set; }
        public bool IsOnGround { get; set; }
        public int Health { get; set; } = 35;
        public int Damage { get; set; } = 5;
        public bool IsAlive { get; set; } = true;
        public List<Block> Hitboxes { get; set; } = new List<Block>();
        public int DamageAmount { get; set; } = 5;
        public bool Attacked { get; set; }
        public bool IsAttacking { get; set; } = false;
        public bool Attacking { get; set; }
        public char AttackDirection { get; set; }
        private bool isTakingDamage = false;
        private float timeSinceInvincibility = 3f;
        private float deathCounter = 0;
        private float hitAnimationTime = 0;
        private double time = 1;
        private double counter = 0;
        public List<Rectangle> SwordHitbox { get; set; } = new List<Rectangle>();
        public Vector2 SwordPosition { get; set; }
        private HealthBar healthBar;
        #endregion 
        public IceGolem(Vector2 Position)
        {
            this.Position = Position;
            Speed = new Vector2(3f, 3f);
            HitboxPosition = new Vector2(5, 15);
            animation.counter = 0;
            healthBar = new HealthBar(this);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 3, spriteEffect, 0f);
            healthBar.DrawBossHealthBar(spritebatch);
            //Hitbox
            //spritebatch.Draw(HitboxTexture, Position + HitboxPosition, Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(), 1, SpriteEffects.None, 0f);
            //spritebatch.Draw(HitboxTexture, Position + HitboxPosition + SwordPosition, SwordHitbox[0], Color.Green, 0f, new Vector2(), 1, SpriteEffects.None, 0f);
        }
        public void Update(GameTime gameTime)
        {
            if (!IsAlive) return;
            Attack(gameTime);
            Move(gameTime);
            Hit(gameTime);
            Death(gameTime);
            IsOnGround = false;

            Hitboxes[textureCounter].Rectangle = new Rectangle((int)Position.X + (int)HitboxPosition.X, (int)Position.Y + (int)HitboxPosition.Y, Hitboxes[textureCounter].Rectangle.Width, Hitboxes[textureCounter].Rectangle.Height);
            SwordHitbox[0] = new Rectangle(Hitboxes[textureCounter].Rectangle.X + (int)SwordPosition.X, Hitboxes[textureCounter].Rectangle.Y + (int)SwordPosition.Y, 200, Hitboxes[0].Rectangle.Height-180);
            animation.Update(gameTime);
            healthBar.Update(gameTime,this);
            animation.fps = 15;
        }

        public void Move(GameTime gameTime)
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
                    if (animation.counter >= 16 && animation.counter <= 25)
                    {
                        Speed = new Vector2(0, 0);
                        animation.counter = 0;
                    }
                    else if (animation.counter <= 5)
                    {
                        if (start == 0)
                            Speed = new Vector2(3, 0);
                        else
                            Speed = new Vector2(-3, 0);

                        animation.counter = 16;
                    }
                }
            }

            if (Speed.X >= 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(Hitboxes[0].Rectangle.Width - 50, 120);
                if (animation.counter >= 25)
                {
                    animation.counter = 16;
                }
            }
            else if (Speed.X <= 0)
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition = new Vector2(-100, 120);
                if (animation.counter >= 25)
                {
                    animation.counter = 16;
                }
            }

            if (Speed.X == 0)
            {
                if (animation.counter >= 5)
                {
                    animation.counter = 0;
                }
            }

            if (!IsOnGround)
                Position += new Vector2(0, Information.Gravity);
            
            if (animation.counter > 5 && animation.counter < 16)
                animation.counter = 0;

            Position += Speed;

            
        }
        public void Hit(GameTime gameTime)
        {
            timeSinceInvincibility += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Attacked && timeSinceInvincibility >= 2)
            {
                timeSinceInvincibility = 0;
                Health -= DamageAmount;
                isTakingDamage = true;
            }

            if (!isTakingDamage) return;

            animation.fps = 10;
            if (animation.counter < 46)
            {
                animation.counter = 47;
            }

            if (hitAnimationTime <= 1)
            {
                if (animation.counter >= 52 && animation.counter < 61)
                {
                    hitAnimationTime = 1;
                    animation.counter = 0;
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
                animation.fps = 15;
            }
        }
        public void Death(GameTime gameTime)
        {
            if (Health > 0) return;

            if (animation.counter < 61)
                animation.counter = 62;

            deathCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed = new Vector2(0, 0);

            if (!(deathCounter > 1)) return;

            animation.counter = 76;
            IsAlive = false;
            Attacking= false;
            Hitboxes[0].Rectangle = new Rectangle(0, 0, 0, 0);
            Speed = new Vector2(0, 0);
        }
        public void Attack(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            animation.fps = 15;

            if (isTakingDamage) return;
            if (!IsAlive) return;

            if (Attacking)
                IsAttacking = true;

            if (!IsAttacking) return;

            if (animation.counter < 31)
                animation.counter = 32;
            else if (animation.counter >= 44 && animation.counter < 52)
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
                animation.counter = 0;
                time = 0;
                IsAttacking = false;
            }
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("IceGolem/frost_guardian_free_192x128_SpriteSheet");
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 16, 5);
            animation.fps = 15;
            animation.counter = 0;

            healthBar.LoadContent(Content);
        }
        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            HitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (texture.Width / 16) * 3 - 340, (texture.Height / 5) * 3 - 100), new Rectangle(), Color.White));
            HitboxPosition = new Vector2(170, 47);
            SwordHitbox.Add(new Rectangle(Hitboxes[textureCounter].Rectangle.X, Hitboxes[textureCounter].Rectangle.Y, 200, Hitboxes[0].Rectangle.Height-120));
            SwordPosition = new Vector2(Hitboxes[0].Rectangle.Width-50, 120);
        }
    }
}
