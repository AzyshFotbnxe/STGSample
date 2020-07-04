using Microsoft.Xna.Framework.Graphics;
using STGSample.Enemies;
using STGSample.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Utils
{
    public static class ConstantTable
    {
        public const float MOVERATE = 5f;
        public const float FINERMOVERATE = 2f;
        public const int BATTLEAREAWIDTH = 600;
        public const int BATTLEAREAHEIGHT = 600;
        public const int DESTROYMARGIN = 100;
        public static SpriteFont FONT;
        public readonly static Random RANDOM = new Random();
        public const int CAMERAWIDTH = 400;
        public const string ARCHIVEPATH = "player.sav";
        public const string SPRITEPATH = "Sprites.xml";
        public readonly static Dictionary<Type, List<(float, Type)>> DROPTABLE = new Dictionary<Type, List<(float, Type)>>
        {
            { typeof(SampleEnemy), new List<(float, Type)>{ (0.2f, typeof(HealPack)) } }
        };
    }
}
