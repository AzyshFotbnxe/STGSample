using STGSample.Utils;

namespace STGSample
{
    public interface IItem : IGameObject
    {
        Physics Physics { get; }
        void Collected(IPlayer player);
    }
}
