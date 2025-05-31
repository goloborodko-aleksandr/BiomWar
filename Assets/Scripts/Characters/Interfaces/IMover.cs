using Characters.Mono;
using GenerateAndCreateMap.Interfaces;

namespace Characters.Interfaces
{
    public interface IMover
    {
        void Move(IPoint point);
    }
}