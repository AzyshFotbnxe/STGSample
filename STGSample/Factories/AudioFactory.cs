using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using STGSample.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.AudioFactories
{
    public static class AudioFactory
    {
        private static ContentManager content;
        private static Dictionary<string, string> songDictionary;
        private static Dictionary<string, string> soundDictionary;
        private static Dictionary<string, string> hurryDictionary;      
        public static void Initialize(ContentManager inputContent, string soundPath, string musicPath, string hurryPath)
        {
            content = inputContent;
            var audioLoader = new AudioLoader(soundPath, musicPath, hurryPath);
            songDictionary = audioLoader.MusicInfo;
            soundDictionary = audioLoader.SoundInfo;
            hurryDictionary = audioLoader.HurryInfo;
        }

        public static Song CreateSong(string name)
        {
            if (songDictionary.TryGetValue(name, out string song)) return content.Load<Song>(song);
            Console.WriteLine("Cannot find song" + name);
            return null;
        }

        public static Song CreateHurrySong(Song song, out bool isHurry)
        {
            string songName = song.Name;
            isHurry = true;
            bool foundHurry = hurryDictionary.TryGetValue(songName, out string hurry);
            if (!foundHurry) return song;
            isHurry = false;
            return content.Load<Song>(hurry);
        }

        public static SoundEffect CreateSound(string name)
        {
            if (soundDictionary.TryGetValue(name, out string sound)) return content.Load<SoundEffect>(sound);
            Console.WriteLine("Cannot find sound" + name);
            return null;
        }
    }
}
