using GameDevelopment.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.UI
{
    internal static class Won
    {
        public static List<Texture2D> backgrounds = new List<Texture2D>();
        private static Button MainMenuButton = new Button(new Rectangle(Information.screenWidth / 2 - 100, Information.screenHeight / 2 - 100, 200, 100), new Rectangle(112, 80, 31, 16), "Main Menu");
        private static Button exitButton = new Button(new Rectangle(Information.screenWidth / 2 - 100, Information.screenHeight / 2 + 50, 200, 100), new Rectangle(112, 80, 31, 16), "Exit");
        private static SpriteFont spriteFont;
        private static Vector2 textPosition;
        private static Vector2 textPosition2;

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var background in backgrounds)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight), Color.White);
            }
            MainMenuButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, "You Won!!", new Vector2(textPosition.X, textPosition.Y), Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, $"You had {Information.Score} points!!", new Vector2(textPosition.X - 100, textPosition.Y + 75), Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
        }

        public static void Update(GameTime gameTime) 
        { 
            MainMenuButton.Update(gameTime);
            exitButton.Update(gameTime);

            if (MainMenuButton.Clicked)
            {
                GameReloader.reloadGame(WorldState.MainMenu, gameTime);
            } else if (exitButton.Clicked)
            {
                Information.KnightsJourney.Exit();
            }
        }

        public static void LoadContent(ContentManager Content)
        {
            MainMenuButton.LoadContent(Content);
            exitButton.LoadContent(Content);

            spriteFont = Content.Load<SpriteFont>("UI/Fonts/Font");
            textPosition = new Vector2(Information.screenWidth / 2 - (spriteFont.MeasureString("Game Over").X / 2) * 3, 50);
            textPosition2 = new Vector2(Information.screenWidth / 2 - (spriteFont.MeasureString($"You had {Information.Score} points!!").X / 2) * 3, 50);

            backgrounds.Add(Content.Load<Texture2D>("UI/Won/1"));
            backgrounds.Add(Content.Load<Texture2D>("UI/Won/2"));
            backgrounds.Add(Content.Load<Texture2D>("UI/Won/3"));
            backgrounds.Add(Content.Load<Texture2D>("UI/Won/4"));
        }
    }
}
