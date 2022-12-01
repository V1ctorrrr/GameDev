using GameDevelopment.animations;
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
    internal class Ghoul : IGameObject
    {
        public List<Texture2D> textures;
        private List<Animation> animations = new List<Animation>();
        private int scale = 3;
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public List<Block> Hitboxes { get; set; }
        private Texture2D HitboxTexture { get; set; }
        private Vector2 HitBoxPosition { get; set; }
        public bool IsOnGround { get; set; }
        public int Health { get; set; } = 20;
        public int Damage { get; set; } = 5;
        public bool IsAlive { get; set; } = true;
        public Ghoul(List<Texture2D> textures, List<Block> hitboxes, Texture2D hitboxTexture)
        {
            this.textures = textures;
            this.Hitboxes = hitboxes;
            this.HitboxTexture = hitboxTexture;

            for (int i = 0; i < textures.Count; i++)
                animations.Add(new Animation());

            

            Position = new Vector2(50f, 50f);
            Speed = new Vector2(2f, 2f);
            HitBoxPosition = new Vector2(5, 15);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(textures[textureCounter], Position, animations[textureCounter].CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffect, 0f);
            spritebatch.Draw(HitboxTexture, Position + HitBoxPosition, Hitboxes[textureCounter].Rectangle, Hitboxes[textureCounter].Color, 0f, new Vector2(), 1, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            animations[textureCounter].Update(gameTime);
            Hitboxes[textureCounter].Rectangle = new Rectangle(((int)Position.X + (int)HitBoxPosition.X), ((int)Position.Y + (int)HitBoxPosition.Y), Hitboxes[textureCounter].Rectangle.Width, Hitboxes[textureCounter].Rectangle.Height);
            //Move();
        }

        public void Move()
        {
            Position += Speed;
        }
    }
}
