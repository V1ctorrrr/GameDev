using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Interfaces
{
    internal interface IPickUp
    {
        public Vector2 Position { get; set; }
        public Rectangle hitBox { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spritebatch);
        void LoadContent(ContentManager Content);
    }
}
