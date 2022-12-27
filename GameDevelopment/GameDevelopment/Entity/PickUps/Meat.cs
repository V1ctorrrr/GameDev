using GameDevelopment.animations;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Entity.PickUps
{
    internal class Meat : IPickUp
    {
        public Vector2 Position { get; set; }
        public Rectangle hitBox { get; set; }
        private Texture2D meatTexture;
        private Animation animation = new Animation();
        internal bool IsPicked = false;
        public Meat(Vector2 Position)
        {
            this.Position = Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsPicked) return;
            spriteBatch.Draw(meatTexture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }
        public void Update(GameTime gameTime)
        {
            if (IsPicked) return;
            animation.Update(gameTime);
        }

        public void LoadContent(ContentManager Content)
        {
            meatTexture = Content.Load<Texture2D>("UI/PickUps/raw meats");
            animation.GetFramesFromTextureProperties(meatTexture.Width/6,meatTexture.Height,1,1);
            hitBox = new Rectangle((int)Position.X,(int)Position.Y,meatTexture.Width/6,meatTexture.Height);
        }
    }
}
