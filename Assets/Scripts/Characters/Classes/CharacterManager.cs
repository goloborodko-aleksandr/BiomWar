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
    public class CharacterManager : IMover, IDisposable
    {
        private Player player;
        private IInput input;
        private Map map;
        private IShowWay showWay;


        [Inject]
        CharacterManager(Player player, Map map, IInput input, IShowWay showWay)
        {
            Debug.Log($"Creating CharacterMoveManager");
            this.player = player;
            this.map = map;
            this.input = input;
            this.showWay = showWay;
            map.GetPointsMap().Last().ComeCharacter(this.player); //пока стартуем здесь
            this.player.transform.position = this.player.Floor.transform.position + Vector3.up; //пока стартуем здесь
            this.showWay.Show(CheckPath(this.player, this.player.CharacterSpeed));
            input.OnDirectionInput += Input;
        }

        public void Input(IPoint point)
        {
            if(!player.IsMove) return;
            Move(point);
        }


        public void Move(IPoint point)
        {
            var target = point.GetPoint();
            var walkables = CheckPath(player, player.CharacterSpeed);
            Floor floor = walkables.FirstOrDefault(i => i.GetPoint() == target);
            if (!walkables.Contains(floor)) return;
            player.transform.position = floor.transform.position + Vector3.up;
            floor.ComeCharacter(player);
            showWay.Show(CheckPath(player, player.CharacterSpeed));
        }

        private List<Floor> CheckPath(BaseCharacter character, int characterSpeed)
        {
            int distance = characterSpeed / 5 < 1 ? 1 : characterSpeed / 4;
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
            input.OnDirectionInput -= Input;
        }
    }
}