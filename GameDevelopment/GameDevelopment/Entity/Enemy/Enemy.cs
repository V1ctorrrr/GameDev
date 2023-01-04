using GameDevelopment.animations;
using GameDevelopment.Environment.BuildingBlocks;
using GameDevelopment.Interfaces;
using GameDevelopment.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Entity.Enemy
{
    internal abstract class Enemy : IEnemy
    {
        #region Properties
        public List<Texture2D> textures { get; set; } = new List<Texture2D>();
        internal List<Animation> animations { get; set; } = new List<Animation>();
        internal int scale = 3;
        public int textureCounter { get; set; } = 0;
        public SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        internal Texture2D HitboxTexture { get; set; }
        public Vector2 HitboxPosition { get; set; }
        public bool IsOnGround { get; set; }
        public int Health { get; set; } = 20;
        public int Damage { get; set; } = 5;
        public bool IsAlive { get; set; } = true;
        public List<Block> Hitboxes { get; set; } = new List<Block>();
        public int DamageAmount { get; set; } = 5;
        public bool Attacked { get; set; }
        public bool IsAttacking { get; set; } = false;
        public bool Attacking { get; set; }
        public char AttackDirection { get; set; }
        internal bool isTakingDamage = false;
        internal float timeSinceInvincibility = 3f;
        internal float deathCounter = 0;
        internal float hitAnimationTime = 0;
        internal double time = 1;
        public List<Rectangle> SwordHitbox { get; set; } = new List<Rectangle>();
        public Vector2 SwordPosition { get; set; }

        internal double counter = 0;
        internal HealthBar healthBar;
        #endregion
        public Enemy(Vector2 position)
        {
            Position = position;
        }

        public abstract void Draw(SpriteBatch spritebatch);
        public abstract void Update(GameTime gameTime);
        public abstract void Move(GameTime gameTime);

        public abstract void Attack(GameTime gameTime);

        public abstract void Death(GameTime gameTime);

        public abstract void Hit(GameTime gameTime);

        public abstract void LoadContent(ContentManager Content);

        public abstract void AddHitboxes(GraphicsDevice GraphicsDevice);
    }
}
