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
    internal class Coin : IPickUp
    {
        private Texture2D coinTexture;
        public Rectangle hitBox { get; set; }
        public bool IsPicked = false;
        public Vector2 Position { get; set; }
        private Animation animation = new Animation();
        public Coin(Vector2 Position)
        {
            this.Position = Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsPicked) return;
            spriteBatch.Draw(coinTexture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public void LoadContent(ContentManager Content)
        {
            coinTexture = Content.Load<Texture2D>("UI/PickUps/coin2_20x20");
            animation.GetFramesFromTextureProperties(coinTexture.Width, coinTexture.Height, 9, 1);
            hitBox = new Rectangle((int)Position.X, (int)Position.Y, coinTexture.Width / 9, coinTexture.Height);
            animation.fps = 20;
        }
    }
}
