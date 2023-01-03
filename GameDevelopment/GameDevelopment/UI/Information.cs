﻿using GameDevelopment.Environment;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevelopment.UI
{
    public static class Information
    {
        public static float Gravity = 14f;
        public static int screenWidth = 1920;
        public static int screenHeight = 1080;
        public static Random random = new Random();
        public static KnightsJourney KnightsJourney;
        public static int Score = 0;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MainMenu.spriteFont, Score.ToString(), new Vector2(screenWidth - 50, 20), Color.Black);
        }
    }
}