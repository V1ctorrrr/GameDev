using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Entity
{
    internal class Block : Interfaces.IGameComponent
    {
        private Texture2D texture;
        public Rectangle Rectangle { get; set; }
        private Color Color { get; set; }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color);
        }


        public Block(Texture2D texture, Rectangle rectangle, Color color)
        {
            this.texture = texture;
            Rectangle = rectangle;
            Color = color;
        }
    }
}
