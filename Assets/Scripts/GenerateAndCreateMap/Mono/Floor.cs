using System.Collections.Generic;
using Characters.Mono;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using Zenject;

namespace GenerateAndCreateMap.Mono
{
    public abstract class Floor : MonoBehaviour, IPoint
    {
        private int x, y, z;
        public int X => x;
        public int Y => y;
        public int Z => z;
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
            Characters.Add(character);
            character.Floor = this;
        }

        public Vector3 GetPoint() => new Vector3(x, y, z);

        public FloorType GetFloorType() => floorType;

        public void Initialize(IPoint point)
        {
            x = point.X;
            y = point.Y;
            z = point.Z;
            floorType = point.GetFloorType();
        }
    }
}