using Characters.Fsm;
using DG.Tweening;
using UnityEngine;

namespace Characters.CharacterPlayer.States
{
    public class FailMove: IState
    {
        private BaseCharacter _character;
        private Vector3 _start;
        private Vector3 _end;
        public Fsm.Fsm fsm { get; }


        public FailMove(Fsm.Fsm fsm, BaseCharacter character)
        {
            this.fsm = fsm;
            _character = character;
        }
        public void EnterState()
        {
            Debug.Log("FAIL");
            _character.Progress.StartProgress(_character.CharacterSpeed, _character.CoolDown);
            _character.CharacterAnimator.Idle();
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