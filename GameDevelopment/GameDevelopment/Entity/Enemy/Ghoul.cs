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
    internal class Ghoul : IEnemy, IGameObject
    {
        #region Properties
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
        public int Health { get; set; } = 20;
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
        public List<Rectangle> SwordHitbox { get; set; } = new List<Rectangle>();
        public Vector2 SwordPosition { get; set; }

        private double counter = 0;
        private HealthBar healthBar;
        #endregion
        public Ghoul(Vector2 Position)
        {
            this.Position= Position;
            Speed = new Vector2(2f, 2f);
            HitboxPosition = new Vector2(5, 15);
            healthBar = new HealthBar(this);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (!IsAlive) return;

            spritebatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
            healthBar.DrawHealthBar(spritebatch);
            //Hitboxes
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
            SwordHitbox[textureCounter] = new Rectangle(Hitboxes[textureCounter].Rectangle.X, Hitboxes[textureCounter].Rectangle.Y, 30, (texture.Height / 5 * scale) - 20);
            animation.Update(gameTime);
            healthBar.Update(gameTime, this);
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
                    if (animation.counter >= 8 && animation.counter <= 15)
                    {
                        Speed = new Vector2(0, 0);
                        animation.counter = 0;
                    }
                    else if (animation.counter<=3)
                    {
                        if (start == 0)
                            Speed = new Vector2(2, 0);
                        else
                            Speed = new Vector2(-2, 0);

                        animation.counter = 8;
                    }
                }
            }

            if (Speed.X <= 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(-30,0);
                if (animation.counter>=15)
                {
                    animation.counter = 8;
                }
            }
            else if (Speed.X >= 0)
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition= new Vector2(65,0);
                if (animation.counter >= 15)
                {
                    animation.counter = 8;
                }
            }

            if (Speed.X == 0)
            {
                if (animation.counter>=4)
                {
                    animation.counter = 0;
                }
            }

            if (!IsOnGround)
                Position += new Vector2(0, Information.Gravity);

            Position += Speed;
        }

        public void Attack(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            if (Attacking)
                IsAttacking = true;

            if (!IsAttacking) return;

            if (animation.counter < 15)
                animation.counter = 15;
            else if(animation.counter > 21 && animation.counter<32)
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
                animation.counter = 0;
                time = 0;
                IsAttacking = false;
            }


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

            animation.fps = 5;
            if (animation.counter < 24)
            {
                animation.counter = 24;
            }

            if (hitAnimationTime <= 1)
            {
                if (animation.counter >= 27 && animation.counter <= 31)
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
                animation.counter = 0;
                isTakingDamage = false;
                animation.fps = 15;
            }
        }

        public void Death(GameTime gameTime)
        {
            if (Health > 0) return;

            if (animation.counter<32)
                animation.counter = 32;            

            deathCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed = new Vector2(0, 0);

            if (!(deathCounter > 1)) return;

            IsAlive = false;
            Speed = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Ghoul/Ghoul Sprite Sheet");
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 5);
            animation.fps = 15;
            healthBar.LoadContent(Content);
        }

        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            HitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1,1,(texture.Width/8*scale)-30,(texture.Height/5*scale)-20), new Rectangle(),Color.White));
            HitboxPosition= new Vector2(20,20);
            SwordHitbox.Add(new Rectangle(Hitboxes[textureCounter].Rectangle.X, Hitboxes[textureCounter].Rectangle.Y,30, (texture.Height / 5 * scale) - 20));
            SwordPosition = new Vector2(65,0);
        }
    }
}
