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
        private Floor floor;
        private float progressTime;
        private Fsm status;
        private Fsm debuff;
        public override Floor Floor { get; set; }


        public float ProgressMoveValue => Mathf.Clamp(progressTime / coolDownMove, 0, 1);
        public bool IsMove => ProgressMoveValue >= 1;


        private void Update()
        {
            progressTime += Time.deltaTime * CharacterSpeed;
            status?.Update();
            debuff?.Update();
        }
    }
}