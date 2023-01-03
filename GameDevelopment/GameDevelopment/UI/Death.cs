using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevelopment.Environment;

namespace GameDevelopment.UI
{
    internal static class Death
    {
        public static List<Texture2D> backgrounds = new List<Texture2D>();
        private static Button restartButton = new Button(new Rectangle(Information.screenWidth / 2 - 100, Information.screenHeight/2-100, 200, 100), new Rectangle(112, 80, 31, 16),"Try again");
        private static Button exitButton = new Button(new Rectangle(Information.screenWidth / 2 - 100, Information.screenHeight / 2 + 50, 200, 100), new Rectangle(112, 80, 31, 16), "Exit");
        private static SpriteFont spriteFont;
        private static Vector2 textPosition;
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var background in backgrounds)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight),Color.White);
            }
            restartButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, "Game Over", new Vector2(textPosition.X, textPosition.Y), Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
        }

        public static void Update(GameTime gameTime)
        {
            restartButton.Update(gameTime);
            exitButton.Update(gameTime);

            if (restartButton.Clicked)
            {
                GameReloader.reloadGame(WorldState.Level1, gameTime);
            } else if(exitButton.Clicked)
            {
                Information.KnightsJourney.Exit();
            }

            textPosition = new Vector2(Information.screenWidth / 2 - (spriteFont.MeasureString("Game Over").X / 2) * 3, 50);
        }

        public static void LoadContent(ContentManager Content)
        {
            restartButton.LoadContent(Content);
            exitButton.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("UI/Fonts/Font");

            backgrounds.Add(Content.Load<Texture2D>("UI/Death/1"));
            backgrounds.Add(Content.Load<Texture2D>("UI/Death/2"));
            backgrounds.Add(Content.Load<Texture2D>("UI/Death/3"));
            backgrounds.Add(Content.Load<Texture2D>("UI/Death/4"));
        }
    }
}
