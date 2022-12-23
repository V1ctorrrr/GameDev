using GameDevelopment.animations;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Entity.PickUps
{
    internal class Flag : IPickUp
    {
        public Rectangle hitBox { get; set; }
        public Vector2 Position { get; set; }
        private Texture2D texture;
        private Animation animation = new Animation();

        public Flag(Vector2 Position)
        {
            this.Position = Position;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("UI/PickUps/flag animation");
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 5, 1);
            hitBox = new Rectangle((int)Position.X, (int)Position.Y, texture.Width / 5, texture.Height);
            animation.fps = 8;
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }
    }
}
