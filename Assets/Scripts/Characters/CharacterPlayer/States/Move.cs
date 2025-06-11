using Characters.Fsm;
using DG.Tweening;
using UnityEngine;

namespace Characters.CharacterPlayer.States
{
    public class Move: IState
    {
        private BaseCharacter _character;
        private Vector3 _start;
        private Vector3 _end;
        public Fsm.Fsm fsm { get; }


        public Move(Fsm.Fsm fsm, BaseCharacter character)
        {
            this.fsm = fsm;
            _character = character;
        }
        public void EnterState()
        {
            _start = _character.transform.position;
            _end = _character.TargetFloor.transform.position + Vector3.up;
            Sequence sequence = DOTween.Sequence()
                .Append(_character.transform
                    .DOLookAt(_end, 0.2f, AxisConstraint.Y)
                    .SetEase(Ease.Linear))
                .AppendCallback(() =>
                {
                    _character.CharacterAnimator.Jump();
                })
                .Append(_character.transform
                    .DOJump(_end, 1f, 1, 0.45f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        _character.TargetFloor.ComeCharacter(_character);
                        fsm.ChangeState<Idle>();
                    }));
            sequence.Play();
        }
        
        

        public void ExitState()
        {

        }

        public void Update()
        {

        }
    }
}