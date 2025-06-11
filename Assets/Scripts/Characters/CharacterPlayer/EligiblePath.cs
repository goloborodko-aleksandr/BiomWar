using System.Collections.Generic;
using System.Linq;
using GenerateAndCreateMap;
using GenerateAndCreateMap.Floors;
using UnityEngine;

namespace Characters.CharacterPlayer
{
    public class EligiblePath
    {
        private Map _map;

        public EligiblePath(Map map)
        {
            Debug.Log($"Creating Eligible Path");
            _map = map;
        }
        
        public List<Floor> GetVariantsPath(BaseCharacter character, Floor floorAround)
        {
            int distance = character.CharacterSpeed / 4 < 1 ? 1 : character.CharacterSpeed / 4;
            var eligibleFloors = _map.GetFloorsMap().Where(i =>
            {
                var delta = floorAround.GetPoint() - i.GetPoint();
                return !(
                    delta == Vector3.zero
                    || !i.IsWalkable
                    || Mathf.Abs(delta.x) > distance || Mathf.Abs(delta.z) > distance //допустимая длина клеток
                    || Mathf.Abs(delta.x) != 0 && Mathf.Abs(delta.z) != 0); // по диагонали ходьба
            }).ToList();
            return eligibleFloors;
        }
    }
}