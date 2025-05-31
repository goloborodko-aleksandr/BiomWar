using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap.Interfaces;
using GenerateAndCreateMap.Mono;
using Tools;
using UnityEngine;
using Zenject;

namespace Characters.Classes
{
    public class CharacterMoveManager : IMovable, IDisposable
    {
        private IPlayer player;
        private BaseCharacter character;
        private TimerService timerService;
        private IInput input;
        private Map map;
        private IShowWay showWay;


        [Inject]
        CharacterMoveManager(IPlayer player, Map map, IInput input, IShowWay showWay, TimerService timerService)
        {
            Debug.Log($"Creating CharacterMoveManager");
            this.player = player;
            this.map = map;
            this.input = input;
            this.timerService = timerService;
            this.showWay = showWay;
            character = (BaseCharacter)this.player;
            map.GetPointsMap().Last().ComeCharacter(character);
            character.transform.position = character.Floor.transform.position + Vector3.up; //пока стартуем здесь
            this.showWay.Show(CheckPath(character, character.CharacterSpeed));
            input.OnDirectionInput += Move;
        }


        public void Move(IPoint point)
        {
            int testSpeedFormula = character.CharacterSpeed; // дистанция клеток от скорости будет зависеть
            var target = point.GetPoint();
            var walkables = CheckPath(character, testSpeedFormula);
            Floor floor = walkables.FirstOrDefault(i => i.GetPoint() == target);
            if (!walkables.Contains(floor)) return;
            player.MainPlayerTransform.position = floor.transform.position + Vector3.up;
            floor.ComeCharacter(character);
            showWay.Show(CheckPath(character, character.CharacterSpeed));
        }

        private List<Floor> CheckPath(BaseCharacter character, int distance)
        {
            var walkables = map.GetPointsMap().Where(i =>
            {
                var delta = character.Floor.GetPoint() - i.GetPoint();
                return !(
                    delta == Vector3.zero
                    || !i.IsWalkable
                    || Mathf.Abs(delta.x) > distance || Mathf.Abs(delta.z) > distance //допустимая длина клеток
                    || Mathf.Abs(delta.x) != 0 && Mathf.Abs(delta.z) !=0); // по диагонали ходьба
            }).ToList();
            return walkables;
        }
        

        public void Dispose()
        {
            input.OnDirectionInput -= Move;
        }
    }
}