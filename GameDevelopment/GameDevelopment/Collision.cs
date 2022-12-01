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
            var tempSpeed = hero.Speed;
            if (hero.Hitboxes[hero.textureCounter].Rectangle.Intersects(block.Rectangle))
            {
                if (hero.Hitboxes[hero.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + hero.Gravity)
                {
                    hero.IsOnGround = true;
                    tempPos.Y = block.Rectangle.Top - 159;
                }
                else if (hero.Hitboxes[hero.textureCounter].Rectangle.Right>block.Rectangle.Left && 
                    hero.Hitboxes[hero.textureCounter].Rectangle.Left < block.Rectangle.Left && 
                    !(hero.Hitboxes[hero.textureCounter].Rectangle.Right > block.Rectangle.Left + (block.Rectangle.Width/10)))
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
                    !(hero.Hitboxes[hero.textureCounter].Rectangle.Left < block.Rectangle.Right - (block.Rectangle.Width / 10)))
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

                if (hero.IsJumping&& hero.Hitboxes[hero.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + hero.Gravity)
                {
                    hero.IsOnGround = true;
                    hero.IsJumping= false;
                    hero.jump = 0;
                }
            }
            
            hero.Position = tempPos;
            hero.Speed = tempSpeed;
        }

        public static void Collide(Hero hero, Enemy enemy)
        {

        }

        public static void Collide(Enemy enemy, Block block) 
        { 

        }
    }
}
