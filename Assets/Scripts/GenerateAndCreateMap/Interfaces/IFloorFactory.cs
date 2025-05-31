using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace GenerateAndCreateMap.Interfaces
{
    public interface IFloorFactory
    {
        Floor CreateFloor(IPoint point, Transform transform);
    }
}