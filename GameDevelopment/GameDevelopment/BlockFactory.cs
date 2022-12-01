using GameDevelopment.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment
{
    internal class BlockFactory
    {
        public static Block CreateBLock(string type,int x, int y, GraphicsDevice graphics,Texture2D texture,Color color)
        {
            Block newBlock = null;
            type.ToUpper();
            if (type == "NORMAL")
            {
                newBlock = new Block(texture,new Rectangle(x,y,80,80),color);
            }
            return newBlock;
        }
    }
}
