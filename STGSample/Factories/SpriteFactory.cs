using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STGSample.Loading;

namespace STGSample.Factories
{
    public static class SpriteFactory
    {
        private static ContentManager content;
        private static Dictionary<Type, SpriteNode> spritesInfo;

        public static void Initialize(ContentManager inputContent)
        {
            content = inputContent;
            SpriteLoader spritesLoading = new SpriteLoader();
            spritesInfo = spritesLoading.SpritesInfo();
        }

        public static Sprite CreateSprite(Type type)
        {
            if (!(spritesInfo.TryGetValue(type, out SpriteNode spriteNode)))
                throw new System.ArgumentException("Cannot find: " + type + " in the dictionary");
            return new Sprite(content.Load<Texture2D>(spriteNode.SpriteName), spriteNode.TotalFrame);
        }

        public static Vector2 ObjectSize(Type objectType)
        {
            if (spritesInfo.TryGetValue(objectType, out SpriteNode spriteNode))
                return new Vector2(spriteNode.Width, spriteNode.Height);
            return new Vector2();
        }
    }
}
