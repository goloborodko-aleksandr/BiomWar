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
        private Player player;
        private float elapsed;
        private float duration;
        private Vector3 start;
        private Vector3 end;
        public Fsm fsm { get; }
        private CompositeDisposable _disposable { get; } = new CompositeDisposable();

        public Move(Fsm fsm, Player player)
        {
            this.fsm = fsm;
            this.player = player;
        }
        public void EnterState()
        {
            start = player.transform.position;
            end = player.TargetFloor.transform.position + Vector3.up;
            elapsed = 0f;
            duration = 1;
            player.ProgressTime = 0;
            Observable
                .Timer(TimeSpan.FromSeconds(duration))
                .Subscribe(_ =>
                {
                    player.TargetFloor.ComeCharacter(player);
                    fsm.ChangeState<Idle>();
                })
                .AddTo(_disposable);
        }
        
        

        public void ExitState()
        {
            
        }

        public void Update()
        {
            if (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                player.transform.position = Vector3.Lerp(start, end, t);
            }
        }
    }
}