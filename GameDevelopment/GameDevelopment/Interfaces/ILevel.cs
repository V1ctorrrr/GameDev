using GameDevelopment.Entity.Character;
using GameDevelopment.Environment.BuildingBlocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Interfaces
{
    internal interface ILevel
    {
        public List<Block> blocks { get; set; }
        int[,] gameBoard { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
        void LoadContent(ContentManager Content);
        void AddHitboxes(GraphicsDevice GraphicsDevice);
    }
}
