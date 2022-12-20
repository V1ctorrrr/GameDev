using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace GameDevelopment.Environment.BuildingBlocks
{
    internal static class BlockFactory
    {
        public static List<Block> CreateBlocks(int[,] gameBoard, List<Block> blocks, Texture2D tileSet)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    if (gameBoard[x, y] == 1)
                    {
                        blocks.Add(new Block(tileSet, new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(15, 11, 50, 50), Color.White));
                    }
                    else if (gameBoard[x, y] == 2)
                    {
                        blocks.Add(new Block(tileSet, new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(22, 126, 38, 32), Color.White));
                    } else if (gameBoard[x,y] == 3)
                    {
                        blocks.Add(new Block(tileSet, new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(22, 120, 30, 22), Color.White));
                    } else if (gameBoard[x,y]== 4)
                    {
                        blocks.Add(new Block(tileSet, new Rectangle(y * 80, x * 80, 80, 20), new Rectangle(21, 129, 42, 29), Color.White));
                    }
                }
            }

            return blocks;
        }
    }
}
