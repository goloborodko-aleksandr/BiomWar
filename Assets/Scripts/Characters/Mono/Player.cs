using System;
using System.Collections.Generic;
using Characters.Classes;
using Characters.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;
using Zenject;

namespace Characters.Mono
{
    public class Player: BaseCharacter
    {
        [SerializeField] private float coolDownMove;
        private List<Floor> walkableFloors;
        private Floor targetFloor;
        private IShowWay showWay;
        private float progressTime;
        private Fsm status;
        private Fsm debuff;
        public override Floor Floor { get; set; }

        [Inject]
        public void Construct(IShowWay showWay)
        {
            this.showWay = showWay;
        }
        
        public void SetPath(Floor target, List<Floor> floors)
        {
            walkableFloors = floors;
            targetFloor = target;
            transform.position = target.transform.position + Vector3.up;
            targetFloor.ComeCharacter(this);
            showWay.Show(walkableFloors);
        }


        public float ProgressMoveValue => Mathf.Clamp(progressTime / coolDownMove, 0, 1);



        private void Update()
        {
            status?.Update();
            debuff?.Update();
        }
    }
}