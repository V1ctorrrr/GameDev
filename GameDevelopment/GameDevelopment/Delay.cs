using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment
{
    //code i got from https://www.youtube.com/watch?v=kWQdCs0YrTw
    internal class Delay
    {
        private TimeSpan delayRate;
        private TimeSpan previousCallTime;
        public Delay(float rate)
        {
            delayRate = TimeSpan.FromSeconds(rate);
            previousCallTime= TimeSpan.FromSeconds(0f);
        }   

        public void setDelay(float rate)
        {
            delayRate= TimeSpan.FromSeconds(rate);
        }

        public float getDelay()
        {
            return (float)delayRate.TotalSeconds;
        }

        public bool isTimerDone(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousCallTime> delayRate)
            {
                previousCallTime = gameTime.TotalGameTime;
                return true;
            }
            return false;
        }
    }
}
