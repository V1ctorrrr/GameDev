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
    internal class JumpPotion : IPickUp
    {
        public Vector2 Position { get; set; }
        public Rectangle hitBox { get; set; }
        private Texture2D texture;
        private Animation animation = new Animation();
        public bool IsPicked = false;

        public JumpPotion(Vector2 position)
        {
            Position = position;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (IsPicked) return;
            spritebatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("UI/PickUps/poção azul");
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 3, 3);
            hitBox = new Rectangle((int)Position.X, (int)Position.Y, texture.Width / 3, texture.Height / 3);
        }

        public void Update(GameTime gameTime)
        {
            if (animation.counter == 6)
            {
                animation.counter = 0;
            }
            animation.Update(gameTime);
        }
    }
}
