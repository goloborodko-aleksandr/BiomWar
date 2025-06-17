using System;
using GenerateAndCreateMap.Floors;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;

namespace GenerateAndCreateMap
{
    [Serializable]
    public class GridPoint : IGridPoint
    {
        [SerializeField] private int _x, _y, _z;
        [SerializeField] private FloorType _floorType;

        public int X => _x;
        public int Y => _y;
        public int Z => _z;

        public GridPoint(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public Vector3 GetPoint() => new Vector3(X, Y, Z);
        public FloorType GetFloorType() => _floorType;
    }

}
