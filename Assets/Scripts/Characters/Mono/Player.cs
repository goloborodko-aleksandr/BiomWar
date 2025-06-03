using System;
using System.Collections.Generic;
using Characters.Classes;
using Characters.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;
using Zenject;

namespace Characters.Mono
{
    public class Player: BaseCharacter, IMovable
    {
        [SerializeField] private float coolDownMove;
        private List<Floor> eligibleFloors;
        private Floor targetFloor;
        private IShowWay showWay;
        private float progressTime;
        private Fsm status;
        private Fsm debuff;
        public override Floor CurrentFloor { get; set; }
        public override List<Floor> EligibleFloors => eligibleFloors;
        public override Floor TargetFloor => targetFloor;
        public override IShowWay ShowWay => showWay;
        public float ProgressTime { get => progressTime; set => progressTime = value; }
        public Fsm Status => status;
        public Fsm Debuff => debuff;

        [Inject]
        public void Construct(IShowWay showWay)
        {
            this.showWay = showWay;
        }

        public void Move(Floor target, List<Floor> floors)
        {
            eligibleFloors = floors;
            targetFloor = target;
            status.ChangeState<Move>();
        }
        
        public void Init(Floor target, List<Floor> floors)
        {
            eligibleFloors = floors;
            targetFloor = target;
            status = new Fsm();
            status.AddState(new Init(status,this, coolDownMove));
            status.AddState(new Idle(status,this));
            status.AddState(new Move(status,this));
            status.ChangeState<Init>();
        }


        public float ProgressMoveValue => Mathf.Clamp(progressTime / coolDownMove, 0, 1);


        private void Update()
        {
            status?.Update();
            debuff?.Update();
        }
    }
}