using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Environment
{
    internal class Level3 : ILevel
    {
        public List<Block> blocks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int[,] gameBoard { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddHitboxes(GraphicsDevice GraphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
