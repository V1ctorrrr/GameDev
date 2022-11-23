using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.Input
{
    internal class KeyboardReader : IInputReader
    {
        public string Direction { get; private set; } = "none";
        public bool Pressed { get; private set; } = false;
        public bool Attacked { get; private set; } = false;
        public bool Jumped { get; private set; } = false;
        public bool Crouch { get; private set; } = false;
        public Vector2 ReadInput()
        {
            Jumped = false;
            Attacked = false;
            Pressed = false;
            Crouch = false;
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;
            if (!state.IsKeyDown(Keys.LeftControl))
            {
                //Walk left
                if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
                {
                    direction.X -= 1;
                    Direction = "left";
                    Pressed = true;
                }
                //Walk right
                if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
                {
                    direction.X += 1;
                    Direction = "right";
                    Pressed = true;
                }


                //Jump
                if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q)) && !(state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D)))
                {
                    Jumped = true;
                    Pressed = true;
                    direction.Y -= 5;
                    Direction = "none";
                }
                //Jump left
                if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up)&&(state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q)) )
                {
                    Jumped = true;
                    Pressed = true;
                    direction.Y -= 5;
                    Direction = "left";
                }
                //Jump right
                if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up)&& (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D)))
                {
                    Jumped = true;
                    Pressed = true;
                    direction.Y -= 5;
                    Direction = "right";
                }


                //Crouch
                if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S) && !(state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q)) && !(state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))) 
                {
                    Crouch = true;
                    Pressed = true;
                    Direction = "none";
                }
                //Crouch walk left
                if ((state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S)) && (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))) 
                {
                    Direction = "left";
                    Pressed = true;
                    Crouch = true;
                }
                //Crouch walk right
                if ((state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S)) && (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D)))
                {
                    Direction = "right";
                    Pressed = true;
                    Crouch = true;
                }


                if (!Pressed)
                {
                    Direction = "none";
                    Pressed = false;
                }
            }
            if (state.IsKeyDown(Keys.LeftControl))
            {
                Pressed = true;
                Attacked = true;

            }
            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                Crouch = true;
                Pressed = true;
            }

            if (!Pressed)
            {
                Direction = "none";
                Pressed = false;
            }
            return direction;
        }

    }
}
