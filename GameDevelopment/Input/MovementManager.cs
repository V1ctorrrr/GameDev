using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevelopment.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Input
{
    internal class MovementManager
    {
        public void Move(IMovable movable)
        {
            var direction = movable.keyboardReader.ReadInput();
            var distance = direction * movable.Speed;
            movable.Position += distance;
        }
    }
}
