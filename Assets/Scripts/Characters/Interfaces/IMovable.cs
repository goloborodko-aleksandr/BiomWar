using Characters.Mono;
using GenerateAndCreateMap.Interfaces;

namespace Characters.Interfaces
{
    public interface IMovable
    {
        void Move(IPoint point);
    }
}