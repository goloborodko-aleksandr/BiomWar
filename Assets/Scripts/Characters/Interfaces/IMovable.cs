using System.Collections.Generic;
using GenerateAndCreateMap.Mono;

namespace Characters.Interfaces
{
    public interface IMovable
    {
        void Move(Floor target, List<Floor> floors);
    }
}