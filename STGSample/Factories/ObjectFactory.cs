using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using STGSample.Items;
using STGSample.Manager;
using System;
using System.Collections.Generic;

namespace STGSample.Factories
{
    public static class ObjectFactory
    {
        private static ObjectsManager objectsManager;
        private readonly static Dictionary<Type, Vector2> dictionary = new Dictionary<Type, Vector2>
        {

        };
        public static void Initialize(ObjectsManager manager)
        {
            objectsManager = manager;
        }
        /* Mainly used for itemBlock creates items*/
        public static void CreateNonCollidableObject(Type type, Vector2 location)
        {
            if (dictionary.TryGetValue(type, out Vector2 offSet))
                location += offSet;
            IGameObject obj = (IGameObject)Activator.CreateInstance(type, location);
            objectsManager.AddNonCollidable(obj);
        }

        public static void CreateScoreText(Vector2 location, SpriteFont inputSpriteFont, string str)
        {
            objectsManager.AddNonCollidable(new ScoreText(location, inputSpriteFont, str));
        }

    }

}
