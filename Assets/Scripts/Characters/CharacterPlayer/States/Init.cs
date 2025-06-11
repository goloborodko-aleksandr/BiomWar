using Characters.Fsm;
using GenerateAndCreateMap;
using UnityEngine;
using Zenject;

namespace Characters.CharacterPlayer.States
{
    public class Init: IState
    {
        private BaseCharacter _character;

        public Fsm.Fsm fsm { get; }

        [Inject]
        public Init(Fsm.Fsm fsm, BaseCharacter character)
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