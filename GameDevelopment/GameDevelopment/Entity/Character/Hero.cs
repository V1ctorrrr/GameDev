using GameDevelopment.animations;
using GameDevelopment.Environment;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Input;
using GameDevelopment.Interfaces;
using GameDevelopment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDevelopment.Entity.Character
{
    internal class Hero : IGameObject, IMovable
    {
        #region Properties
        //First texture = idle, then run, then jump, then fall,then crouch, then crouch walk, then attack, then crouch attack, then hit
        public List<Texture2D> textures = new List<Texture2D>();
        internal List<Animation> animations = new List<Animation>();
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; }
        public bool IsAttacking { get; set; } = false;
        public bool IsJumping { get; set; } = false;
        public bool IsCrouching { get; set; } = false;
        public bool IsOnGround { get; set; } = false;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public int jump { get; set; } = 0;
        public int maxJump { get; set; } = 35;
        private int jumpHeight;
        public KeyboardReader keyboardReader { get; set; } = new KeyboardReader();
        private MovementManager movementManager = new MovementManager();
        public List<Block> Hitboxes { get; set; } = new List<Block>();
        public List<Rectangle> SwordHitbox { get; set; } = new List<Rectangle>();
        public Vector2 SwordPosition { get; set; }
        public int swordBoxCounter { get; set; } = 0;
        private Texture2D HitboxTexture { get; set; }
        public Vector2 HitboxPosition { get; set; }
        public int Health { get; set; } = 20;
        public int Damage { get; set; } = 5;
        private int attackCount = 0;
        private double time = 1;
        private float timeSinceInvincibility = 3f;
        private float deathCounter = 0;
        private float hitAnimationTime = 0;
        public bool Attacked { get; set; } = false;
        internal bool isTakingDamage = false;
        public int DamageAmount { get; set; } = 0;
        public bool IsAlive { get; set; } = true;
        private HealthBar healthBar;

        #endregion
        public Hero(Vector2 position)
        {
            textureCounter= 0;
            keyboardReader= new KeyboardReader();
            Position = position;
            HitboxPosition = new Vector2(82, 82);
            Speed = new Vector2(5, 0);
            jumpHeight = 10;
            SwordPosition= new Vector2(70,0);
            healthBar = new HealthBar(this);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive) return;
            spriteBatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 2, spriteEffect, 0f);
            healthBar.DrawHearts(spriteBatch);
            //Draw Hitboxes
            /*
            spriteBatch.Draw(HitboxTexture,Position + HitboxPosition,Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(0, 0), 1, spriteEffect, 0f);
            if (textureCounter==6||textureCounter==7)
            {
                spriteBatch.Draw(HitboxTexture, Position + HitboxPosition + SwordPosition, SwordHitbox[swordBoxCounter], Color.Black, 0f, new Vector2(0, 0), 1, spriteEffect, 1f);
            }
            */
        }
        public void Update(GameTime gameTime)
        {
            if (!IsAlive) return;
            Attack(gameTime);
            Jump();
            Move();
            Hit(gameTime);
            Death(gameTime);
            HeroAnimations.ChangeAnimation(this);
            IsOnGround = false;
            Attacked= false;

            Hitboxes[textureCounter].Rectangle = new Rectangle((int)Position.X + (int)HitboxPosition.X, (int)Position.Y + (int)HitboxPosition.Y, Hitboxes[textureCounter].Rectangle.Width, Hitboxes[textureCounter].Rectangle.Height);
            if (textureCounter==6)
                SwordHitbox[swordBoxCounter] = new Rectangle((int)Hitboxes[swordBoxCounter].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[swordBoxCounter].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[swordBoxCounter].Rectangle.Width + 20, Hitboxes[swordBoxCounter].Rectangle.Height + 10);
            else if(textureCounter==7)
                SwordHitbox[swordBoxCounter] = new Rectangle((int)Hitboxes[swordBoxCounter].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[swordBoxCounter].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[swordBoxCounter].Rectangle.Width + 20, Hitboxes[swordBoxCounter].Rectangle.Height - 20);
            
            
            animations[textureCounter].Update(gameTime);
            healthBar.Update(gameTime, this);
        }
        public void Move() 
        {
            movementManager.Move(this);
            if (!IsOnGround && !IsJumping)
                Position += new Vector2(0, Information.Gravity);

            if (Position.Y>Information.screenHeight)
                Health = 0;
        }
        private void Attack(GameTime gameTime)
        {
            IsAttacking = false;
            time += gameTime.ElapsedGameTime.TotalSeconds;
            animations[textureCounter].fps = 20;
            if (!keyboardReader.Attacked) return;
            animations[textureCounter].fps = 10;
            if (attackCount <= 20)
            {
                IsAttacking = true;
                if (!IsCrouching)
                {
                    swordBoxCounter = 0;
                    textureCounter = 6;
                    animations[textureCounter].fps = 10;
                    attackCount++;
                }
                else
                {
                    swordBoxCounter = 1;
                    textureCounter = 7;
                    animations[textureCounter].fps = 10;
                    attackCount++;
                }
            }
            else
            {
                animations[textureCounter].counter = 0;
                animations[textureCounter].fps = 20;
            }

            if (time >= 1)
            {
                time = 0;
                attackCount = 0;
            }
        }
        private void Jump()
        {
            jumpHeight = maxJump - jump - (int)Information.Gravity;
            if (keyboardReader.hasJumped && !IsJumping && IsOnGround)
            {
                textureCounter = 2;
                if (animations[textureCounter].counter < 3)
                {
                    animations[textureCounter].counter = 2;
                }
                IsJumping = true;
                IsOnGround = false;
            }
            if (!IsJumping) return;

            if (jumpHeight < jump - Information.Gravity)
            {
                textureCounter = 3;
            }
            Position -= new Vector2(0, jumpHeight);
            jump++;

            if (jump != maxJump) return;
            IsJumping = false;
            jump = 0;
            maxJump = 35;
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
                textureCounter = 8;
                hitAnimationTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                hitAnimationTime = 0;
                isTakingDamage = false;
            }
        }
        public void Death(GameTime gameTime)
        {
            if (Health > 0) return;

            textureCounter = 8;
            
            deathCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed = new Vector2(0, 0);
            if (!(deathCounter > 1.5)) return;

            IsAlive = false;
            Speed = new Vector2(0, 0);
            Information.KnightsJourney.worldState = WorldState.Death;
            Information.Score= 0;
        }
        public void LoadContent(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("Hero/_Idle"));
            textures.Add(Content.Load<Texture2D>("Hero/_Run"));
            textures.Add(Content.Load<Texture2D>("Hero/_Jump"));
            textures.Add(Content.Load<Texture2D>("Hero/_Fall"));
            textures.Add(Content.Load<Texture2D>("Hero/_CrouchAll"));
            textures.Add(Content.Load<Texture2D>("Hero/_CrouchWalk"));
            textures.Add(Content.Load<Texture2D>("Hero/_Attack"));
            textures.Add(Content.Load<Texture2D>("Hero/_CrouchAttack"));
            textures.Add(Content.Load<Texture2D>("Hero/_Hit"));

            for (int i = 0; i < textures.Count; i++)
                animations.Add(new Animation());

            animations[0].GetFramesFromTextureProperties(this.textures[0].Width, this.textures[0].Height, 10, 1);
            animations[1].GetFramesFromTextureProperties(this.textures[1].Width, this.textures[1].Height, 10, 1);
            animations[2].GetFramesFromTextureProperties(this.textures[2].Width, this.textures[2].Height, 3, 1);
            animations[3].GetFramesFromTextureProperties(this.textures[3].Width, this.textures[3].Height, 3, 1);
            animations[4].GetFramesFromTextureProperties(this.textures[4].Width, this.textures[4].Height, 3, 1);
            animations[5].GetFramesFromTextureProperties(this.textures[5].Width, this.textures[5].Height, 8, 1);
            animations[6].GetFramesFromTextureProperties(this.textures[6].Width, this.textures[6].Height, 4, 1);
            animations[7].GetFramesFromTextureProperties(this.textures[7].Width, this.textures[7].Height, 4, 1);
            animations[8].GetFramesFromTextureProperties(this.textures[8].Width, this.textures[8].Height, 1, 1);

            healthBar.LoadContent(Content);
        }
        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            HitboxTexture = new Texture2D(GraphicsDevice,1,1);
            HitboxTexture.SetData(new[] { Color.White });

            
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[0].Width / 10) - 65, textures[0].Height), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[1].Width / 10) - 65, textures[1].Height), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[2].Width / 3) - 65, textures[2].Height), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[3].Width / 3) - 65, textures[3].Height), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[4].Width / 3) - 62, textures[4].Height - 22), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[5].Width / 8) - 62, textures[5].Height - 22), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[6].Width / 10) + 30, textures[6].Height + 10), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[7].Width / 4) - 40, textures[7].Height - 20), new Rectangle(), Color.Red));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, (textures[8].Width) - 65, textures[8].Height), new Rectangle(), Color.Red));

            SwordHitbox.Add(new Rectangle((int)Hitboxes[6].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[6].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[6].Rectangle.Width, Hitboxes[6].Rectangle.Height));
            SwordHitbox.Add(new Rectangle((int)Hitboxes[7].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[7].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[7].Rectangle.Width, Hitboxes[7].Rectangle.Height));
        }
    }
}
