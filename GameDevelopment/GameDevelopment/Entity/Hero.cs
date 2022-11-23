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
        public Texture2D texture { get; set; }
        //First texture = idle, then run, then jump, then fall,then crouch, then crouch walk, then attack, then crouch attack
        private List<Texture2D> textures;
        private List<Animation> animations = new List<Animation>();
        private int textureCounter = 0;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public KeyboardReader keyboardReader { get; set; }
        private MovementManager movementManager = new MovementManager();
        public Rectangle Hitbox { get; set; }
        public SpriteEffects spriteEffect { get; set; }
        private bool IsAttacking { get; set; } = false;
        private bool IsJumping { get; set; } = false;
        private bool IsCrouching { get; set; } = false;
        private static Delay delay =new Delay(0);

        public Hero(/*Texture2D runTexture,Texture2D idleTexture,Texture2D attackTexture*/List<Texture2D> textures, KeyboardReader keyboard)
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
            Position = new Vector2(0, 0);
            Speed = new Vector2(1, 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 1, spriteEffect, 0f);
        }


        public void Update(GameTime gameTime)
        {
            Move();
            if (IsAttacking)
            {
                delay.setDelay(2);
            }
            Attack(gameTime);
            animations[textureCounter].Update(gameTime);
        }

        private void Move()
        {
            movementManager.Move(this);
            if (!IsAttacking)
            {
                if (keyboardReader.Direction == "left")
                {
                    textureCounter = 1;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }
                else if (keyboardReader.Direction == "right")
                {
                    textureCounter = 1;
                    spriteEffect = SpriteEffects.None;
                }
                else if (!keyboardReader.Pressed)
                {
                    textureCounter = 0;
                }
                else if (keyboardReader.Jumped)
                {
                    textureCounter = 2;
                    IsJumping = true;
                } 
                else if(keyboardReader.Crouch) 
                {
                    IsCrouching= true;
                    textureCounter = 4;
                    if (animations[textureCounter].counter<3)
                    {
                        animations[textureCounter].counter = 1;
                    }

                }
                else if(!keyboardReader.Crouch)
                {
                    IsCrouching = false;
                }
                else if(!keyboardReader.Jumped)
                {
                    IsJumping= false;
                }
            }
        }

        private void Attack(GameTime gameTime)
        {

            if (keyboardReader.Attacked)
            {
                textureCounter = 6;
                IsAttacking= true;
                if (animations[textureCounter].counter>4 )
                {
                    IsAttacking = false;
                    animations[textureCounter].counter = 0;
                }
                
            } 
            if(keyboardReader.Attacked && IsCrouching)
            {
                textureCounter= 7;
                IsAttacking= true;
                if (animations[textureCounter].counter>4)
                {
                    IsAttacking = false;
                    animations[textureCounter].counter = 0;
                }
            }
            else
            {
                IsAttacking= false;
            }
        }
    }
}
