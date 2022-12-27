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
    internal interface IEnemy
    {
        public List<Block> Hitboxes { get; set; }
        public int textureCounter { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public bool IsOnGround { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public bool IsAlive { get; set; }
        public int DamageAmount { get; set; }
        public bool Attacked { get; set; }
        public bool IsAttacking { get; set; }
        public bool Attacking { get; set; }
        public char AttackDirection { get; set; } 
        public Vector2 HitboxPosition { get; set; }
        public List<Rectangle> SwordHitbox { get; set; }
        public Vector2 SwordPosition { get; set; }
        public SpriteEffects spriteEffect { get; set; }
        void Draw(SpriteBatch spritebatch);
        void Update(GameTime gameTime);
        void Move(GameTime gameTime);
        void Hit(GameTime gameTime);
        void Death(GameTime gameTime);
        void Attack(GameTime gameTime);
        void LoadContent(ContentManager Content);
        void AddHitboxes(GraphicsDevice GraphicsDevice);
    }
}
