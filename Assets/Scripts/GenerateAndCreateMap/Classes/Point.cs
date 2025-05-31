using System;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;

namespace GenerateAndCreateMap.Classes
{
    [Serializable]
    public class Point : IPoint
    {
        [SerializeField] private int x, y, z;
        [SerializeField] private FloorType floorType;

        public int X => x;
        public int Y => y;
        public int Z => z;

        public Point(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3 GetPoint() => new Vector3(X, Y, Z);
        public FloorType GetFloorType() => floorType;
    }

}
