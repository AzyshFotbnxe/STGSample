using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STGSample.Utils.XMLUtils;

namespace STGSample.Loading
{
    public class AudioLoader
    {
        public Dictionary<string, string> MusicInfo { get; }
        public Dictionary<string, string> SoundInfo { get; }
        public Dictionary<string, string> HurryInfo { get; }
        public AudioLoader(string soundPath, string musicPath, string hurryPath)
        {
            List<AudioNode> sounds, musics, hurry;
            sounds = XMLReader<AudioNode>(soundPath);
            SoundInfo = new Dictionary<string, string>();
            foreach (AudioNode soundnode in sounds)
            {
                SoundInfo.Add(soundnode.Name, soundnode.AudioName);
            }
            musics = XMLReader<AudioNode>(musicPath);
            MusicInfo = new Dictionary<string, string>();
            foreach (AudioNode musicnode in musics)
            {
                MusicInfo.Add(musicnode.Name, musicnode.AudioName);
            }
            hurry = XMLReader<AudioNode>(hurryPath);
            HurryInfo = new Dictionary<string, string>();
            foreach (AudioNode hurrynode in hurry)
            {
                HurryInfo.Add(hurrynode.Name, hurrynode.AudioName);
            }

        }
    }
}
