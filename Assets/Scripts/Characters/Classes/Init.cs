using System.Collections.Generic;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace Characters.Classes
{
    public class Init: IState
    {
        private BaseCharacter _character;
        public Fsm fsm { get; }

        public Init(Fsm fsm, Player character)
        {
            this.fsm = fsm;
            _character = character;
        }
        public void EnterState()
        {
            _character.TargetFloor.ComeCharacter(_character);
            _character.transform.position = _character.CurrentFloor.transform.position + Vector3.up;
            fsm.ChangeState<Idle>();
        }

        public void ExitState()
        {
            
        }

        public void Update()
        {
            
        }
    }
}