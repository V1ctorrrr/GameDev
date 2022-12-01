using GameDevelopment.animations;
using GameDevelopment.Input;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDevelopment.Entity
{
    internal class Hero : IGameObject, IMovable
    {
        #region Properties
        //First texture = idle, then run, then jump, then fall,then crouch, then crouch walk, then attack, then crouch attack
        public List<Texture2D> textures;
        private List<Animation> animations = new List<Animation>();
        private int scale = 2;
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; }
        public bool IsAttacking { get; set; } = false;
        public bool IsJumping { get; set; } = false;
        public bool IsCrouching { get; set; } = false;
        public bool IsOnGround { get; set; } = false;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public int jump = 0;
        private int maxJump = 38;
        private int jumpHeight;
        public float Gravity { get; set; } = 15f;
        public KeyboardReader keyboardReader { get; set; }
        private MovementManager movementManager = new MovementManager();
        public List<Block> Hitboxes { get; set; }
        private Texture2D HitboxTexture{ get; set; }
        private Vector2 HitBoxPosition { get; set; }

        #endregion
        public Hero(List<Texture2D> textures, KeyboardReader keyboard, List<Block> hitboxes, Texture2D hitboxTexture)
        {
            this.textures=textures;
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

            keyboardReader = keyboard;
            Position = new Vector2(0,0);
            HitBoxPosition = (new Vector2(82, 82));
            Speed = new Vector2(2 * scale, 0);
            Hitboxes = hitboxes;
            this.HitboxTexture = hitboxTexture;
            jumpHeight = 10;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
            spriteBatch.Draw(HitboxTexture,Position + HitBoxPosition,Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(0, 0), 1, spriteEffect, 0f);
        }
        public void Update(GameTime gameTime)
        {
            jumpHeight = maxJump - jump - (int)Gravity;
            Jump();
            Attack();
            Move();
            Hitboxes[textureCounter].Rectangle = new Rectangle(((int)Position.X+(int)HitBoxPosition.X), ((int)Position.Y + (int)HitBoxPosition.Y), Hitboxes[textureCounter].Rectangle.Width, Hitboxes[textureCounter].Rectangle.Height);
            animations[textureCounter].Update(gameTime);
        }

        private void Move()
        {
            movementManager.Move(this);
            
            if (!IsAttacking)
            {
                if (!IsJumping)
                {
                    //Right
                    if (keyboardReader.Direction == "left" && !IsCrouching)
                    {
                        textureCounter = 1;
                        spriteEffect = SpriteEffects.FlipHorizontally;
                        HitBoxPosition = (new Vector2(100, 82));
                    }
                    //Left
                    else if (keyboardReader.Direction == "right" && !IsCrouching)
                    {
                        textureCounter = 1;
                        spriteEffect = SpriteEffects.None;
                        HitBoxPosition = new Vector2(82, 82);
                    }
                    //Idle
                    else if (!keyboardReader.Pressed)
                    {
                        textureCounter = 0;
                        if (spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            HitBoxPosition = new Vector2(102, 82);
                        }
                        else if (spriteEffect == SpriteEffects.None)
                        {
                            HitBoxPosition = new Vector2(82, 82);
                        }
                    }
                    //Crouch
                    else if (keyboardReader.Crouch && keyboardReader.Direction == "none")
                    {
                        IsCrouching = true;
                        textureCounter = 4;
                        if (animations[textureCounter].counter < 3)
                        {
                            animations[textureCounter].counter = 1;
                        }
                        if (spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            HitBoxPosition = new Vector2(98, 102);
                        }
                        else if (spriteEffect == SpriteEffects.None)
                        {
                            HitBoxPosition = new Vector2(87, 104);
                        }
                    }
                    //Crouch walk left
                    else if (keyboardReader.Crouch && keyboardReader.Direction == "left")
                    {
                        IsCrouching = true;
                        textureCounter = 5;
                        spriteEffect = SpriteEffects.FlipHorizontally;
                        HitBoxPosition = new Vector2(98 , 104 );
                    }
                    //Crouch walk right
                    else if (keyboardReader.Crouch && keyboardReader.Direction == "right")
                    {
                        IsCrouching = true;
                        textureCounter = 5;
                        spriteEffect = SpriteEffects.None;
                        HitBoxPosition = new Vector2(87 , 104);
                    }
                    else if (!keyboardReader.Crouch)
                    {
                        IsCrouching = false;
                    }
                    if (IsCrouching)
                    {
                        Speed = new Vector2(2, Speed.Y);
                    }
                    else if (!IsCrouching)
                    {
                        Speed = new Vector2(4, Speed.Y);
                    }
                    else if (!IsOnGround)
                    {
                        textureCounter = 3;
                        HitBoxPosition = new Vector2(82, 82);
                    }
                }
            }

            if (spriteEffect==SpriteEffects.FlipHorizontally&&textureCounter==6)
                HitBoxPosition = new Vector2(0, 70);
            else if(spriteEffect==SpriteEffects.None&&textureCounter==6)
                HitBoxPosition = new Vector2(82, 70);
            else if (spriteEffect == SpriteEffects.FlipHorizontally&&textureCounter==7)
                HitBoxPosition = new Vector2(40, 104);
            else if(spriteEffect == SpriteEffects.None&&textureCounter==7)
                HitBoxPosition = new Vector2(48, 104);

            if (!IsOnGround && !IsJumping)
            {
                Position += new Vector2(0, Gravity);
            }

            IsOnGround = false;
        }
        private void Attack()
        {
                if (keyboardReader.Attacked && !IsCrouching)
                {
                    textureCounter = 6;
                    IsAttacking = true;
                    if (animations[textureCounter].counter > 4)
                    {
                        IsAttacking = false;
                        animations[textureCounter].counter = 0;
                    }
                }
                else if (keyboardReader.Attacked && IsCrouching)
                {
                    textureCounter = 7;
                    IsAttacking = true;
                    if (animations[textureCounter].counter > 4)
                    {
                        IsAttacking = false;
                        animations[textureCounter].counter = 0;
                    }
                }
                else
                {
                    IsAttacking = false;
                }
            
        }

        private void Jump()
        {
            if (keyboardReader.hasJumped && !IsJumping && IsOnGround)
            {
                textureCounter = 2;
                if (animations[textureCounter].counter < 3)
                {
                    animations[textureCounter].counter = 2;
                }
                IsJumping = true;
                IsOnGround= false;
            }
            if (!IsJumping) return;

            if (jumpHeight<jump-Gravity)
            {
                textureCounter = 3;
            }
            Position -= new Vector2(0, jumpHeight);
            jump++;

            if (jump != maxJump) return;
            IsJumping= false;
            jump= 0;
        }
    }
}
