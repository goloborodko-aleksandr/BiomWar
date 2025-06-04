using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap.Interfaces;
using GenerateAndCreateMap.Mono;
using R3;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

namespace Characters.Classes
{
    public class CharacterMoveHandler : IDisposable
    {
        private Player player;
        private IInput input;
        private Map map;


        private CompositeDisposable compositeDisposable = new CompositeDisposable();
        
        [Inject]
        CharacterMoveHandler(Player player, Map map, IInput input)
        {
            Debug.Log($"Creating CharacterMoveManager");
            this.player = player;
            this.map = map;
            this.input = input;
            //init пока так
            var startFloor = this.map.GetFloorsMap().Last();
            var variants = GetVariantsPath(this.player, startFloor);
            this.player.Init(startFloor, variants);
            //
            this.input
                .OnDirectionInput
                .Subscribe(Path)
                .AddTo(compositeDisposable);
        }

        private void Path(IPoint point)
        {
            var eligibleFloors = GetVariantsPath(player, player.CurrentFloor);
            var floor = eligibleFloors.FirstOrDefault(i => i.GetPoint() == point.GetPoint());
            if (eligibleFloors.Contains(floor))
            {
                var newEligibleFloors = GetVariantsPath(player, floor);
                player.Move(floor, newEligibleFloors);    
            }
        }
        

        private List<Floor> GetVariantsPath(BaseCharacter character, Floor floorAround)
        {
            int distance = character.CharacterSpeed / 4 < 1 ? 1 : character.CharacterSpeed / 4;
            var eligibleFloors = map.GetFloorsMap().Where(i =>
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
        

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}