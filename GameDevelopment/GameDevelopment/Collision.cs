using GameDevelopment.Entity;
using Microsoft.Xna.Framework;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment
{
    internal static class Collision
    {
        public static void Collide(Hero hero, Block block)
        {
            if (block == null || hero == null) return;
            if (!hero.Hitboxes[hero.textureCounter].Rectangle.Intersects(block.Rectangle)) return;

            var tempPos = hero.Position;
            if (hero.Hitboxes[hero.textureCounter].Rectangle.Intersects(block.Rectangle))
            {
                if (hero.Hitboxes[hero.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + Information.Gravity)
                {
                    hero.IsOnGround = true;
                    tempPos.Y = block.Rectangle.Top - 159;
                }
                else if (hero.Hitboxes[hero.textureCounter].Rectangle.Right>block.Rectangle.Left && 
                    hero.Hitboxes[hero.textureCounter].Rectangle.Left < block.Rectangle.Left && 
                    !(hero.Hitboxes[hero.textureCounter].Rectangle.Right > block.Rectangle.Left + (block.Rectangle.Width/20)))
                {
                    if (hero.textureCounter != 6 && hero.textureCounter != 7)
                    {
                        if (hero.spriteEffect == SpriteEffects.None)
                        {
                            tempPos.X = block.Rectangle.Left - hero.Hitboxes[hero.textureCounter].Rectangle.Width * 2 - 30;
                        }
                        else if (hero.spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            tempPos.X = block.Rectangle.Left - hero.Hitboxes[hero.textureCounter].Rectangle.Width * 2 - 60;
                        }
                    }
                    hero.IsOnGround = false;
                }
                else if (hero.Hitboxes[hero.textureCounter].Rectangle.Left<block.Rectangle.Right && 
                    hero.Hitboxes[hero.textureCounter].Rectangle.Right> block.Rectangle.Right && 
                    !(hero.Hitboxes[hero.textureCounter].Rectangle.Left < block.Rectangle.Right - (block.Rectangle.Width /20)))
                {
                    if (hero.textureCounter!=6 && hero.textureCounter!= 7)
                    {
                        if (hero.spriteEffect == SpriteEffects.None)
                        {
                            tempPos.X = block.Rectangle.Right - hero.Hitboxes[hero.textureCounter].Rectangle.Width / 2 - 40;
                        }
                        else if (hero.spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            tempPos.X = block.Rectangle.Right - hero.Hitboxes[hero.textureCounter].Rectangle.Width / 2 - 70;
                        }
                    }
                    hero.IsOnGround = false;
                }
                else if (hero.Hitboxes[hero.textureCounter].Rectangle.Top <= block.Rectangle.Bottom)
                {
                    tempPos.Y = block.Rectangle.Bottom - hero.Hitboxes[hero.textureCounter].Rectangle.Height/2-40;
                }

                if (hero.IsJumping&& hero.Hitboxes[hero.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + Information.Gravity)
                {
                    hero.IsOnGround = true;
                    hero.IsJumping= false;
                    hero.jump = 0;
                }
            }
            
            hero.Position = tempPos;
        }

        public static void Collide(Hero hero, Ghoul enemy)
        {
            if (enemy == null || hero == null) return;
            if (!(hero.Hitboxes[hero.textureCounter].Rectangle.Intersects(enemy.Hitboxes[enemy.textureCounter].Rectangle)))
            {
                if (hero.textureCounter == 6 || hero.textureCounter == 7)
                {
                    enemy.Health -= hero.Damage; 
                } else
                {
                    hero.Health -= enemy.Damage;
                }
            }
            if (hero.textureCounter == 6 || hero.textureCounter == 7)
            {
                enemy.Health -= hero.Damage;
            }
        }

        public static void Collide(Ghoul enemy, Block block) 
        {
            if (enemy == null && block == null) return;
            if (!(enemy.Hitboxes[enemy.textureCounter].Rectangle.Intersects(block.Rectangle))) return;

            var tempPos = enemy.Position;
            var tempSpeed = enemy.Speed;

            if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + Information.Gravity)
            {
                enemy.IsOnGround = true;
                tempPos.Y = block.Rectangle.Top - enemy.Hitboxes[enemy.textureCounter].Rectangle.Height - 13;
            }
            else if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Right > block.Rectangle.Left &&
                    enemy.Hitboxes[enemy.textureCounter].Rectangle.Left < block.Rectangle.Left)
            {
                tempPos.X = block.Rectangle.Left - enemy.Hitboxes[enemy.textureCounter].Rectangle.Width - 20;
                tempSpeed *= -1;
            }
            else if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Left < block.Rectangle.Right &&
                    enemy.Hitboxes[enemy.textureCounter].Rectangle.Right > block.Rectangle.Right)
            {
                tempPos.X = block.Rectangle.Right;
                tempSpeed *= -1;
            }

            enemy.Position = tempPos;
            enemy.Speed = tempSpeed;
        }

        public static void Collide(Boar boar, Block block)
        {
            if (boar == null && block == null) return;
            if (!(boar.Hitboxes[boar.textureCounter].Rectangle.Intersects(block.Rectangle))) return;

            var tempPos = boar.Position;
            var tempSpeed = boar.Speed;

            if (boar.Hitboxes[boar.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + Information.Gravity)
            {
                boar.IsOnGround = true;
                tempPos.Y = block.Rectangle.Top - boar.Hitboxes[boar.textureCounter].Rectangle.Height - 13;
            } 
            else if (boar.Hitboxes[boar.textureCounter].Rectangle.Right > block.Rectangle.Left &&
                    boar.Hitboxes[boar.textureCounter].Rectangle.Left < block.Rectangle.Left)
            {
                tempPos.X = block.Rectangle.Left - boar.Hitboxes[boar.textureCounter].Rectangle.Width - 20;
                tempSpeed *= -1;
            }
            else if (boar.Hitboxes[boar.textureCounter].Rectangle.Left < block.Rectangle.Right &&
                    boar.Hitboxes[boar.textureCounter].Rectangle.Right > block.Rectangle.Right)
            {
                tempPos.X = block.Rectangle.Right;
                tempSpeed *= -1;
            }

            boar.Position = tempPos;
            boar.Speed = tempSpeed;
        }

        public static void Collide(Hero hero, Boar boar)
        {
            if (boar == null && hero == null) return;
            if (!(boar.Hitboxes[boar.textureCounter].Rectangle.Intersects(hero.Hitboxes[hero.textureCounter].Rectangle))) return;

            if (hero.Hitboxes[hero.textureCounter].Rectangle.Intersects(boar.Hitboxes[boar.textureCounter].Rectangle))
            {
                if (hero.textureCounter == 6 || hero.textureCounter == 7)
                {
                    boar.Health -= hero.Damage;
                    boar.textureCounter = 2;
                }
            }
        }
    }
}
