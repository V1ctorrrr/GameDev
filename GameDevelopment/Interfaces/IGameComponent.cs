using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Interfaces
{
    internal interface IGameComponent
    {
        public void Draw(SpriteBatch spriteBatch);
    }
}
