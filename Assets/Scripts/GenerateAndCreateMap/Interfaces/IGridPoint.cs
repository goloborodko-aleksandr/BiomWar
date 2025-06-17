using GenerateAndCreateMap.Floors;
using UnityEngine;

namespace GenerateAndCreateMap.Interfaces
{
    public interface IGridPoint
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        
        Vector3 GetPoint();
        FloorType GetFloorType();
    }
}