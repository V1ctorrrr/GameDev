using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Environment.BuildingBlocks
{
    internal class BlockFactory
    {
        public static Block CreateBLock(string type, int x, int y, GraphicsDevice graphics, Texture2D texture, Color color)
        {
            Block newBlock = null;
            type.ToUpper();
            if (type == "NORMAL")
            {
                newBlock = new Block(texture, new Rectangle(x, y, 80, 80), color);
            }
            return newBlock;
        }

        private void CreateBlocks()
        {
            /*
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == 1)
                    {
                        blocks.Add(new Block(blockTexture, new Rectangle(j * 80, i * 80, 80, 80), Color.Brown));
                    }
                }
            }
            */
        }
    }
}
