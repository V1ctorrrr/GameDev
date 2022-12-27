using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace GameDevelopment.Environment.BuildingBlocks
{
    internal static class BlockFactory
    {
        public static void CreateBlocks(int[,] gameBoard, List<Block> blocks, List<Texture2D> tileSet)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    if (gameBoard[x, y] == 1)
                    {
                        blocks.Add(new Block(tileSet[0], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(15, 11, 50, 50), Color.White));
                    }
                    else if (gameBoard[x, y] == 2)
                    {
                        blocks.Add(new Block(tileSet[0], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(22, 126, 38, 32), Color.White));
                    } else if (gameBoard[x,y] == 3)
                    {
                        blocks.Add(new Block(tileSet[0], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(22, 120, 30, 22), Color.White));
                    } else if (gameBoard[x,y]== 4)
                    {
                        blocks.Add(new Block(tileSet[0], new Rectangle(y * 80, x * 80, 80, 20), new Rectangle(21, 129, 42, 29), Color.White));
                    } else if(gameBoard[x, y] == 5)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(48, 54, 23, 23), Color.White));
                    } else if(gameBoard[x, y] == 6)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(49, 81, 24, 25), Color.White));
                    } else if(gameBoard[x, y] == 7)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(52, 67, 14, 14), Color.White));
                    } else if (gameBoard[x, y] == 8)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(117, 20, 22, 29), Color.White));
                    } else if(gameBoard[x, y] == 9)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(194, 54, 22, 23), Color.White));
                    } else if (gameBoard[x, y] == 10)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(194, 81, 24, 25), Color.White));
                    } else if (gameBoard[x, y] == 11)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(38, 54, 20, 20), Color.White));
                    } else if (gameBoard[x, y] == 12)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(0, 0, 18, 18), Color.White));
                    } else if (gameBoard[x, y] == 13)
                    {
                        blocks.Add(new Block(tileSet[1], new Rectangle(y * 80, x * 80, 80, 80), new Rectangle(0, 18, 18, 18), Color.White));
                    }
                }
            }
        }
    }
}
