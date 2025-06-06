using System.Collections.Generic;
using Characters.Mono;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using Zenject;

namespace GenerateAndCreateMap.Mono
{
    public abstract class Floor : MonoBehaviour, IPoint
    {
        private int _x, _y, _z;
        public int X => _x;
        public int Y => _y;
        public int Z => _z;
        public bool IsWalkable => isWalkableMap[floorType];
        private Dictionary<FloorType, bool> isWalkableMap;
        private FloorType floorType;

        private List<BaseCharacter> characters = new();

        public List<BaseCharacter> Characters
        {
            get => characters;
            private set => characters = value;
        }

        [Inject]
        public void Construct(Dictionary<FloorType, bool> isWalkableMap)
        {
            this.isWalkableMap = isWalkableMap;
        }

        public void ComeCharacter(BaseCharacter character)
        {
            character.CurrentFloor?.Characters.Remove(character);
            Characters.Add(character);
            character.CurrentFloor = this;
        }

        public Vector3 GetPoint() => new Vector3(X, Y, Z);

        public FloorType GetFloorType() => floorType;

        public void Initialize(IPoint point)
        {
            _x = point.X;
            _y = point.Y;
            _z = point.Z;
            floorType = point.GetFloorType();
        }
    }
}