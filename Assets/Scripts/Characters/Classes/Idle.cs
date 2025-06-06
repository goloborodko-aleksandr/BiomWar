using System;
using System.Collections.Generic;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace Characters.Classes
{
    public class Idle: IState
    {
        private BaseCharacter _character;

        public Fsm fsm { get; }

        public Idle(Fsm fsm,  BaseCharacter character)
        {
            this.fsm = fsm; 
            _character = character;
        }
        public void EnterState()
        {
            _character.Progress.StartProgress(_character.CharacterSpeed, _character.CoolDown);
        }

        public void ExitState()
        {
            
        }

        public void Update()
        {
            
        }
    }
}