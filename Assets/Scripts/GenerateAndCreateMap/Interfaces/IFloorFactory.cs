using GenerateAndCreateMap.Floors;
using UnityEngine;

namespace GenerateAndCreateMap.Interfaces
{
    public interface IFloorFactory
    {
        Floor CreateFloor(IGridPoint gridPoint, Transform transform);
    }
}