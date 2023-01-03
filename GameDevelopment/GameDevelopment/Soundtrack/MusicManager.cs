using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevelopment.Environment;
using GameDevelopment.UI;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameDevelopment.Soundtrack
{
    internal static class MusicManager
    {
        private static List<Song> soundtracks = new List<Song>();
        private static SoundEffect death;
        private static SoundEffect won;
        private static List<bool> IsPlaying = new List<bool> 
        { 
            false,
            false,
            false,
            false,
            false
        };
        private static bool deathPlaying = false;
        private static bool wonPlaying = false;
        public static void Play()
        {
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;

            if (Information.KnightsJourney.worldState==WorldState.MainMenu && !IsPlaying[0])
            {
                MediaPlayer.Play(soundtracks[0]);
                for (int i = 0; i < IsPlaying.Count; i++)
                    IsPlaying[i] = false;
                IsPlaying[0] = true;
                deathPlaying = false;
                wonPlaying= false;
            }            
            if (Information.KnightsJourney.worldState == WorldState.Tutorial && !IsPlaying[1])
            {
                MediaPlayer.Play(soundtracks[1]);
                for (int i = 0; i < IsPlaying.Count; i++)
                    IsPlaying[i] = false;
                IsPlaying[1] = true;
                deathPlaying = false;
                wonPlaying = false;
            }
            if(Information.KnightsJourney.worldState == WorldState.Level1 && !IsPlaying[2])
            {
                MediaPlayer.Play(soundtracks[2]);
                for (int i = 0; i < IsPlaying.Count; i++)
                    IsPlaying[i] = false;
                IsPlaying[2] = true;
                deathPlaying = false;
                wonPlaying = false;
            }
            if (Information.KnightsJourney.worldState == WorldState.Level2 && !IsPlaying[3])
            {
                MediaPlayer.Play(soundtracks[3]);
                for (int i = 0; i < IsPlaying.Count; i++)
                    IsPlaying[i] = false;
                IsPlaying[3] = true;
                deathPlaying = false;
                wonPlaying = false;
            }
            if (Information.KnightsJourney.worldState == WorldState.Level3 && !IsPlaying[4])
            {
                MediaPlayer.Play(soundtracks[4]);
                for (int i = 0; i < IsPlaying.Count; i++)
                    IsPlaying[i] = false;
                IsPlaying[4] = true;
                deathPlaying = false;
                wonPlaying = false;
            }

            if (Information.KnightsJourney.worldState == WorldState.Death && !deathPlaying)
            {
                deathPlaying= true;
                MediaPlayer.Stop();
                death.Play();
                wonPlaying = false;
                for (int i = 0; i < IsPlaying.Count; i++)
                    IsPlaying[i] = false;
            }

            if (Information.KnightsJourney.worldState == WorldState.Won && !wonPlaying)
            {
                wonPlaying = true;
                MediaPlayer.Stop();
                won.Play();
                deathPlaying= false;
                for (int i = 0; i < IsPlaying.Count; i++)
                    IsPlaying[i] = false;
            }
        }

        public static void LoadContent(ContentManager Content)
        {
            soundtracks.Add(Content.Load<Song>("Soundtrack/MainMenu/vgm-atmospheric-below"));
            soundtracks.Add(Content.Load<Song>("Soundtrack/Tutorial/Worldmap Theme"));
            soundtracks.Add(Content.Load<Song>("Soundtrack/Level1/Grasslands Theme"));
            soundtracks.Add(Content.Load<Song>("Soundtrack/Level2/Mushroom Theme"));
            soundtracks.Add(Content.Load<Song>("Soundtrack/Level3/Boss Theme"));

            death = Content.Load<SoundEffect>("Soundtrack/Death/mixkit-game-over-trombone-1940");
            won = Content.Load<SoundEffect>("Soundtrack/Won/mixkit-video-game-win-2016");
        }
    }
}
