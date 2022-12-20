using Microsoft.Xna.Framework.Graphics;
using GameDevelopment.Entity.Enemy;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Entity.Hero;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;

namespace GameDevelopment
{
    internal static class Collision
    {
        public static void Collide(Hero hero, Block block)
        {
            if (block == null || hero == null) return;

            var tempPos = hero.Position;

            if (hero.Position.X<0-hero.HitboxPosition.X)
                tempPos.X = 0-hero.HitboxPosition.X;

            if (!hero.Hitboxes[hero.textureCounter].Rectangle.Intersects(block.Rectangle)) return;
            
            if (hero.Hitboxes[hero.textureCounter].Rectangle.Intersects(block.Rectangle))
            {
                if (hero.Hitboxes[hero.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + Information.Gravity)
                {
                    hero.IsOnGround = true;
                    tempPos.Y = block.Rectangle.Top - 159;
                    hero.IsJumping = false;
                    hero.jump = 0;
                }
                else if (hero.Hitboxes[hero.textureCounter].Rectangle.Right > block.Rectangle.Left &&
                    hero.Hitboxes[hero.textureCounter].Rectangle.Left < block.Rectangle.Left &&
                    !(hero.Hitboxes[hero.textureCounter].Rectangle.Right > block.Rectangle.Left + (block.Rectangle.Width / 20)))
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
                else if (hero.Hitboxes[hero.textureCounter].Rectangle.Left < block.Rectangle.Right &&
                    hero.Hitboxes[hero.textureCounter].Rectangle.Right > block.Rectangle.Right &&
                    !(hero.Hitboxes[hero.textureCounter].Rectangle.Left < block.Rectangle.Right - (block.Rectangle.Width / 20)))
                {
                    if (hero.textureCounter != 6 && hero.textureCounter != 7)
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
                    tempPos.Y = block.Rectangle.Bottom - hero.Hitboxes[hero.textureCounter].Rectangle.Height / 2 - 40;
                    hero.IsOnGround = false;
                    hero.IsJumping = false;
                    hero.jump = 0;
                }

                if (hero.IsJumping && hero.Hitboxes[hero.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + Information.Gravity)
                {
                    hero.IsOnGround = true;
                    hero.IsJumping= false;
                    hero.jump = 0;
                }
            }
            
            hero.Position = tempPos;
        }

        public static void Collide(IEnemy enemy, Block block)
        {
            if (enemy == null && block == null) return;

            var tempPos = enemy.Position;
            var tempSpeed = enemy.Speed;
            if (enemy.Position.X<0-enemy.HitboxPosition.X)
            {
                tempSpeed *= -1;
            }
            if (!(enemy.Hitboxes[enemy.textureCounter].Rectangle.Intersects(block.Rectangle))) return;

            

            if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Bottom <= block.Rectangle.Top + Information.Gravity)
            {
                enemy.IsOnGround = true;
                tempPos.Y = block.Rectangle.Top - enemy.Hitboxes[enemy.textureCounter].Rectangle.Height - enemy.HitboxPosition.Y;
                
            }
            else if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Right > block.Rectangle.Left &&
                    enemy.Hitboxes[enemy.textureCounter].Rectangle.Left < block.Rectangle.Left)
            {
                if (enemy is Skeleton)
                    tempPos.X = block.Rectangle.Left - enemy.Hitboxes[enemy.textureCounter].Rectangle.Width - 26;
                else
                    tempPos.X = block.Rectangle.Left - enemy.Hitboxes[enemy.textureCounter].Rectangle.Width - enemy.HitboxPosition.X;
                tempSpeed *= -1;
            }
            else if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Left < block.Rectangle.Right &&
                    enemy.Hitboxes[enemy.textureCounter].Rectangle.Right > block.Rectangle.Right)
            {
                if (enemy is Skeleton)
                    tempPos.X = block.Rectangle.Right;
                else
                    tempPos.X = block.Rectangle.Right - enemy.HitboxPosition.X;
                tempSpeed *= -1;
            }

            enemy.Position = tempPos;
            enemy.Speed = tempSpeed;
        }

        public static void Collide(IEnemy enemy, Hero hero)
        {
            if (enemy == null && hero == null) return;

            if (hero.SwordHitbox[hero.swordBoxCounter].Intersects(enemy.Hitboxes[enemy.textureCounter].Rectangle))
            {
                if (hero.IsAttacking)
                {
                    enemy.Attacked = true;
                    enemy.DamageAmount = hero.Damage;
                    enemy.Attacking = false;
                }
                else
                {
                    enemy.Attacked = false;
                }
            }
            
            if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Top < hero.Hitboxes[hero.textureCounter].Rectangle.Bottom && 
                enemy.Hitboxes[enemy.textureCounter].Rectangle.Bottom > hero.Hitboxes[hero.textureCounter].Rectangle.Top)
            {
                if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Left < hero.Hitboxes[hero.textureCounter].Rectangle.Right + 10 &&
                enemy.Hitboxes[enemy.textureCounter].Rectangle.Left > hero.Hitboxes[hero.textureCounter].Rectangle.Left)
                {
                    enemy.Attacking = true;
                    enemy.AttackDirection = 'L';
                }
                else if (enemy.Hitboxes[enemy.textureCounter].Rectangle.Right > hero.Hitboxes[hero.textureCounter].Rectangle.Left - 30 &&
                    enemy.Hitboxes[enemy.textureCounter].Rectangle.Right < hero.Hitboxes[hero.textureCounter].Rectangle.Right)
                {
                    enemy.Attacking = true;
                    enemy.AttackDirection = 'R';
                }
                else
                {
                    enemy.Attacking = false;
                }

                if (enemy.SwordHitbox[0].Intersects(hero.Hitboxes[hero.textureCounter].Rectangle) && enemy.IsAttacking)
                {
                    hero.DamageAmount = enemy.Damage;
                    hero.Attacked = true;
                }
                else
                {
                    hero.Attacked = false;
                }
            }
            else
            {
                enemy.Attacking = false;
            }
        }
    }
}
