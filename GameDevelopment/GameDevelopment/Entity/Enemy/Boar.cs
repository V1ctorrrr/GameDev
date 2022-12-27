using GameDevelopment.animations;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameDevelopment.Interfaces;
using GameDevelopment.Environment.BuildingBlocks;
using Microsoft.Xna.Framework.Content;

namespace GameDevelopment.Entity.Enemy
{
    internal class Boar : IEnemy, IGameObject
    {
        #region Properties
        public List<Texture2D> textures = new List<Texture2D>();
        private List<Animation> animations = new List<Animation>();
        private int scale = 3;
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public bool IsOnGround { get; set; } = false;
        private bool isTakingDamage = false;
        public bool Attacked { get; set; } = false;
        private double counter = 0;
        public bool IsAttacking { get; set; } = false;
        public List<Block> Hitboxes { get; set; } = new List<Block>();
        private Texture2D HitboxTexture { get; set; }
        public Vector2 HitboxPosition { get; set; }
        public int Health { get; set; } = 15;
        public int DamageAmount { get; set; } = 0;
        public bool IsAlive { get; set; } = true;
        public int Damage { get; set; } = 5;
        public bool Attacking { get; set; } = true;
        public char AttackDirection { get; set ; }
        public List<Rectangle> SwordHitbox { get; set; } = new List<Rectangle>();
        public Vector2 SwordPosition { get; set; }
        private float timeSinceInvincibility = 3f;
        private float deathCounter = 0;
        private float hitAnimationTime = 0;
        private double time = 1;

        private HealthBar healthBar;
        #endregion
        public Boar(Vector2 position)
        {
            Position = position;
            Speed = new Vector2(3f, 0);
            HitboxPosition = new Vector2(5, 14);
            SwordPosition = new Vector2(120, 0);
            healthBar = new HealthBar(this);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (IsAlive)
            {
                spritebatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
                //Hitboxes
                //spritebatch.Draw(HitboxTexture, Position + HitboxPosition, Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(),1, SpriteEffects.None, 0f);
                //spritebatch.Draw(HitboxTexture, Position + HitboxPosition + SwordPosition, SwordHitbox[0], Color.Black, 0f, new Vector2(0, 0), 1, spriteEffect, 0f);
                healthBar.DrawHealthBar(spritebatch);
            }
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
            SwordHitbox[0] = new Rectangle((int)Hitboxes[2].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[2].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[2].Rectangle.Width -100, Hitboxes[2].Rectangle.Height);
            
            animations[textureCounter].Update(gameTime);
            healthBar.Update(gameTime, this);
        }

        public void Move(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalSeconds;
            int direction = Information.random.Next(0, 2);
            double delayBetweenStops = Information.random.NextDouble();
            int start = Information.random.Next(0, 2);

            if (isTakingDamage) return;


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

            if (Speed.X < 0)
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition = new Vector2(-20, 0);
                textureCounter = 1;
            }
            else if (Speed.X > 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(120, 0);
                textureCounter = 1;
            }

            if (Speed.X == 0)
            {
                textureCounter = 0;
            }

            Position += Speed;

            if (!IsOnGround)
                Position += new Vector2(0, Information.Gravity);
        }

        public void Death(GameTime gameTime)
        {
            if (Health > 0) return;

            textureCounter = 2;

            deathCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed = new Vector2(0, 0);
            if (!(deathCounter > 1.5)) return;

            IsAlive = false;
            Speed = new Vector2(0, 0);
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

            if (hitAnimationTime <= 1)
            {
                textureCounter = 2;
                hitAnimationTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                hitAnimationTime = 0;
                isTakingDamage = false;
            }
        }
        public void Attack(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            if (isTakingDamage) return;
            if (Attacking)
                IsAttacking = true;

            if (!IsAttacking) return;

            textureCounter = 1;
            if (AttackDirection == 'R')
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                SwordPosition = new Vector2(120, 0);
            }
            else if (AttackDirection == 'L')
            {
                spriteEffect = SpriteEffects.None;
                SwordPosition = new Vector2(-20, 0);
            }

            if (time >= 1)
            {
                animations[textureCounter].counter = 0;
                time = 0;
                IsAttacking = false;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("Boar/Idle-Sheet"));
            textures.Add(Content.Load<Texture2D>("Boar/Run-Sheet"));
            textures.Add(Content.Load<Texture2D>("Boar/Hit-Sheet"));

            for (int i = 0; i < textures.Count; i++)
                animations.Add(new Animation());

            animations[0].GetFramesFromTextureProperties(this.textures[0].Width, this.textures[0].Height, 4, 1);
            animations[1].GetFramesFromTextureProperties(this.textures[1].Width, this.textures[1].Height, 6, 1);
            animations[2].GetFramesFromTextureProperties(this.textures[2].Width, this.textures[2].Height, 4, 1);

            healthBar.LoadContent(Content);
        }

        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            HitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(5, 50, (textures[0].Width / 4) + 80, textures[0].Height + 50), new Rectangle(), Color.Blue));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(5, 50, (textures[1].Width / 6) + 80, textures[1].Height + 50), new Rectangle(), Color.Blue));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(5, 50, (textures[2].Width / 4) + 80, textures[2].Height + 50), new Rectangle(), Color.Blue));

            SwordHitbox.Add(new Rectangle((int)Hitboxes[2].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[2].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[2].Rectangle.Width - 100, Hitboxes[2].Rectangle.Height));
        }
    }
}
