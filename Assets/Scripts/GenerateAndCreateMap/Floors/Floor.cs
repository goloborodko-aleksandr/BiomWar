using System.Collections.Generic;
using Characters;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using Zenject;

namespace GenerateAndCreateMap.Floors
{
    public abstract class Floor : MonoBehaviour, IGridPoint
    {
        private int _x, _y, _z;
        public int X => _x;
        public int Y => _y;
        public int Z => _z;
        public bool IsWalkable => _isWalkableMap[_floorType];
        private Dictionary<FloorType, bool> _isWalkableMap;
        private FloorType _floorType;

        private List<BaseCharacter> _characters = new();


        public List<BaseCharacter> Characters
        {
            get => _characters;
            private set => _characters = value;
        }

        [Inject]
        public void Construct(Dictionary<FloorType, bool> isWalkableMap)
        {
            _isWalkableMap = isWalkableMap;
        }

        public virtual void ComeCharacter(BaseCharacter character)
        {
            Debuff(character);
            Characters.Add(character);
            character.CurrentFloor = this;
        }


        public virtual void LiveCharacter(BaseCharacter character)
        {
            character.CurrentFloor?.Characters.Remove(character);
        }

        public abstract void Debuff(BaseCharacter character);

        public Vector3 GetPoint() => new Vector3(X, Y, Z);

        public FloorType GetFloorType() => _floorType;

        public void Initialize(IGridPoint gridPoint)
        {
            _x = gridPoint.X;
            _y = gridPoint.Y;
            _z = gridPoint.Z;
            _floorType = gridPoint.GetFloorType();
        }
    }
}