using Characters.Fsm;
using Characters.Mono;
using UnityEngine;

namespace Characters.Player.States
{
    public class Init: IState
    {
        private BaseCharacter _character;
        public Fsm.Fsm fsm { get; }

        public Init(Fsm.Fsm fsm, Player character)
        {
            this.fsm = fsm;
            _character = character;
        }
        public void EnterState()
        {
            _character.TargetFloor.ComeCharacter(_character);
            _character.transform.position = _character.CurrentFloor.transform.position + Vector3.up * 1.3f;
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