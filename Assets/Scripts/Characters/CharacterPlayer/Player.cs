using System.Collections.Generic;
using Characters.CharacterPlayer.States;
using Characters.Interfaces;
using GenerateAndCreateMap.Floors;
using UnityEngine;
using Zenject;

namespace Characters.CharacterPlayer
{
    public class Player: BaseCharacter, IMovable
    {
        public void Move(Floor target, List<Floor> floors)
        {
            if(status.CurrentState?.GetType() != typeof(Idle) || progress.ProgressTimeProperty.CurrentValue < 1) return;
            bool isMove = Random.Range(0, characterSpeed) > 0;
            if (isMove)
            {
                status.ChangeState<FailMove>();
                return;
            }
            eligibleFloors = floors;
            targetFloor = target;
            status.ChangeState<Move>();
        }

        public void Init(Floor target, List<Floor> floors)
        {
            eligibleFloors = floors;
            targetFloor = target;
            status = new Fsm.Fsm();
            status.AddState(new Init(status,this));
            status.AddState(new Idle(status,this));
            status.AddState(new Move(status,this));
            status.AddState(new FailMove(status,this));
            status.ChangeState<Init>();
        }


        private void Update()
        {
            status?.Update();
            debuff?.Update();
        }
    }
}