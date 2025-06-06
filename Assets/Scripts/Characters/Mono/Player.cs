using System;
using System.Collections.Generic;
using Characters.Classes;
using Characters.Interfaces;
using GenerateAndCreateMap.Mono;
using R3;
using UnityEngine;
using Zenject;

namespace Characters.Mono
{
    public class Player: BaseCharacter, IMovable, IProgressive
    {
        [SerializeField] private float coolDownMove;
        private List<Floor> eligibleFloors;
        private Floor targetFloor;

        private Fsm status;
        private Fsm debuff;
        public override Floor CurrentFloor { get; set; }
        public override List<Floor> EligibleFloors => eligibleFloors;
        public override Floor TargetFloor => targetFloor;

        public Fsm Status => status;
        public Fsm Debuff => debuff;

        private Progress progress;
        public Progress Progress => progress;

        [Inject]
        public void Construct(Progress progress)
        {
            this.progress = progress;
        }

        public void Move(Floor target, List<Floor> floors)
        {
            if(status.CurrentState?.GetType() != typeof(Idle) || progress.ProgressTimeProperty.CurrentValue < 1) return;
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
            status.AddState(new Idle(status,this, ()=>progress.StartProgress(CharacterSpeed, coolDownMove)));
            status.AddState(new Move(status,this));
            status.ChangeState<Init>();
            // progress.StartProgress(CharacterSpeed,coolDownMove);
        }


        private void Update()
        {
            status?.Update();
            debuff?.Update();
        }
    }
}