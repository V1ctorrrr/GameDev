using GameDevelopment.Environment;
using GameDevelopment.Soundtrack;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameDevelopment.UI
{
    internal static class GameReloader
    {
        public static void reloadGame(WorldState stateAfterReload, GameTime gameTime)
        {
            
            Information.KnightsJourney.level1 = new Level1();
            Information.KnightsJourney.level2 = new Level2();
            Information.KnightsJourney.level3 = new Level3();
            Information.KnightsJourney.tutorial = new Tutorial();

            Information.KnightsJourney.level1.LoadContent(Information.KnightsJourney.Content);
            Information.KnightsJourney.level1.AddHitboxes(Information.KnightsJourney.GraphicsDevice);
            Information.KnightsJourney.level1.Update(gameTime);

            Information.KnightsJourney.level2.LoadContent(Information.KnightsJourney.Content);
            Information.KnightsJourney.level2.AddHitboxes(Information.KnightsJourney.GraphicsDevice);
            Information.KnightsJourney.level2.Update(gameTime);

            Information.KnightsJourney.level3.LoadContent(Information.KnightsJourney.Content);
            Information.KnightsJourney.level3.AddHitboxes(Information.KnightsJourney.GraphicsDevice);
            Information.KnightsJourney.level3.Update(gameTime);

            Information.KnightsJourney.tutorial.LoadContent(Information.KnightsJourney.Content);
            Information.KnightsJourney.tutorial.AddHitboxes(Information.KnightsJourney.GraphicsDevice);
            Information.KnightsJourney.tutorial.Update(gameTime);

            Information.KnightsJourney.worldState = stateAfterReload;
        }
    }
}
