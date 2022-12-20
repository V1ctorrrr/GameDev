using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace GameDevelopment.UI
{
    internal class Button
    {
        //inspiratie: https://www.youtube.com/watch?v=lcrgj26G5Hg
        #region Properties
        private Texture2D tile;
        private MouseState state = new MouseState();
        public bool Clicked = false;
        private bool IsHovering = false;

        private Rectangle buttonSize;
        private Rectangle tilePosition;
        private Color color = Color.White;

        private SpriteFont spriteFont;
        private string text;
        private Vector2 textPosition;
        #endregion
        public Button(Rectangle buttonSize, Rectangle tilePosition, string text)
        {
            this.buttonSize = buttonSize;
            this.tilePosition = tilePosition;
            this.text= text;
        }

        public void Update(GameTime gameTime)
        {
            state = Mouse.GetState();
            
            if (state.X<buttonSize.Right && state.X>buttonSize.Left&&
                state.Y<buttonSize.Bottom && state.Y>buttonSize.Top) 
            {
                IsHovering= true;
                color = Color.Gray;
            }
            else
            {
                IsHovering= false;
                color= Color.White;
            }

            if (IsHovering&& state.LeftButton==ButtonState.Pressed)
                Clicked = true;
            else
                Clicked= false;

            textPosition = new Vector2(buttonSize.X + buttonSize.Width / 2 - spriteFont.MeasureString(text).X / 2, buttonSize.Y + buttonSize.Height / 2 - spriteFont.MeasureString(text).Y / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tile,buttonSize,tilePosition, color);
            spriteBatch.DrawString(spriteFont,text,new Vector2(textPosition.X,textPosition.Y),Color.White);
        }

        public void LoadContent(ContentManager Content)
        {
            tile = Content.Load<Texture2D>("UI/GUI");
            spriteFont = Content.Load<SpriteFont>("UI/Fonts/Font");
        }
    }
}
