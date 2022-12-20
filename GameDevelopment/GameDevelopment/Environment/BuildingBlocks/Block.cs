using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Environment.BuildingBlocks
{
    internal class Block : Interfaces.IGameComponent
    {
        private Texture2D texture;
        public Rectangle Rectangle { get; set; }
        public Rectangle TileTexture { get; set; }
        public Color Color { get; set; }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, TileTexture,Color);
        }


        public Block(Texture2D texture, Rectangle rectangle, Rectangle tileTexture,Color color)
        {
            this.texture = texture;
            Rectangle = rectangle;
            this.TileTexture = tileTexture;
            Color = color;
        }
    }
}
