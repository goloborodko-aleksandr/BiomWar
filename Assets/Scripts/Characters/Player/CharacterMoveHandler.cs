using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap;
using GenerateAndCreateMap.Floors;
using GenerateAndCreateMap.Interfaces;
using R3;
using UnityEngine;
using Zenject;

namespace Characters.Player
{
    public class CharacterMoveHandler : IDisposable
    {
        private Characters.Player.Player _player;
        private IInput _input;
        private Map _map;


        private CompositeDisposable compositeDisposable = new CompositeDisposable();
        
        [Inject]
        CharacterMoveHandler(Player player, Map map, IInput input)
        {
            Debug.Log($"Creating CharacterMoveManager");
            _player = player;
            _map = map;
            _input = input;
            //init пока так прокину zenject
            var startFloor = this._map.GetFloorsMap().Last();
            var variants = GetVariantsPath(this._player, startFloor);
            _player.Init(startFloor, variants);
            //
            _input
                .OnDirectionInput
                .Subscribe(Path)
                .AddTo(compositeDisposable);
        }

        private void Path(IPoint point)
        {
            var eligibleFloors = GetVariantsPath(_player, _player.CurrentFloor);
            var floor = eligibleFloors.FirstOrDefault(i => i.GetPoint() == point.GetPoint());
            if (eligibleFloors.Contains(floor))
            {
                var newEligibleFloors = GetVariantsPath(_player, floor);
                _player.Move(floor, newEligibleFloors);    
            }
        }
        

        private List<Floor> GetVariantsPath(BaseCharacter character, Floor floorAround)
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
        

        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}