﻿using GameDevelopment.animations;
using GameDevelopment.Entity;
using GameDevelopment.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace GameDevelopment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Hero
        private Hero hero;
        //First texture = idle, then run, then jump, then fall,then crouch, t crouch walk, then attack, then crouch attack
        private List<Texture2D> _heroTextures = new List<Texture2D>();
        private List<Block> _heroHitboxes= new List<Block>();
        private KeyboardReader keyboard;
        private Block heroBlock;

        //Block
        private Texture2D blockTexture;
        private static Color color;
        private List<Block> blocks= new List<Block>();
        private int[,] gameBoard = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };

        private void CreateBlocks()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i,j] == 1)
                    {
                        blocks.Add(new Block(blockTexture, new Rectangle(j * 80, i * 80, 80, 80),Color.Brown));
                    }
                }
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            _graphics.PreferredBackBufferWidth = 2560;
            _graphics.PreferredBackBufferHeight = 1440;
            _graphics.ApplyChanges();

            
            keyboard = new KeyboardReader();
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(80, 82, (_heroTextures[0].Width / 10) - 65, _heroTextures[0].Height), Color.Red));
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(80, 82, (_heroTextures[1].Width / 10) - 65, _heroTextures[1].Height), Color.Red));
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(82, 82, (_heroTextures[2].Width / 3) - 65, _heroTextures[2].Height), Color.Red));
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(82, 82, (_heroTextures[3].Width / 3) - 65, _heroTextures[3].Height), Color.Red));
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(87, 104, (_heroTextures[4].Width / 3) - 62, _heroTextures[4].Height - 22), Color.Red));
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(87, 104, (_heroTextures[5].Width / 8) - 62, _heroTextures[5].Height - 22), Color.Red));
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(80, 70, (_heroTextures[0].Width / 10) + 34, _heroTextures[0].Height + 10), Color.Red));
            _heroHitboxes.Add(new Block(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(25, 104, (_heroTextures[7].Width / 4) + 30, _heroTextures[7].Height - 20), Color.Red));

            hero = new Hero(_heroTextures, keyboard, _heroHitboxes, blockTexture);
            heroBlock = new Block(blockTexture, new Rectangle((int)hero.Position.X + 41, (int)hero.Position.Y + 41, (_heroTextures[0].Width / 10) - 90, _heroTextures[0].Height - 41), Color.Red);
            color = Color.CornflowerBlue;
            CreateBlocks();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _heroTextures.Add(Content.Load<Texture2D>("_Idle"));
            _heroTextures.Add(Content.Load<Texture2D>("_Run"));
            _heroTextures.Add(Content.Load<Texture2D>("_Jump"));
            _heroTextures.Add(Content.Load<Texture2D>("_Fall"));
            _heroTextures.Add(Content.Load<Texture2D>("_CrouchAll"));
            _heroTextures.Add(Content.Load<Texture2D>("_CrouchWalk"));
            _heroTextures.Add(Content.Load<Texture2D>("_Attack"));
            _heroTextures.Add(Content.Load<Texture2D>("_CrouchAttack"));

            blockTexture = new Texture2D(GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.Update(gameTime);
            heroBlock.Rectangle = new Rectangle((int)hero.Position.X + 45, (int)hero.Position.Y+ 104, (_heroTextures[0].Width / 10) +30, _heroTextures[0].Height -20);
            foreach (var block in blocks)
            {
                Collision.Collide(hero, block);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }
            //heroBlock.Draw(_spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}