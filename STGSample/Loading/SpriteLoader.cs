using System;
using System.Collections.Generic;
using static STGSample.Utils.XMLUtils;
using static STGSample.Utils.ConstantsTable;

namespace STGSample.Loading
{
    public class SpriteLoader
    {
        private readonly List<SpriteNode> spritesCollection = new List<SpriteNode>
        {
            new SpriteNode("STGSample.Players.Player","player", 30, 30),
            new SpriteNode("STGSample.Bullets.Bullet","bullet", 9, 9),
            new SpriteNode("STGSample.Bullets.EnemyBullet","enemyBullet", 9, 9),
            new SpriteNode("STGSample.Bullets.BouncingBullet","enemyBullet", 9, 9),
            new SpriteNode("STGSample.Test.TestCircle", "circle", 20, 20),
            new SpriteNode("STGSample.Enemies.SampleEnemy", "normalenemy", 20, 20),
            new SpriteNode("STGSample.Enemies.SampleBoss", "normalenemy", 20, 20),
            new SpriteNode("STGSample.Bullets.FireBall","fireball",totalFrame:2),
            new SpriteNode("STGSample.Enemies.Tracer", "circle"),
            new SpriteNode("STGSample.Items.HealPack", "healpack", 18, 18),
            new SpriteNode("STGSample.Bullets.RotatingBullet", "enemyBullet")
        };

        public SpriteLoader()
        {
            XMLWriter(SPRITEPATH, spritesCollection);
        }

        public Dictionary<Type, SpriteNode> SpritesInfo()
        {
            Dictionary<Type, SpriteNode> spritesInfo = new Dictionary<Type, SpriteNode>();
            List<SpriteNode> spritesList = XMLReader<SpriteNode>(SPRITEPATH);
            foreach (SpriteNode node in spritesList)
            {
                spritesInfo.Add(Type.GetType(node.ObjectName), node);
            }
            return spritesInfo;
        }
    }
}