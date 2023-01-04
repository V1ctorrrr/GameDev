using GameDevelopment.animations;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
using GameDevelopment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace GameDevelopment.Entity.Enemy
{
    internal class Skeleton: Enemy,IEnemy, IGameObject
    {
        
        public Skeleton(Vector2 position): base(position)
        {
            Position = position;
            Speed = new Vector2(2f, 0);
            HitboxPosition = new Vector2(0, 15);
            
            healthBar = new HealthBar(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
            if (!IsAlive) return;
            healthBar.DrawHealthBar(spriteBatch);
                //Draw Hitboxes
                //spriteBatch.Draw(HitboxTexture, Position + HitboxPosition, Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(), 1, spriteEffect, 0f);
                //spriteBatch.Draw(HitboxTexture, Position + HitboxPosition + SwordPosition, SwordHitbox[swordBoxCounter], Color.Black, 0f, new Vector2(0, 0), 1, spriteEffect, 1f);
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
            SwordHitbox[0] = new Rectangle((int)Hitboxes[0].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[0].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[0].Rectangle.Width+10, Hitboxes[0].Rectangle.Height+5);
            animations[textureCounter].Update(gameTime);
            healthBar.Update(gameTime, this);
        }

        public override void Move(GameTime gameTime)
        {
            if (IsAttacking) return;

            counter += gameTime.ElapsedGameTime.TotalSeconds;
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
                            Speed = new Vector2(2, 0);
                        else
                            Speed = new Vector2(-2, 0);

                    }
                }
            }

            if (isTakingDamage)
            {
                Speed = new Vector2(0, 0);
            }
            
            if (Speed.X < 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                textureCounter = 1;
                HitboxPosition = new Vector2(28, 14);
            }
            else if (Speed.X > 0)
            {
                spriteEffect = SpriteEffects.None;
                textureCounter = 1;
                HitboxPosition = new Vector2(0, 14);
            }
            else if (Speed.X == 0 && spriteEffect==SpriteEffects.None)
            {
                textureCounter = 0;
                HitboxPosition = new Vector2(0, 15);
            } else if(Speed.X == 0 && spriteEffect == SpriteEffects.FlipHorizontally)
            {
                textureCounter = 0;
                HitboxPosition = new Vector2(28, 15);
            }

            Position += Speed;
            if (!IsOnGround)
                Position += new Vector2(0, Information.Gravity);
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

            if (!isTakingDamage) return;

            if (hitAnimationTime <= 1)
            {
                textureCounter = 3;
                animations[textureCounter].fps = 10;
                if (animations[textureCounter].counter>=7)
                    hitAnimationTime = 1;
                else
                    hitAnimationTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                hitAnimationTime = 0;
                animations[textureCounter].counter= 0;
                animations[textureCounter].fps = 20;
                isTakingDamage = false;
            }
        }

        public override void Death(GameTime gameTime)
        {
            if (Health > 0) return;

            textureCounter = 4;
            animations[textureCounter].fps = 10;

            deathCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed = new Vector2(0, 0);
            if (!(deathCounter > 1)) return;

            textureCounter = 4;
            animations[textureCounter].counter = 13;
            IsAlive = false;
            Speed = new Vector2(0, 0);
        }

        public override void Attack(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            if (Attacking)
                IsAttacking = true;

            if (!IsAttacking) return;

            textureCounter = 2;
            if (AttackDirection == 'L')
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                HitboxPosition= new Vector2(65, 24);
                SwordPosition= new Vector2(-53, 0);
            } 
            else if (AttackDirection == 'R')
            {
                spriteEffect = SpriteEffects.None;
                HitboxPosition = new Vector2(10, 24);
                SwordPosition = new Vector2(53, 0);
            }

            if (time>=1)
            {
                animations[textureCounter].counter= 0;
                time = 0;
                IsAttacking= false;
                textureCounter= 0;
                if (spriteEffect==SpriteEffects.FlipHorizontally)
                {
                    HitboxPosition = new Vector2(28,15);
                }
                else
                {
                    HitboxPosition = new Vector2(1, 15);
                }
            }
        }

        public override void LoadContent(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("Skeleton/Skeleton Idle"));
            textures.Add(Content.Load<Texture2D>("Skeleton/Skeleton Walk"));
            textures.Add(Content.Load<Texture2D>("Skeleton/Skeleton Attack"));
            textures.Add(Content.Load<Texture2D>("Skeleton/Skeleton Hit"));
            textures.Add(Content.Load<Texture2D>("Skeleton/Skeleton Dead"));

            for (int i = 0; i < textures.Count; i++)
                animations.Add(new Animation());

            animations[0].GetFramesFromTextureProperties(this.textures[0].Width, this.textures[0].Height, 11, 1);
            animations[1].GetFramesFromTextureProperties(this.textures[1].Width, this.textures[1].Height, 13, 1);
            animations[2].GetFramesFromTextureProperties(this.textures[2].Width, this.textures[2].Height, 18, 1);
            animations[3].GetFramesFromTextureProperties(this.textures[3].Width, this.textures[3].Height, 8, 1);
            animations[4].GetFramesFromTextureProperties(this.textures[4].Width, this.textures[4].Height, 15, 1);
            healthBar.LoadContent(Content);
        }

        public override void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            HitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, textures[0].Width / 11 + 20, textures[0].Height + 50), new Rectangle(), Color.DarkCyan));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, textures[1].Width / 13 + 20, textures[1].Height + 50), new Rectangle(), Color.DarkCyan));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, textures[2].Width / 18 + 10, textures[2].Height + 50), new Rectangle(), Color.DarkCyan));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, textures[3].Width / 11 + 20, textures[3].Height + 50), new Rectangle(), Color.DarkCyan));
            Hitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(1, 1, textures[4].Width / 11 + 20, textures[4].Height + 50), new Rectangle(), Color.DarkCyan));

            SwordHitbox.Add(new Rectangle((int)Hitboxes[2].Rectangle.X + (int)SwordPosition.X, (int)Hitboxes[2].Rectangle.Y + (int)SwordPosition.Y, Hitboxes[2].Rectangle.Width - 50, Hitboxes[2].Rectangle.Height - 15));
            SwordPosition = new Vector2(53, 0);
        }
    }
}
