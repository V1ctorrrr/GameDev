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
    internal class MainMenu
    {
        private Button tutorialButton = new Button(new Rectangle((Information.screenWidth / 2) - 100, 400, 175, 75), new Rectangle(112, 80, 31, 16), "Tutorial");
        private Button level1Button = new Button(new Rectangle((Information.screenWidth / 2) - 100, 500, 175, 75), new Rectangle(112, 80, 31, 16),"Level1");
        private Button level2Button = new Button(new Rectangle((Information.screenWidth / 2) - 100, 600, 175, 75), new Rectangle(112, 80, 31, 16), "Level2");
        private Button level3Button = new Button(new Rectangle((Information.screenWidth / 2) - 100, 700, 175, 75), new Rectangle(112, 80, 31, 16), "Level3");
        private Button exitButton = new Button(new Rectangle((Information.screenWidth / 2) - 100, 800, 175, 75), new Rectangle(112, 80, 31, 16), "Exit");

        private SpriteFont spriteFont;
        private string text;
        private Vector2 textPosition;

        private Texture2D backgroundImage;

        public MainMenu() 
        {
            text = "Knights journey";
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, Information.screenWidth, Information.screenHeight), new Rectangle(0, 0, 320, 200), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
            spriteBatch.DrawString(spriteFont, text, new Vector2(textPosition.X, textPosition.Y), Color.White,0,new Vector2(0,0),3,SpriteEffects.None,0);
            tutorialButton.Draw(spriteBatch);
            level1Button.Draw(spriteBatch);
            level2Button.Draw(spriteBatch);
            level3Button.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            tutorialButton.Update(gameTime);
            level1Button.Update(gameTime);
            level2Button.Update(gameTime);
            level3Button.Update(gameTime);
            exitButton.Update(gameTime);


            if (tutorialButton.Clicked)
            {
                Information.KnightsJourney.worldState = WorldState.Tutorial;
            } else if (level1Button.Clicked)
            {
                Information.KnightsJourney.worldState = WorldState.Level1;
            } else if (level2Button.Clicked)
            {
                Information.KnightsJourney.worldState = WorldState.Level2;
            } else if (level3Button.Clicked)
            {
                Information.KnightsJourney.worldState = WorldState.Level3;
            } else if (exitButton.Clicked)
            {
                Information.KnightsJourney.Exit();
            }

            textPosition = new Vector2(Information.screenWidth / 2 - (spriteFont.MeasureString(text).X / 2)*3, 50);
        }

        public void LoadContent(ContentManager Content)
        {
            tutorialButton.LoadContent(Content);
            level1Button.LoadContent(Content);
            level2Button.LoadContent(Content);
            level3Button.LoadContent(Content);
            exitButton.LoadContent(Content);

            backgroundImage = Content.Load<Texture2D>("UI/MainMenu/Mountain-Dusk");
            spriteFont = Content.Load<SpriteFont>("UI/Fonts/Font");
        }
    }
}
