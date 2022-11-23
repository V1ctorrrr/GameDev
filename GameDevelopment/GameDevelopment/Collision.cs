using GameDevelopment.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment
{
    internal static class Collision
    {
        public static void CollisionWithObject(Hero hero, Block block, Color color) 
        {
            var heroPos = hero.Position;
            if (hero.Hitbox.Intersects(block.Rectangle))
            {
                hero.Position = heroPos;
                color = Color.Black;
            } 
        }  
    }
}
