using GameDevelopment.Entity.Character;
using GameDevelopment.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.animations
{
    internal static class HeroAnimations
    {
        public static void ChangeAnimation(Hero hero)
        {
            if (!hero.IsAttacking && !hero.isTakingDamage)
            {
                if (!hero.IsJumping)
                {
                    //Right
                    if (hero.keyboardReader.Direction == "left" && !hero.IsCrouching)
                    {
                        hero.textureCounter = 1;
                        hero.spriteEffect = SpriteEffects.FlipHorizontally;
                        hero.HitboxPosition = new Vector2(100, 82);
                    }
                    //Left
                    else if (hero.keyboardReader.Direction == "right" && !hero.IsCrouching)
                    {
                        hero.textureCounter = 1;
                        hero.spriteEffect = SpriteEffects.None;
                        hero.HitboxPosition = new Vector2(82, 82);
                    }
                    //Idle
                    else if (!hero.keyboardReader.Pressed)
                    {
                        hero.textureCounter = 0;
                        if (hero.spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            hero.HitboxPosition = new Vector2(102, 82);
                        }
                        else if (hero.spriteEffect == SpriteEffects.None)
                        {
                            hero.HitboxPosition = new Vector2(82, 82);
                        }
                    }
                    //Crouch
                    else if (hero.keyboardReader.Crouch && hero.keyboardReader.Direction == "none")
                    {
                        hero.IsCrouching = true;
                        hero.textureCounter = 4;
                        if (hero.animations[hero.textureCounter].counter < 3)
                        {
                            hero.animations[hero.textureCounter].counter = 1;
                        }
                        if (hero.spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            hero.HitboxPosition = new Vector2(98, 102);
                        }
                        else if (hero.spriteEffect == SpriteEffects.None)
                        {
                            hero.HitboxPosition = new Vector2(87, 104);
                        }
                    }
                    //Crouch walk left
                    else if (hero.keyboardReader.Crouch && hero.keyboardReader.Direction == "left")
                    {
                        hero.IsCrouching = true;
                        hero.textureCounter = 5;
                        hero.spriteEffect = SpriteEffects.FlipHorizontally;
                        hero.HitboxPosition = new Vector2(98, 104);
                    }
                    //Crouch walk right
                    else if (hero.keyboardReader.Crouch && hero.keyboardReader.Direction == "right" )
                    {
                        hero.IsCrouching = true;
                        hero.textureCounter = 5;
                        hero.spriteEffect = SpriteEffects.None;
                        hero.HitboxPosition = new Vector2(87, 104);
                    }
                    else if (!hero.keyboardReader.Crouch)
                    {
                        hero.IsCrouching = false;
                    }

                    if (hero.IsCrouching)
                    {
                        hero.Speed = new Vector2(2, hero.Speed.Y);
                    }
                    else if (!hero.IsCrouching)
                    {
                        hero.Speed = new Vector2(4, hero.Speed.Y);
                    }
                    else if (!hero.IsOnGround)
                    {
                        hero.textureCounter = 3;
                        hero.HitboxPosition = new Vector2(82, 82);
                    }
                }
            }
            //attack move hitbox left/right with direction
            if (hero.spriteEffect == SpriteEffects.FlipHorizontally && hero.textureCounter == 6)
            {
                hero.HitboxPosition = new Vector2(70, 70);
                hero.SwordPosition = new Vector2(-70, 0);
            }
            else if (hero.spriteEffect == SpriteEffects.None && hero.textureCounter == 6)
            {
                hero.HitboxPosition = new Vector2(90, 70);
                hero.SwordPosition = new Vector2(70, 0);
            }
            else if (hero.spriteEffect == SpriteEffects.FlipHorizontally && hero.textureCounter == 7)
            {
                hero.HitboxPosition = new Vector2(90, 104);
                hero.SwordPosition = new Vector2(-60, 0);
            }
            else if (hero.spriteEffect == SpriteEffects.None && hero.textureCounter == 7)
            {
                hero.HitboxPosition = new Vector2(60, 104);
                hero.SwordPosition = new Vector2(80, 0);
            }

            if (hero.IsOnGround == false && !hero.IsJumping)
            {
                hero.textureCounter = 3;
            }
        }
    }
}
