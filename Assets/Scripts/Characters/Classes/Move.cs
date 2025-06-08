using System;
using Characters.Interfaces;
using Characters.Mono;
using GenerateAndCreateMap.Mono;
using R3;
using UnityEngine;

namespace Characters.Classes
{
    public class Move: IState
    {
        private Player _player;
        private Vector3 _start;
        private Vector3 _end;
        public Fsm fsm { get; }
        private CompositeDisposable _disposable { get; } = new CompositeDisposable();

        public Move(Fsm fsm, Player player)
        {
            this.fsm = fsm;
            _player = player;
        }
        public void EnterState()
        {
            _start = _player.transform.position;
            _end = _player.TargetFloor.transform.position + Vector3.up * 1.3f;
            _player.transform.position = _end;
            _player.TargetFloor.ComeCharacter(_player);
            Observable
                .Timer(TimeSpan.FromSeconds(0.3f))
                .Subscribe(_ =>
                {
                    fsm.ChangeState<Idle>();
                })
                .AddTo(_disposable);
        }
        
        

        public void ExitState()
        {
            _disposable.Clear();
        }

        public void Update()
        {

        }
    }
}