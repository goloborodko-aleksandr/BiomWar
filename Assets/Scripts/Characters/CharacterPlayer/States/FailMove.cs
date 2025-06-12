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
            Sequence sequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _character.CharacterAnimator.Jump();
                })
                .Append(_character.transform
                    .DOJump(_character.transform.position, 1, 1, 0.45f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        Debug.Log("Fail message => low speed");
                        _character.Progress.StartProgress(_character.CharacterSpeed, _character.CoolDown);
                        _character.CharacterAnimator.Idle();
                        fsm.ChangeState<Idle>();
                    })
                );
        }
        
        

        public void ExitState()
        {

        }

        public void Update()
        {

        }
    }
}