using System;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace GenerateAndCreateMap.Classes
{
    [Serializable]
    public class Point : IPoint
    {
        [SerializeField] private int _x, _y, _z;
        [SerializeField] private FloorType _floorType;

        public int X => _x;
        public int Y => _y;
        public int Z => _z;

        public Point(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public Vector3 GetPoint() => new Vector3(X, Y, Z);
        public FloorType GetFloorType() => _floorType;
    }

}
